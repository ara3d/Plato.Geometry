using Unity.Mathematics;

namespace Assets.ClonerExample
{
    public struct CpuInstanceData
    {
        public float3 Velocity;
        public float3 Acceleration;
        public float3 Propulsion;
        public uint GroupId;
        public float2 Uv;
        public float Mass;
        public float Selection;
        public float CreateTime;
        public float LastUpdateTime;
        
        public bool SeekGoal;
        public GoalState Initial;
        public GoalState Final;

        public CpuInstanceData(float currentTime)
        {
            CreateTime = currentTime;
            LastUpdateTime = currentTime;
            Velocity = 0;
            Acceleration = 0;
            Propulsion = 0;
            GroupId = 0;
            Uv = 0;
            Mass = 0;
            Selection = 0;
            Initial = new GoalState();
            Final = new GoalState();
            SeekGoal = false;
        }

        public void Update(float currentTime, ref GpuInstanceData gpu)
        {
            var t = currentTime - LastUpdateTime;
            LastUpdateTime = currentTime;
            var forward = math.mul(gpu.Orientation, Propulsion);
            
            gpu.Pos += Velocity * t;
            //Velocity += Acceleration * t;

            gpu.Pos += forward * t;

            if (SeekGoal)
            {
                if (currentTime >= Final.Time)
                {
                    Propulsion = Final.Propulsion;
                    gpu.Orientation = Final.Orientation;
                    SeekGoal = false;
                }
                else if (currentTime <= Initial.Time)
                {
                    Propulsion = Initial.Propulsion;
                    gpu.Orientation = Initial.Orientation;
                }
                else
                {
                    var timeToGoal = Final.Time - Initial.Time;
                    var amount = (currentTime - Initial.Time) / timeToGoal;
                    Propulsion = math.lerp(Initial.Propulsion, Final.Propulsion, amount);
                    gpu.Orientation = math.slerp(Initial.Orientation, Final.Orientation, amount);
                }
            }
        }

        public void SetGoal(float currentTime, GoalState goal, ref GpuInstanceData gpu)
        {
            SeekGoal = true;    
            Initial = new GoalState(currentTime, gpu.Orientation, Propulsion);
            Final = goal;
            //Debug.Log($"Initial {Initial.ToString()}");
            //Debug.Log($"New goal {Final.ToString()}");
        }

        public float GetAge(float currentTime)
        {
            return currentTime - CreateTime;
        }
    }
 }