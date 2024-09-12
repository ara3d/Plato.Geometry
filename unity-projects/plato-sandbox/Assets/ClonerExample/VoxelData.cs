using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    public struct Voxel<T>
    {
        public float3x2 Bounds;
        public T Value;

        public float3 Position
            => (Bounds.c0 + Bounds.c1) / 2;

        public Voxel(float3x2 bounds, T value)
        {
            Bounds = bounds;
            Value = value;
        }

        public override string ToString()
            => $"Bounds = {Bounds}, Center = {Bounds.Center()}, Size = {Bounds.Size()} Value = {Value}";
    }

    public struct VoxelData<T> where T : unmanaged  
    {
        [NativeDisableParallelForRestriction] public NativeArray<T> Data;

        public int VoxelCount;
        public int GridWidth;
        public int GridHeight;
        public int RowColumnCount;
        public int GridDepth;
        
        public float3 Min;
        public float3 Max;
        public int3 GridDim;
        public float3 TotalSize;
        public float3 VoxelSize;

        // Returns true if data is now invalid, and has to be recomputed
        public bool Update(Bounds bounds, int gridResolution)
            => Update(bounds.min, bounds.max, new int3(gridResolution, gridResolution, gridResolution));

        // Returns true if data is now invalid, and has to be recomputed
        public bool Update(float3 min, float3 max, int3 gridDim)
        {
            GridDim = gridDim;
            GridWidth = GridDim.x;
            GridHeight = GridDim.y;
            GridDepth = GridDim.z;
            RowColumnCount = GridWidth * GridDim.y;
            VoxelCount = RowColumnCount * GridDim.z;
            Min = min;
            Max = max;
            TotalSize = max - min;
            VoxelSize = math.float3(TotalSize) / math.float3(GridDim);
            if (Data.IsCreated && Data.Length == VoxelCount)
                return false;
            Data.SafeDispose();
            Data = new NativeArray<T>(VoxelCount, Allocator.Persistent);
            return true;
        }

        public void Clear()
            => Data.Clear();

        public void SafeDispose()
            => Data.SafeDispose();

        public float3 GetRelPos(float3 pos)
            => math.unlerp(Min, Max, pos);

        public float3 GetRelPos(int3 index)
            => math.unlerp(0, math.float3(GridDim), math.float3(index));

        public float3x2 GetBounds(int i)
            => GetBounds(ToIndex3(i));

        public float3 GetCenter(int index)
            => GetCenter(ToIndex3(index));

        public float3 GetCenter(int3 i)
            => Min + math.float3(VoxelSize * math.float3(i) + VoxelSize / 2);

        public float3x2 GetBounds(int3 i)
            => math.float3x2(Min + VoxelSize * math.float3(i), Min + VoxelSize * math.float3(i + 1));

        public int3 ToIndex3(int index)
            => new(index % GridWidth, (index / GridWidth) % GridHeight, index / RowColumnCount);

        public int ToIndex(int3 index)
            => index.x + index.y * GridDim.x + index.z * GridDim.x * GridDim.y;

        public int3 ToIndex3(float3 pos)
            => math.int3(GetRelPos(pos) * math.float3(GridDim));

        public bool InBounds(int3 index)
            => (index.x >= 0 && index.x < GridDim.x)
               && (index.y >= 0 && index.y < GridDim.y)
               && (index.z >= 0 && index.z < GridDim.z);

        public void SetValue(int3 index3, T value)
        {
            if (InBounds(index3)) 
                SetValue(ToIndex(index3), value);
        }

        public void SetValue(int index, T value)
            => Data[index] = value;

        public T GetValue(int3 index3)
        {
            if (!InBounds(index3)) return default;
            return GetValue(ToIndex(index3));
        }

        public T GetValue(int index)
            => Data[index];

        public Voxel<T> GetVoxel(int index)
            => new(GetBounds(index), GetValue(index));

        public Voxel<T> GetVoxel(int3 index)
            => new(GetBounds(index), GetValue(index));

        public Voxel<T> GetVoxel(int i, int j, int k)
            => GetVoxel(math.int3(i, j, k));

        public Voxel<T> GetVoxel(float3 pos)
            => GetVoxel(ToIndex3(pos));

        public bool IsValid
            => Data.IsCreated;

        public void FromSDF(Func<float3, T> f)
        {
            for (var i = 0; i < Data.Length; i++)
                Data[i] = f(GetCenter(i));
        }

        public void FromBoundsSDF(Func<float3x2, T> f)
        {
            for (var i = 0; i < Data.Length; i++)
                Data[i] = f(GetBounds(i));
        }

        public int VoxelizePoint(float3 f, T value)
        {
            var index3 = ToIndex3(f);
            if (!InBounds(index3)) return -1;
            var index = ToIndex(index3);
            SetValue(index, value);
            return index;
        }
        
        public void VoxelizeLine(float3x2 line, T value)
        {
            var a = VoxelizePoint(line.c0, value);
            var b = VoxelizePoint(line.c1, value);
            if (a == b) return;
            VoxelizeLine(line, a, b, value);
        }

        public void VoxelizeLine(float3 lineA, float3 lineB, int a, int b, T value)
            => VoxelizeLine(math.float3x2(lineA, lineB), a, b, value);

        public void VoxelizeLine(float3x2 line, int a, int b, T value)
        {
            if (a == b)
                return;
            var mid = line.Center();
            var c = VoxelizePoint(mid, value);
            if (a == c || b == c)
                return;
            VoxelizeLine(math.float3x2(line.c0, mid), a, c, value);
            VoxelizeLine(math.float3x2(mid, line.c1), c, b, value);
        }

        public void VoxelizeTriangle(float3x3 tri, T value)
        {
            var a = VoxelizePoint(tri.c0, value);
            var b = VoxelizePoint(tri.c1, value);
            var c = VoxelizePoint(tri.c2, value);
            VoxelizeTriangle(tri, a, b, c, value);
        }

        // NOTE: we could use this to do an even faster voxelization of meshes, by assigning voxel Indices to the points first.
        // once a point is assigned a voxel, we can use those to prevent vertices from being looked up multiple times.

        public void VoxelizeTriangle(float3x3 tri, int a, int b, int c, T value)
        {
            if (a == b && b == c)
                return;
            var mid = tri.Center();
            var d = VoxelizePoint(mid, value);
            if (a == d || b == d || c == d)
                return;

            var midAB = (tri.c0 + tri.c1) / 2;
            var midBC = (tri.c1 + tri.c2) / 2;
            var midCA = (tri.c2 + tri.c0) / 2;
            {
                var ab = VoxelizePoint(midAB, value);
                var bc = VoxelizePoint(midBC, value);
                var ca = VoxelizePoint(midCA, value);
                VoxelizeTriangle(math.float3x3(tri.c0, midAB, midCA), a, ab, ca, value);
                VoxelizeTriangle(math.float3x3(tri.c1, midBC, midAB), b, bc, ab, value);
                VoxelizeTriangle(math.float3x3(tri.c2, midCA, midBC), c, ca, bc, value);
                VoxelizeTriangle(math.float3x3(midAB, midBC, midCA), ab, bc, ca, value);
            }
        }
    }

    /*
     * [AlwaysUpdateSystem]
public class SystemWithDelegate : JobComponentSystem {
 
  public delegate int MyDelegate(int arg1, int arg2);
 
  int Increment(int arg1, int arg2) {
    return arg1 + arg2;
  }
 
  NativeArray<int> Values;
  NativeArray<int> Result;
 
  protected override void OnCreate() {
    base.OnCreate();
    Values = new NativeArray<int>(Enumerable.Range(0, 1000).ToArray(), Allocator.Persistent);
    Result = new NativeArray<int>(Values.Length, Allocator.Persistent);
  }
 
  protected override void OnDestroy() {
    base.OnDestroy();
    Values.SafeDispose();
    Result.SafeDispose();
  }
 
  [BurstCompile]
  struct JobWithDelegate : IJobParallelFor {
    public FunctionPointer<MyDelegate> Function;
    public int IncrementValue;
    [ReadOnly]
    public NativeArray<int> Source;
    [WriteOnly]
    public NativeArray<int> Result;
 
    public void Execute(int index) {
      Result[index] = Function.Invoke(Source[index], IncrementValue);
    }
  }
 
  [BurstCompile]
  public struct CopyToNativeArrayJob<T> : IJobParallelFor where T : struct {
    [ReadOnly]
    public NativeArray<T> Source;
    [WriteOnly]
    public NativeArray<T> Target;
    public void Execute(int index) {
      Target[index] = Source[index];
    }
  }
 
  protected override JobHandle OnUpdate(JobHandle inputDeps) {
    inputDeps = new JobWithDelegate {
      Function = new FunctionPointer<MyDelegate>(Marshal.GetFunctionPointerForDelegate((MyDelegate)Increment)),
      IncrementValue = 2,
      Source = Values,
      Result = Result
    }.Schedule(Values.Length, 64, inputDeps);
    inputDeps = new CopyToNativeArrayJob<int> {
      Source = Result,
      Target = Values
    }.Schedule(Values.Length, 64, inputDeps);
    return inputDeps;
  }
}
     */
}