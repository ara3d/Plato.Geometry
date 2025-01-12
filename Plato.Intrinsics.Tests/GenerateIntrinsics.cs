using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ara3D.Utils;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework.Constraints;
using Console = System.Console;

namespace Plato.Geometry.Tests
{
    public static class GenerateIntrinsics
    {
        public static class Intrinsics
        {
            public static DirectoryPath Dir = PathUtil.GetCallerSourceFolder()
                .RelativeFolder("..", "Plato.Core");

            public static Dictionary<Type, string> Types = new()
            {
                { typeof(Matrix4x4),"System.Numerics.Matrix4x4" },
                { typeof(Matrix3x2),"System.Numerics.Matrix3x2" },
                { typeof(Vector2), "System.Numerics.Vector2" },
                { typeof(Vector3), "System.Numerics.Vector3" },
                { typeof(Vector4), "System.Numerics.Vector4" },
                { typeof(Vector8), "System.Runtime.Intrinsics.Vector256<float>" },
                { typeof(Plane), "System.Numerics.Plane" },
                { typeof(Quaternion), "System.Numerics.Quaternion" },
                //{ typeof(float), "float" },
                { typeof(Integer), "int" },
                { typeof(Boolean), "bool" },
            };

            // https://stackoverflow.com/questions/3016429/reflection-and-operator-overloads-in-c-sharp
            public static (string, string, int, string)[] OverloadedOperators =
            {
                ("op_Addition", "+", 2, "Add"),
                ("op_Subtraction", "-", 2, "Subtract"),
                ("op_Division", "/", 2, "Divide"),
                ("op_Multiply", "*", 2, "Multiply"),
                ("op_Modulus", "%", 2, "Modulus"),
                ("op_ExclusiveOr", "^", 2, "ExclusiveOr"),
                ("op_BitwiseAnd", "&", 2, "BitwiseAnd"),
                ("op_BitwiseOr", "|", 2, "BitwiseOr"),
                ("op_LogicalAnd", "&&", 2, "And"),
                ("op_LogicalOr", "||", 2, "Or"),
                ("op_Equality", "==", 2, "Equals"),
                ("op_GreaterThan", ">", 2, "GreaterThan"),
                ("op_LessThan", "<", 2, "LessThan"),
                ("op_GreaterThanOrEqual", ">=", 2, "GreaterThanOrEquals"),
                ("op_LessThanOrEqual", "<=", 2, "LessThanOrEquals"),
                ("op_Inequality", "!=", 2, "NotEquals"),
                ("op_UnaryNegation", "-", 1, "Negative"),
                ("op_OnesComplement", "~", 1, "Complement"),
                ("op_LogicalNot", "!", 1, "Not")
            };

            public static void WritePlatoFunction(CodeBuilder cb, string retType, string name, IEnumerable<string> parameterNames, IEnumerable<string> parameterTypes)
            {
                var paramStr = string.Join(", ", parameterNames.Zip(parameterTypes, (n, t) => $"{n}: {t}"));
                cb.WriteLine($"{name}({paramStr}): {retType};");
            }

            public static void WriteFunction(CodeBuilder cb, string retType, string name, string body, IEnumerable<string> parameterNames, IEnumerable<string> parameterTypes)
            {
                var paramStr = string.Join(", ", parameterNames.Zip(parameterTypes, (n, t) => $"{t} {n}"));
                cb.WriteLine($"[MethodImpl(AggressiveInlining)] public static {retType} {name}(this {paramStr}) => {body};");
            }

            public static void WriteFieldAccessor(CodeBuilder cb, CodeBuilder cb2, CodeBuilder cb3, FieldInfo fi, string typeName, string altType)
            {
                var self = fi.IsStatic ? typeName : "self";

                var pNames = new[] { "self" };
                var pTypes = new[] { typeName };
                var fieldType = TypeToString(fi.FieldType);
                WriteFunction(cb, fieldType, fi.Name, $"{self}.{fi.Name}", pNames, pTypes);

                //WritePlatoFunction(cb3, fieldType, fi.Name, pNames, pTypes);

                pTypes[0] = altType;
                WriteFunction(cb2, fieldType, fi.Name, $"Intrinsics.{fi.Name}(({typeName})self)", pNames, pTypes);
            }

