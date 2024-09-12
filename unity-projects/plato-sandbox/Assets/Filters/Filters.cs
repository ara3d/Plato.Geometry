using System;
using System.Collections.Generic;
using System.Linq;
using Ara3D.Collections;
using Ara3D.Mathematics;

namespace Filters
{
    /*
     * Input types:
     *
     * * AABox
     * * AABox2D
     * * Sphere 
     * * Vector2
     * * Vector3
     * * Point2D
     * * Point3D
     * * Color
     * * Number
     * * Ray
     * * OrientedPoint (Frame of reference)
     * * PolyLine
     * * Surface
     * * Line
     * * Curve
     * * TriMesh
     * * QuadMesh
     * * Plane 
     * * Transform: Rotation, Translation, Scale. 
     * * Distance field 2D
     * * Distance field 3D
     * * Distance field 4D
     *
     * * 2D Array of anything (pixels)
     * * 3D Array of anything (voxels) 
     * * Array of anything (bins)
     *
     * Attributes:
     * * X, Y, Z, Position, Color, RotationEuler, RotationQuaternion, Selection, Alpha, GroupID, 
     *
     * Mesh Attributes:
     * * Edges, Points, Faces, FaceNormals, VertexNormals, 
     */
    public interface IFilter
    {
        Type Input { get; }
        Type Output { get; }
        object Eval(object input);
    }

    public class FilterObject
    {
        public object MainData { get; }
        public int Count { get; }
        public Type Type { get; }
        public Dictionary<string, object> Properties { get; }
        public FilterObject To2D() => throw new NotImplementedException();
        public FilterObject To3D() => throw new NotImplementedException();
    }

    public class FilterOperation
    {
        public bool UseSelection;
        public bool SelectionBoxFilter;
        public float SelectionBoxFilterAmount;
        public float Strength;
    }

    public static class FilterFunctions
    {
        public static object Triangulate(object input) => input;
        public static object Parallel(object input, float distance) => null;
        public static object Subdivide(object input) => null;
        public static object GridMesh(object input) => null;
        public static bool IsValid(object input) => input != null;

        // This is also a cloner, if segments is more than 1. 
        public static object Transform(object input, Vector3 translation, Quaternion rotation, Vector3 scale, int segments = 1)
            => input;

        // NOTE: this is different in that it is a binary filter ... it needs two inputs 
        public static object Lerp(object min, object max, float amount)
            => null;

        // Array operations 
        public static object ArraySlice(object input, int from, int count, int stride)  => null;
        public static object ArrayChunk(object input, int from, int count, int stride) => null;
        public static object ArrayCombine(object input) => null;
        public static object ArraySplit(object input) => null;

        // 
        public static object Displace(object input, object amount) => null;
        public static object Clamp(object input, object min, object max) => null;
        public static object TestDistance(object input, object target) => null;
        public static object Normalize(object input) => null;
        public static object Revolve(object input, object axis) => null;
        public static object Project(object input, object target) => null;
        
        // Clipboard operationts 
        public static object Delete(object input) => null;
        public static object Swap(object input, object other) => null;
        public static object Cut(object input) => null;
        public static object Copy(object input) => null;
        public static object Replace(object input, object target) => null;
        
        // Grow / Dilate
        public static object Expand(object input) => null;
        
        // Erode / Contract  
        public static object Contract(object input) => null;


        public static object Extrude(object input, Vector3 amount, int segments = 1) => null;

        public static object Sample(object input, int count) => null;
        public static object GetAttributes(object input, string name) => null;
        public static object Cast(object input, Type type) => null;

        public static object Sweep(object input) => null;
        public static object Mirror(object input) => null;
        public static object Connect(object input, object other) => null;
        public static object Extend(object input) => null;
        public static object Smooth(object input) => null;
        public static object Countour(object input) => null;

        public static object ConvexHull(object input) => null;
        public static object Delanauay(object input) => null;
        public static object EarClipping(object input) => null;
        public static object Voronoi(object input) => null;

        // Numerical operations 
        public static object Add(object a, object b) => null;
        public static object Subtract(object a, object b) => null;
        public static object Multiply(object a, object b) => null;
        public static object Divide(object a, object b) => null;
        public static object Modulo(object a, object b) => null;
        public static object Negate(object a) => null;
        public static object Invert(object a) => null;

