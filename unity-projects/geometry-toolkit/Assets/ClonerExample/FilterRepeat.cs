using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class FilterRepeat : FilterComponent<object, List<object>>
    {
        public int Count = 3;

        public override List<object> EvalImpl(object input)
        {
            return Enumerable.Repeat(input, Count).ToList();
        }
    }
}