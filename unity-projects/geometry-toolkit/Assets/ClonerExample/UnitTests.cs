using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class UnitTests : MonoBehaviour
    {
        public bool Run;
        public ulong Seed = 123;

        public void Update()
        {
            if (!Run) return;
            Run = false;
            var f1 = Rng.GetNthFloat(Seed, 0);
            var f2 = Rng.GetNthFloat(Seed, 1);
            var f3 = Rng.GetNthFloat(Seed, 2, 5, 10);
            Debug.Log($"{f1}, {f2}, {f3}");
        }
    }
}