        // From CSG
        public static object Union(object a, object b) => null;
        public static object Intersection(object a, object b) => null;
        public static object Difference(object a, object b) => null;

        // From VDB (object morphology)
        // https://en.wikipedia.org/wiki/Mathematical_morphology
        public static object MorphologyErode(object a, float amount) => null;
        public static object MorphologyDilate(object a, float amount) => null;
        public static object MorphologyOpen(object a, object b) => null;
        public static object MorphologyClose(object a, object b) => null;

        // Image processing
        // https://en.wikipedia.org/wiki/Digital_image_processing
        public static object Watershed(object a) => null;
        public static object Colorize(object a) => null;
        public static object EdgeDetection(object a) => null;
        public static object Blur(object a) => null;
        public static object Sharpen(object a) => null;
        public static object Noise(object a) => null;
        public static object Denoise(object a) => null;
        public static object GrayScale(object a) => null;

        // Analysis
        public static object Fourier(object a) => null;
        public static object Binning(object a) => null;

        // Affine transformations
        public static object Reflect(object a) => null;
        public static object Scale(object a) => null;
        public static object Rotate(object a) => null;
        public static object Shear(object a) => null;

        // Modeling
        public static object Bevel(object input) => null;
        public static object Chamfer(object input) => null;
        public static object Flatten(object input) => null;

        // Non-constant transofmration
        // TODO: these should be compound operators. 
        public static object Bend(object a) => null;
        public static object Twist(object a) => null;
        public static object Skew(object a) => null;
        public static object Stretch(object a) => null;

        // https://en.wikipedia.org/wiki/Filter_(signal_processing)
        public static object Butterworth(object a) => null;
        public static object Chebyshev(object a, bool type1or2) => null;
        public static object Elliptic(object a) => null;
        public static object BoxFilter(object a) => null; 

        // Like angle and size constraints?
        public static object ConstrainAngles(object a) => null;
        public static object ConstrainSizes(object a) => null;
        public static object ConstrainDistances(object a) => null;

        // Physics
        // TODO

        // Modeling modifiers 
        public static object CapHoles(object a) => null;
        public static object Simplify(object a) => null;
        
        // Conversion operatols
        public static object Voxelize(object a) => null;
        public static object Sample(object a) => null;

        // Simulation? 
        
        public static object Ripple(object a) => null;
        public static object Jiggle(object a) => null;
        public static object Melt(object a) => null;
        public static object Flex(object a) => null;
        public static object BreakConstraint(object a) => null;
        public static object Fracture(object a) => null;
        public static object DetectCollision(object a) => null;
        public static object HighlightSelection(object a) => null;

        // Boids
        public static object Goal(object o) => null;
        public static object Simulate(object o) => null;
        
        // 3ds Max inspired https://help.autodesk.com/view/3DSMAX/2024/ENU/?guid=GUID-DB31F6ED-D543-41F5-80B5-F4FDB6E606BB
        public static object Wrap(object a) => null;
        public static object Squeeze(object a) => null;
        public static object Slice(object a) => null;
        public static object CrossSection(object a) => null;

        // Meta-operations
        public static object OperationBefore(object a) => null;
        public static object OperationChanged(object a) => null; 
        public static object OperationDelta(object a) => null;
        public static object OperationRepeat(object a) => null;

        // Some operations, are going to only operate on attributes
        public static object WithAttribute(object a, float name) => null;
        public static object WithParent(object a) => null;
    }

    public static class FilterExtensions
    {
        public static object EvalFilter(IFilter f, object input, float strength)
        {
            var expectedType = f.Input;
            if (input == null)
                return f.Eval(input);

            var inputType = input.GetType();
            if (expectedType.IsAssignableFrom(inputType))
                return f.Eval(input);

            if (input is List<object> list)
            {
                // Auto-mapping 
                return list
                    .Select((item, i)
                        => EvalFilter(f, item, (float)i / (list.Count - 1)))
                    .ToList();
            }

            if (expectedType == typeof(Vector3))
            {
                var deformer = input.GetType().GetMethod("Deform");
                if (deformer != null)
                {
                    Func<Vector3, Vector3> func
                        = v => v.Lerp((Vector3)f.Eval(v), strength);

                    return deformer.Invoke(input, new object[] { func });
                }
            }

            throw new Exception($"Cannot cast from {inputType} to {expectedType}");
        }

        public static object ConvertFilterOutput(this object input) => null;
    }
}