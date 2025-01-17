// Autogenerated file: DO NOT EDIT
// Created on 2025-01-17 3:12:39 PM

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Plato
{
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public partial struct Boolean: IValue<Boolean>, IOrderable<Boolean>, IBoolean<Boolean>
    {
        // Static factory function
        [MethodImpl(AggressiveInlining)] public static Boolean Create() => new Boolean();

        // Object virtual function overrides: Equals, GetHashCode, ToString
        [MethodImpl(AggressiveInlining)] public Boolean Equals(Boolean other) => Value.Equals(other.Value);
        [MethodImpl(AggressiveInlining)] public override bool Equals(object obj) => obj is Boolean other ? Equals(other) : false;
        [MethodImpl(AggressiveInlining)] public override string ToString() => Value.ToString();
        [MethodImpl(AggressiveInlining)] public static Boolean operator==(Boolean a, Boolean b) => a.Equals(b);
        [MethodImpl(AggressiveInlining)] public static Boolean operator!=(Boolean a, Boolean b) => !a.Equals(b);

        // Explicit implementation of interfaces by forwarding properties to fields

        // Implemented concept functions and type functions
        [MethodImpl(AggressiveInlining)]  public IArray<Boolean> Repeat(Integer n){
            var _var308 = this;
            return n.MapRange((i) => _var308);
        }

        [MethodImpl(AggressiveInlining)]  public Boolean LessThan(Boolean b) => this.LessThanOrEquals(b).And(this.NotEquals(b));
        [MethodImpl(AggressiveInlining)]  public Boolean GreaterThan(Boolean b) => b.LessThan(this);
        [MethodImpl(AggressiveInlining)]  public Boolean GreaterThanOrEquals(Boolean b) => b.LessThanOrEquals(this);
        [MethodImpl(AggressiveInlining)]  public Boolean Lesser(Boolean b) => this.LessThanOrEquals(b) ? this : b;
        [MethodImpl(AggressiveInlining)]  public Boolean Greater(Boolean b) => this.GreaterThanOrEquals(b) ? this : b;

        // Unimplemented concept functions
        [MethodImpl(AggressiveInlining)]  public Boolean LessThanOrEquals(Boolean y) => Intrinsics.LessThanOrEquals(this, y);
        [MethodImpl(AggressiveInlining)]  public Boolean And(Boolean b) => Intrinsics.And(this, b);
        [MethodImpl(AggressiveInlining)]  public Boolean Or(Boolean b) => Intrinsics.Or(this, b);
    }
}
