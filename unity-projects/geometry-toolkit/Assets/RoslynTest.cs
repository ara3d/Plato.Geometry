using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

using Ara3D.Bowerbird.Unity2022;
using Ara3D.Bowerbird.Interfaces;
using Ara3D.Utils.Roslyn;
using Microsoft.CodeAnalysis.Emit;

public class RoslynTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var s = code;

        /*
        var filePaths = new string[]
        {
            typeof(Mesh).Assembly.Location, 
            typeof(ValueType).Assembly.Location,
            typeof(RoslynTest).Assembly.Location,
            typeof(object).Assembly.Location,
            typeof(System.String).Assembly.Location,
            @"C:\Program Files\Unity\Hub\Editor\2022.3.24f1\Editor\Data\Tools\netcorerun\netstandard.dll",
            @"C:\Program Files\Unity\Hub\Editor\2022.3.24f1\Editor\Data\NetCoreRuntime\shared\Microsoft.NETCore.App\6.0.21\System.Runtime.dll",
            @"C:\Program Files\Unity\Hub\Editor\2022.3.24f1\Editor\Data\NetCoreRuntime\shared\Microsoft.NETCore.App\6.0.21\System.Private.CoreLib.dll",
        };
        */
        var filePaths = RoslynUtils.LoadedAssemblyLocations().Select(fp => fp.Value);
        var output = BowerbirdSample.CompileScript(s, filePaths);
        Debug.Log($"Output result = {output.EmitResult}, output file = {output.OutputFile}");
        foreach (var d in output.Diagnostics)
            Debug.Log($"{d}");
        if (output.EmitResult.Success)
        {
            var asm = Assembly.LoadFile(output.OutputFile);
            var type = asm.GetType("Ara3D.Bowerbird.Unity.CreateBox");
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod("Execute");
            method.Invoke(instance, Array.Empty<object>());
        }
    }

    public class CreateBox 
    {
        public string Name => "Create Box";

        public Vector3 boxSize = new Vector3(5, 5, 5);

        public void Execute()
        {
            // Create a new GameObject named ""Box""
            var box = new GameObject("Box");

            // Add a MeshFilter component to hold the box mesh
            var meshFilter = box.AddComponent<MeshFilter>();
            meshFilter.mesh = CreateBoxMesh(boxSize);

            // Add a MeshRenderer component to display the box
            MeshRenderer meshRenderer = box.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Standard"));

            // Optional: Set the position of the box
            box.transform.position = new Vector3(0, 0.5f * boxSize.y, 0);
        }

        public static Mesh CreateBoxMesh(Vector3 size)
        {
            // Create a new mesh
            var mesh = new Mesh();

            // Define vertices of the box
            Vector3[] vertices = {
                // Bottom face
                new Vector3(-size.x / 2, -size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, -size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, -size.y / 2, size.z / 2),
                new Vector3(-size.x / 2, -size.y / 2, size.z / 2),

                // Top face
                new Vector3(-size.x / 2, size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, size.y / 2, size.z / 2),
                new Vector3(-size.x / 2, size.y / 2, size.z / 2)
            };

            // Define triangles of the box
            int[] triangles = {
                // Bottom face
                0, 2, 1,
                0, 3, 2,

                // Top face
                4, 5, 6,
                4, 6, 7,

                // Front face
                0, 1, 5,
                0, 5, 4,

                // Back face
                2, 3, 7,
                2, 7, 6,

                // Left face
                0, 4, 7,
                0, 7, 3,

                // Right face
                1, 2, 6,
                1, 6, 5
            };

            // Assign vertices and triangles to the mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles;

            // Recalculate normals for proper lighting
            mesh.RecalculateNormals();

            return mesh;
        }
    }

    private static string code = @"using UnityEngine;
using System;

namespace Ara3D.Bowerbird.Unity
{
   public class CreateBox 
   {
       public string Name => ""Create Box"";

       public Vector3 boxSize = new Vector3(5, 5, 5);

       public void Execute()
       {
           // Create a new GameObject named """"Box""""
           var box = new GameObject(""Box"");

           // Add a MeshFilter component to hold the box mesh
           var meshFilter = box.AddComponent<MeshFilter>();
           meshFilter.mesh = CreateBoxMesh(boxSize);

           // Add a MeshRenderer component to display the box
           MeshRenderer meshRenderer = box.AddComponent<MeshRenderer>();
           meshRenderer.material = new Material(Shader.Find(""Standard""));

           // Optional: Set the position of the box
           box.transform.position = new Vector3(0, 0.5f * boxSize.y, 0);
       }

       public static Mesh CreateBoxMesh(Vector3 size)
       {
           // Create a new mesh
           var mesh = new Mesh();

           // Define vertices of the box
           Vector3[] vertices = {
               // Bottom face
               new Vector3(-size.x / 2, -size.y / 2, -size.z / 2),
               new Vector3(size.x / 2, -size.y / 2, -size.z / 2),
               new Vector3(size.x / 2, -size.y / 2, size.z / 2),
               new Vector3(-size.x / 2, -size.y / 2, size.z / 2),

               // Top face
               new Vector3(-size.x / 2, size.y / 2, -size.z / 2),
               new Vector3(size.x / 2, size.y / 2, -size.z / 2),
               new Vector3(size.x / 2, size.y / 2, size.z / 2),
               new Vector3(-size.x / 2, size.y / 2, size.z / 2)
           };

           // Define triangles of the box
           int[] triangles = {
               // Bottom face
               0, 2, 1,
               0, 3, 2,

               // Top face
               4, 5, 6,
               4, 6, 7,

               // Front face
               0, 1, 5,
               0, 5, 4,

               // Back face
               2, 3, 7,
               2, 7, 6,

               // Left face
               0, 4, 7,
               0, 7, 3,

               // Right face
               1, 2, 6,
               1, 6, 5
           };

           // Assign vertices and triangles to the mesh
           mesh.vertices = vertices;
           mesh.triangles = triangles;

           // Recalculate normals for proper lighting
           mesh.RecalculateNormals();

           return mesh;
       }
   }
}";
}
