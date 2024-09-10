using Unity.Mathematics;

namespace Assets.ClonerExample
{
    public readonly struct GoalState
    {
        public readonly float Time;
        public readonly quaternion Orientation;
        public readonly float3 Propulsion;

        public GoalState(float time, quaternion orientation, float3 propulsion)
        {
            Time = time;
            Orientation = orientation;
            Propulsion = propulsion;
        }

        public override string ToString()
            => $"Time {Time}, Orientation {Orientation}, Propulsion {Propulsion}";
    }
}