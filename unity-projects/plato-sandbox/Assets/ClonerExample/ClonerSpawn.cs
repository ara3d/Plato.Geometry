using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    public enum DistributionType
    {
        Equal,
        Linear,
        Power2,
        Power3,
    }

    [Serializable]
    public struct Range<T>
    {
        public Range(T center, T size, DistributionType dist = DistributionType.Equal)
        {
            Center = center;
            Size = size;
            Distribution = dist;
        }

        public DistributionType Distribution; 
        public T Center;
        public T Size;
    }


    [ExecuteAlways]
    public class ClonerSpawn : ClonerJobComponent, ICloneJob
    {
        public CloneData _cloneData;
        public CloneData _previousCloneData;
        public Range<float3> Positions = new (Vector3.zero, Vector3.one * 5);
        public Range<float3> Velocities = new (Vector3.one, Vector3.one / 2);
        public Range<float3> Rotations = new(Vector3.zero, Vector3.one / 10);
        public float Lifetime = 5;
        public ulong Seed = 367;
        public int ParticlesPerSecond = 300;
        public JobHandle Handle { get; set; }
        public JobHandle ReclaimHandle { get; set; }
        public bool Reset;
        public NativeArray<int> LiveParticleCount;

        public float timeSinceLastSecond;
        public int particleSpawnCount;
        public bool deletePreviousData;
        public int oldSize;
        public int newSize;
        public int expectedParticles;
        public int liveParticleCount;
        public int particlesCreated;
        public int LastSecond;

        public JobHandle Schedule(ICloneJob previous)
        {
            if (!LiveParticleCount.IsCreated)
                LiveParticleCount = new NativeArray<int>(1, Allocator.Persistent);

            timeSinceLastSecond = Time.time - LastSecond;
            expectedParticles = (int)(timeSinceLastSecond * ParticlesPerSecond);
            particleSpawnCount = expectedParticles - particlesCreated;
            if (particleSpawnCount < 0)
                particleSpawnCount = 0;
            Seed += (ulong)(particleSpawnCount * 10);

            if (Math.Truncate(Time.time) > LastSecond)
            {
                LastSecond = (int)Math.Truncate(Time.time);
                particlesCreated = 0;
            }
            else
            {
                particlesCreated += particleSpawnCount;
            }

            oldSize = _cloneData.Count;
            liveParticleCount = LiveParticleCount[0];
            newSize = LiveParticleCount[0] + particleSpawnCount;

            Handle = previous?.Handle ?? default;

            if (deletePreviousData)
            {
                deletePreviousData = false;
                if (_previousCloneData.IsValid)
                    _previousCloneData.Dispose();
            }

            if (newSize > oldSize)
            {
                // The previous state is the current _cloneData. 
                _previousCloneData = _cloneData;
                _cloneData = new CloneData();
                _cloneData.Resize(newSize);

                if (oldSize > 0)
                {
                    var jobCopy = new JobCopy(_previousCloneData, _cloneData, 0, 0);
                    Handle = jobCopy.Schedule(oldSize, 256, Handle);
                    deletePreviousData = true;
                }
            }

            // Default CPU and GPU data 
            var cpuInst = new CpuInstanceData(Time.time);
            var gpuInst = new GpuInstanceData
            {
                Color = new float4(0.5f, 0.5f, 1, 1),
                Id = 0,
                Metallic = 0.5f,
                Orientation = quaternion.identity,
                Pos = float3.zero,
                Scl = new float3(1, 1, 1),
                Smoothness = 0.5f,
            };

            if (newSize > _cloneData.Count)
            {
                throw new Exception($"{newSize} > {_cloneData.Count}");
            }
                
            if (newSize != LiveParticleCount[0] + particleSpawnCount)
            {
                throw new Exception($"{newSize} != {LiveParticleCount[0]} + {particleSpawnCount}");
            }

            // Create the new particles, starting at the LiveParticleCount. 
            if (particleSpawnCount > 0)
            {
                var jobSpawn = new JobSpawn(_cloneData,
                    Positions,
                    Velocities,
                    Rotations,
                    (ulong)Seed,
                    cpuInst,
                    gpuInst,
                    LiveParticleCount[0]);

                Handle = jobSpawn.Schedule(particleSpawnCount, 256, Handle);
            }

            LiveParticleCount[0] = newSize;
            
            var jobUpdate = new JobUpdate(_cloneData, Time.time);
            Handle = jobUpdate.Schedule(_cloneData.Count, 256, Handle);

            // Gather any particles to be reclaimed, moving them to the back of the list. 
            if (_cloneData.Count > 0)
            {
                var jobGatherReclaim = new JobGatherReclaimed(_cloneData, Time.time, Lifetime, LiveParticleCount);
                Handle = jobGatherReclaim.Schedule(Handle);
            }

            return Handle;
        }
        
        public ref CloneData CloneData => ref _cloneData;
        public int Count => liveParticleCount;

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            throw new NotSupportedException();
        }

        public void Update()
        {
            if (Reset)
            {
                _previousCloneData.Resize(0);
                _cloneData.Resize(0);
                Reset = false;
                LiveParticleCount[0] = 0;
            }
        }
    }

    // Moves all expired particles to the end of the list, and decrements the "lastIndex"
    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = true, DisableSafetyChecks = false)]
    public struct JobGatherReclaimed : IJob
    {
        public JobGatherReclaimed(CloneData data, float currentTime, float maxAge, NativeArray<int> liveParticleCount)
        {
            Data = data;
            CurrentTime = currentTime;
            MaxAge = maxAge;
            LiveParticleCount = liveParticleCount;      
        }

        private CloneData Data;
        private readonly float CurrentTime;
        private readonly float MaxAge;
        private NativeArray<int> LiveParticleCount;

        public void Execute()
        {
            for (var i = 0; i < LiveParticleCount[0]; i++)
            {
                if (Data.Expired(i, CurrentTime, MaxAge))
                {
                    // Move the last pointer backwards if it is also expired. 
                    while (LiveParticleCount[0] > i && Data.Expired(LiveParticleCount[0] - 1, CurrentTime, MaxAge))
                    {
                        LiveParticleCount[0] -= 1;
                    }

                    Data.Swap(i, LiveParticleCount[0] - 1);
                    LiveParticleCount[0] -= 1;
                }
            }
        }
    }

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = true, DisableSafetyChecks = false)]
    public struct JobSetData : IJobParallelFor
    {
        private CloneData Data;
        private CpuInstanceData CpuInst;
        private GpuInstanceData GpuInst;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JobSetData(in CloneData cloneData, in CpuInstanceData cpuInst, in GpuInstanceData gpuInst)
        {
            Data = cloneData;
            CpuInst = cpuInst;
            GpuInst = gpuInst;
        }

        [SkipLocalsInit, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int i)
        {
            Data.GpuInstance(i) = GpuInst;
            Data.CpuInstance(i) = CpuInst;
        }
    }

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = false, DisableSafetyChecks = true)]
    public struct JobCopy : IJobParallelFor
    {
        [ReadOnly]
        private readonly CloneData OldData;
        
        private CloneData NewData;
        
        private int OffsetSource;
        private int OffsetDest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JobCopy(in CloneData oldData, in CloneData newData, int offsetSource, int offsetDest)
        {
            OldData = oldData;
            NewData = newData;
            OffsetSource = offsetSource;
            OffsetDest = offsetDest;
        }

        [SkipLocalsInit, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int i)
        {
            NewData.GpuInstance(i + OffsetDest) = OldData.GpuInstance(i + OffsetSource);
            NewData.CpuInstance(i + OffsetDest) = OldData.CpuInstance(i + OffsetSource);
        }
    }

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = true, DisableSafetyChecks = false)]
    public struct JobSpawn : IJobParallelFor
    {
        private CloneData CloneData;
        private readonly Range<float3> PositionRange;
        private readonly Range<float3> VelocityRange;
        private readonly Range<float3> RotationRange;
        private readonly ulong Seed;
        private readonly GpuInstanceData GpuInst;
        private readonly CpuInstanceData CpuInst;
        private readonly int Offset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JobSpawn(in CloneData cloneData, in Range<float3> posRange, in Range<float3> velRange, in Range<float3> rotRange, ulong seed,             
            in CpuInstanceData cpuInst, in GpuInstanceData gpuInst, int offset)
        {
            CloneData = cloneData;
            PositionRange = posRange;
            VelocityRange = velRange;
            RotationRange = rotRange;
            Seed = seed;
            GpuInst = gpuInst;
            CpuInst = cpuInst;
            Offset = offset;
        }

        [SkipLocalsInit, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int i)
        {
            i += Offset;
            var pos = GetValue(i, Seed, PositionRange);
            var vel = GetValue(i, Seed + 444, VelocityRange);
            var rot = quaternion.EulerXYZ(GetValue(i, Seed + 888, RotationRange));
            CloneData.CpuInstance(i) = CpuInst;
            CloneData.GpuInstance(i) = GpuInst;
            CloneData.CpuInstance(i).Velocity = vel;
            CloneData.GpuInstance(i).Pos = pos;
            CloneData.GpuInstance(i).Orientation = rot;
        }

        [SkipLocalsInit, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 GetValue(int i, ulong seed, in Range<float3> range)
        {
            var amount = Rng.GetNthFloat3(seed, (ulong)i);

            if (range.Distribution == DistributionType.Equal)
            {
                return range.Lerp(amount);
            }

            // Scale from -1 to +1
            amount = amount * 2 -  new float3(1,1,1);
            if (range.Distribution == DistributionType.Power2)
                return math.lerp(range.Center, range.GetMax(), amount * amount * math.sign(amount));
            else if (range.Distribution == DistributionType.Power3)
                return math.lerp(range.Center, range.GetMax(), amount * amount * amount);
            
            return math.lerp(range.Center, range.GetMax(), amount);
        }
    }

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = false, DisableSafetyChecks = true)]
    public struct JobIndirect<T> : IJobParallelForDefer where T: struct, IJobParallelFor
    {
        private T _job;
        private NativeArray<int> _indices;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JobIndirect(in T job, NativeList<int> indices)
        {
            _job = job;
            _indices = indices.AsDeferredJobArray();
        }

        [SkipLocalsInit, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int i)
        {
            _job.Execute(_indices[i]);
        }
    }

    [BurstCompile(CompileSynchronously = true, FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance, Debug = true, DisableSafetyChecks = false)]
    public struct JobSlice<T> : IJobParallelFor where T : struct, IJobParallelFor
    {
        private T _job;
        private int _offset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JobSlice(in T job, int offset)
        {
            _job = job;
            _offset = offset;
        }

        [SkipLocalsInit, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int i)
        {
            _job.Execute(i + _offset);
        }
    }

    public static class CloneDataJobExtensions
    {
        public static JobHandle ScheduleResize(this ref CloneData dest, ref CloneData previous, int newSize,
            in CpuInstanceData cpuInst, in GpuInstanceData gpuInst, JobHandle previousJob = default)
        {
            dest.Resize(newSize);
            var oldSize = previous.Count;
            var minSize = Math.Min(newSize, oldSize);
            if (minSize > 0)
            {
                var jobCopy = new JobCopy(previous, dest, 0, 0);
                previousJob = jobCopy.Schedule(minSize, 256, previousJob);
            }

            if (newSize > oldSize)
            {
                var jobSlice = new JobSlice<JobSetData>(new JobSetData(dest, cpuInst, gpuInst), oldSize);
                previousJob = jobSlice.Schedule(newSize - oldSize, 256, previousJob);
            }

            return previousJob;
        }
    }

    public static class RangeExtensions
    {
        public static float3 GetMin(this in Range<float3> range)
            => range.Center - range.GetHalfSize();

        public static float3 GetMax(this in Range<float3> range)
            => range.Center + range.GetHalfSize();

        public static float3 GetHalfSize(this in Range<float3> range)
            => range.Size / 2f;

        public static float3 Lerp(this in Range<float3> range, in float3 amount)
            => math.lerp(range.GetMin(), range.GetMax(), amount);
    }
}