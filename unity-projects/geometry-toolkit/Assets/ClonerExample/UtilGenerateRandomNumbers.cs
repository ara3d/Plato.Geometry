using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class UtilGenerateRandomNumbers : MonoBehaviour
    {
        public int Count = 5;
        public int Seed = 1;
        public float[] Floats;
        public uint[] Uints;
        public int[] Ints;


        public void Update()
        {

        }

        public void OnValidate()
        {
            Update();
        }
    }
}