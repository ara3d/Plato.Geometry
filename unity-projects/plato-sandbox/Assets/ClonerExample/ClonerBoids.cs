using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    // https://www.red3d.com/cwr/boids/
    // https://swharden.com/csdv/simulations/boids/
    [ExecuteAlways]
    public class ClonerBoids : ClonerJobComponent
    {
        public float CoherenceStrength;
        public float AlignmentStrength;
        public float AvoidanceStrength;
        public float NeighbourhoodDistance;
        public float AvoidanceDistance;
        public float Speed;

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            return (previousData, new JobUpdateBoids() { 
                    CurrentTime = Time.time,
                    Data = previousData,
                    CoherenceStrength = CoherenceStrength,
                    AlignmentStrength = AlignmentStrength,
                    AvoidanceStrength = AvoidanceStrength,
                    Distance2 = NeighbourhoodDistance * NeighbourhoodDistance,
                    AvoidanceDistance2 = AvoidanceDistance * AvoidanceDistance,
                    Speed = Speed,
                }
                .Schedule(previousData.Count, batchSize, previousHandle));
        }
    }


    [BurstCompile(CompileSynchronously = true)]
    public struct JobUpdateBoids : IJobParallelFor
    {
        public CloneData Data;
        public float CurrentTime;
        public float CoherenceStrength;
        public float AlignmentStrength;
        public float AvoidanceStrength;
        public float Distance2;
        public float AvoidanceDistance2;
        public float Speed;

        public void Execute(int index)
        {
            var pos0 = Data.GpuArray[index].Pos;
            var goalOrientation = Data.GpuArray[index].Orientation;
            var cnt = 0;
            var sumPropulsion = float3.zero;
            var sumPosition = float3.zero;
            var closestPos = float3.zero;
            var minDist = AvoidanceDistance2;
            var tooClose = false;

            for (var i = 0; i < Data.Count; i++)
            {
                var pos1 = Data.GpuInstance(i).Pos;
                
                var dist = math.distancesq(pos1, pos0);
                if (dist < Distance2)
                {
                    cnt++;
                    sumPosition += pos1;
                    sumPropulsion += Data.CpuInstance(i).Propulsion;

                    if (dist < minDist)
                    {
                        minDist = dist;
                        closestPos = pos1;
                        tooClose = true;
                    }
                }
            }

            var avgPropulsion = sumPropulsion / cnt;
            var center = sumPosition / cnt;

            var avgOrientation = quaternion.identity;
            if (AlignmentStrength != 0)
            {
                for (var i = 0; i < Data.Count; i++)
                {
                    var pos1 = Data.GpuInstance(i).Pos;

                    var dist = math.distancesq(pos1, pos0);
                    if (dist < Distance2)
                    {
                        var orient = Data.GpuInstance(i).Orientation;
                        avgOrientation = math.mul(avgOrientation, math.slerp(quaternion.identity, orient, AlignmentStrength / cnt));
                    }
                }
            }

            if (cnt > 0)
            {
                var orientatonTowardsCenter = quaternion.LookRotation(center, new float3(0, 1, 0));
                goalOrientation = math.slerp(goalOrientation, orientatonTowardsCenter, CoherenceStrength);
            }

            if (cnt > 0)
            {
                goalOrientation = math.slerp(goalOrientation, avgOrientation, AlignmentStrength);
            }

            var goalPropulsion = Data.CpuInstance(index).Propulsion;    
            if (cnt > 0)
            {
                goalPropulsion = math.lerp(goalPropulsion, avgPropulsion, AlignmentStrength);
            }

            if (tooClose)
            {
                var orientationAwayFromOthers = quaternion.LookRotation(closestPos, new float3(0, 1, 0));
                goalOrientation = math.slerp(goalOrientation, orientationAwayFromOthers, AvoidanceStrength);
            }

            var state = new GoalState(CurrentTime + Speed, goalOrientation, goalPropulsion);
            Data.CpuInstance(index).SetGoal(CurrentTime, state, ref Data.GpuInstance(index));
        }
    }
}