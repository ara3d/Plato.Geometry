using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerBehavior : ClonerJobComponent
    {
        public float MinSpeed;
        public float MaxSpeed;
        public Vector3 Forward = new Vector3(1,0,0);
        public float MinGoalTime;
        public float MaxGoalTime;
        public ulong Seed = 441;

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            return (previousData, new JobUpdateGoals(
                previousData,
                Seed,
                Time.time,
                MinSpeed,
                MaxSpeed,
                Forward,
                MinGoalTime,
                MaxGoalTime)
                .Schedule(previousData.Count, batchSize, previousHandle));
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    public struct JobUpdateGoals : IJobParallelFor
    {
        private CloneData Data;
        private readonly ulong Seed;
        private readonly float MinSpeed;
        private readonly float MaxSpeed;
        private readonly float3 Forward;
        private readonly float MinGoalTime;
        private readonly float MaxGoalTime;
        private readonly float CurrentTime;

        public JobUpdateGoals(CloneData data, ulong seed, float currentTime, float minSpeed, float maxSpeed, float3 forward, float minGoalTime,
            float maxGoalTime)
        {
            Data = data;
            Seed = seed;
            CurrentTime = currentTime;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            Forward = forward;
            MinGoalTime = minGoalTime;
            MaxGoalTime = maxGoalTime;
        }

        public void Execute(int index)
        {
            //var prob = (ProbabilityOfChange / 100) * DeltaTime;
            //var changeGoal = Rng.GetNthFloat(Seed, (ulong)index) < prob;

            var cpu = Data.CpuInstance(index);
            var newSeed = Seed + (ulong)(CurrentTime * 10000);
            if (!cpu.SeekGoal)
            {
                var goal = new GoalState(
                    cpu.LastUpdateTime + Rng.GetNthFloat(newSeed + 1, (ulong)index, MinGoalTime, MaxGoalTime),
                    Rng.GetNthQuaternion(newSeed + 3, (ulong)index),
                    Rng.GetNthFloat(newSeed + 2, (ulong)index, MinSpeed, MaxSpeed) * Forward
                );
                Data.CpuInstance(index).SetGoal(CurrentTime, goal, ref Data.GpuInstance(index));
            }
        }
    }
}