            public static void WritePropertyAccessor(CodeBuilder cb, CodeBuilder cb2, CodeBuilder cb3, PropertyInfo pi, string typeName, string altType)
            {
                if (pi.GetMethod == null) return;
                var self = pi.GetMethod.IsStatic ? typeName : "self";

                var pNames = new[] { "self" };
                var pTypes = new[] { typeName };

                var propType = TypeToString(pi.PropertyType);

                if (pi.GetIndexParameters().Length > 0)
                {
                    pNames = pNames.Concat(pi.GetIndexParameters().Select(p => p.Name ?? "_")).ToArray();
                    pTypes = pTypes.Concat(pi.GetIndexParameters().Select(p => TypeToString(p.ParameterType))).ToArray();

                    var body = $"{self}[{pNames.Skip(1).JoinStringsWithComma()}]";
                    var paramStr = string.Join(", ", pNames.Zip(pTypes, (n, t) => $"{t} {n}"));
                    cb.WriteLine($"[MethodImpl(AggressiveInlining)] public static {propType} At(this {paramStr}) => {body};");
                    return;
                }

                WriteFunction(cb, propType, pi.Name, $"{self}.{pi.Name}", pNames, pTypes);

                WritePlatoFunction(cb3, propType, pi.Name, pNames, pTypes);

                pTypes[0] = altType; 
                WriteFunction(cb2, propType, pi.Name, $"Intrinsics.{pi.Name}(({typeName})self)", pNames, pTypes);
            }

            public static readonly Dictionary<Type, string> TypeAliases = new()
            {
                { typeof(bool), "bool" },
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(char), "char" },
                { typeof(decimal), "decimal" },
                { typeof(double), "double" },
                { typeof(float), "float" },
                { typeof(int), "int" },
                { typeof(uint), "uint" },
                { typeof(long), "long" },
                { typeof(ulong), "ulong" },
                { typeof(short), "short" },
                { typeof(ushort), "ushort" },
                { typeof(string), "string" },
                { typeof(object), "object" },
                { typeof(void), "void" }
            };

            public static string TypeToString(Type t)
            {
                var name = TypeAliases.TryGetValue(t, out var alias) ? alias : t.Name;

                if (name.EndsWith("&"))
                {
                    name = name.Substring(0, name.Length - 1);
                    name = t.IsByRef ? "ref " + name : "out " + name;
                }
                var tmp = name.IndexOf('`');
                if (tmp >= 0)
                {
                    name = name.Substring(0, tmp);
                    Debug.Assert(t.GenericTypeArguments.Length > 0);
                    var typeArgs = t.GenericTypeArguments.Select(TypeToString).JoinStringsWithComma();
                    name = $"{name}<{typeArgs}>";
                }
                return name;
            }

            public static IEnumerable<string> GetArgs(IEnumerable<string> types, IEnumerable<string> names)
                => types.Zip(names, (t, n) => t.StartsWith("ref ") ? $"ref {n}" : n);

