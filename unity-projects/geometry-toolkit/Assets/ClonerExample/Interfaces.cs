using System;
using System.Collections.Generic;
using Ara3D.Geometry;
using Unity.Jobs;
using Unity.Profiling;

namespace Assets.ClonerExample
{
    public enum TypeEnum 
    {
        Array,
        Object,

        Mesh, // Vector2, Vector3
        Polygon, // Vector2, Vector3 
        PolyLine, // Vector2, Vector3
        Points, // Vector2, Vector3
        
        Bitmap, 
        Voxels,
        
        Instances, // Vector2, Vector3
        Curve, // Number, Vector2, Vector3, 
        Field, // Vector2, Vector3, Vector4 ||=>|| Number, Vector2, Vector3, Vector4  
        Transforms, // Vector2, Vector3 
        Particles, // Vector2, Vector3  
        
        Number,
        Vector2,
        Vector3,
        Vector4,
        Transform, //  2d, 3d 
        Rotation, // 2d, 3d
        Scale, // 2d, 3d
        Color, 

        Pair,
        Sphere, // 2d, 3d
    }

    public static class POperators
    {
        //== 
        // Generators:

        // Can be used to create a Curve, 2D, 3D, or field 
        public class Noise { }
        

        //==

        // Projects 3D data onto a 2D plane 
        public class Project2D { }



        // Extrudes 2D data linearly into 3D 
        public class Extrude { }
        

        //===
        // Mesh specific operations 
        
        // Extract each line from a mesh 
        public class Lines {}

        // Extract points frrom 
        public class Vertices { }

        //== 
        // Polygons 

        // Converts a polygon into a mesh 
        public class EarClipper { }

        //== 
        // General cnoverters

        // Converts a polygon into a mesh using Ear clipping
        // or 
        public class Triangulate { }

        // Converts 3D data into voxels. Implicitly treats 2D data as 3D.
        public class Voxelize {}

        // Converts a 2D point cloud into a mesh 
        public class Delaunay { }

        // Converts a 2D point cloud into 
        // To-do: support 3D as well.
        public class Voronoi { }

        // Converts a signed distance field into a mesh 
        public class MarchingCubes { }

        //==
        // Generic

        public class Array { }
    }
}