            public static void WriteTypeFunctions(CodeBuilder cb, CodeBuilder cb2, CodeBuilder cb3, Type t, string altType)
            {
                var typeName = t.Name;
                
                foreach (var f in t.GetFields())
                {
                    WriteFieldAccessor(cb, cb2, cb3, f, typeName, altType);
                }

                foreach (var p in t.GetProperties())
                {
                    WritePropertyAccessor(cb, cb2, cb3, p, typeName, altType);
                }

                foreach (var m in t.GetMethods())
                {
                    var retType = TypeToString(m.ReturnType);
                    var mainType = t.Name;
                    var pNames = m.GetParameters().Select(p => p.Name ?? "_").ToList();
                    var pTypes = m.GetParameters().Select(p => TypeToString(p.ParameterType)).ToList();

                    if (m.Name.StartsWith("op_") && m.IsSpecialName)
                    {
                        foreach (var (opName, opSymbol, numParams, opFunction) in OverloadedOperators)
                        {
                            if (m.Name == opName && m.GetParameters().Length == numParams)
                            {
                                var name = opFunction;
                                var body = numParams == 2
                                    ? $"{pNames[0]} {opSymbol} {pNames[1]}"
                                    : $"{opSymbol}{pNames[0]}";
                                
                                WriteFunction(cb, retType, name, body, pNames, pTypes);
                                WritePlatoFunction(cb3, retType, name, pNames, pTypes);
                            }
                        }
                    }
                    else
                    {
                        if (m.Name == "Equals" || m.Name == "ToSystem" || m.Name == "FromSystem") continue;
                        if (m.Name.StartsWith("get_")) continue;

                        var self = m.IsStatic ? t.Name : "self";

                        pNames = pNames.Prepend("self").ToList();
                        pTypes = pTypes.Prepend(mainType).ToList();

                        var args = GetArgs(pTypes, pNames);

                        var body = $"{self}.{m.Name}({args.Skip(1).JoinStringsWithComma()})";
                        WriteFunction(cb, retType, m.Name, body, pNames, pTypes);

                        WritePlatoFunction(cb3, retType, m.Name, pNames, pTypes);

                        var pNames2 = pNames.Skip(1).Prepend($"({mainType})self").ToList();
                        var args2 = GetArgs(pTypes, pNames2);

                        var body2 = $"Intrinsics.{m.Name}({args2.JoinStringsWithComma()})";
                        pTypes[0] = altType;
                        WriteFunction(cb2, retType, m.Name, body2, pNames, pTypes);
                    }
                }
            }

            [Test, Explicit]
            public static void GenerateIntrinsicsFile()
            {
                var cb = new CodeBuilder();
                cb.WriteLine("// This file is auto-generated");
                cb.WriteLine("using System;");
                cb.WriteLine("using System.Runtime.CompilerServices;");
                cb.WriteLine("using System.Runtime.InteropServices;");
                cb.WriteLine("using System.Runtime.Intrinsics;");
                cb.WriteLine("using static System.Runtime.CompilerServices.MethodImplOptions;");
                cb.WriteLine();
                cb.WriteLine("namespace Plato");
                cb.WriteStartBlock();
                cb.WriteLine("public static class Intrinsics");
                cb.WriteStartBlock();

                var cb2 = new CodeBuilder();
                cb2.WriteLine("// This file is auto-generated");
                cb2.WriteLine("using System;");
                cb2.WriteLine("using System.Runtime.CompilerServices;");
                cb2.WriteLine("using System.Runtime.InteropServices;");
                cb2.WriteLine("using System.Runtime.Intrinsics;");
                cb2.WriteLine("using static System.Runtime.CompilerServices.MethodImplOptions;");
                cb2.WriteLine("using static Plato.Intrinsics;");
                cb2.WriteLine();
                cb2.WriteLine("namespace Plato");
                cb2.WriteStartBlock();
                cb2.WriteLine("public static class SystemExtensions");
                cb2.WriteStartBlock();

                var cb3= new CodeBuilder();
                cb3.WriteLine("// This file is auto-generated");
                cb3.WriteLine("library GeneratedInstrinsics");
                cb3.WriteStartBlock();

                foreach (var kv in Types)
                {
                    WriteTypeFunctions(cb, cb2, cb3, kv.Key, kv.Value);
                }

                cb.WriteEndBlock();
                cb.WriteEndBlock();

                cb2.WriteEndBlock();
                cb2.WriteEndBlock();

                cb3.WriteEndBlock();

                var f = Dir.RelativeFile("g.Intrinsics.cs");
                f.WriteAllText(cb.ToString());
                Console.WriteLine($"Wrote text to {f}");

                var f2 = Dir.RelativeFile("g.SystemExtensions.cs");
                f2.WriteAllText(cb2.ToString());
                Console.WriteLine($"Wrote text to {f2}");

                var f3 = Dir.RelativeFile("g.intrinsics.plato");
                f3.WriteAllText(cb3.ToString());
                Console.WriteLine($"Wrote text to {f3}");
            }
        }
    }
}
