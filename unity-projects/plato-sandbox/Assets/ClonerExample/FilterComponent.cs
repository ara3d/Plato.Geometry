using System;
using System.Collections.Generic;
using System.Linq;
using Filters;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Assets.ClonerExample
{
    [Serializable]
    public class FallOffParameters
    {
        // TODO: 
        // Distance from point
        // Distance from plane 
        // Distance from lane
    }

    [ExecuteAlways]
    public abstract class BaseFilterComponent : MonoBehaviour, IFilter
    {
        public BaseFilterComponent InputFilter;

        public Type Input { get; }
        public Type Output { get; }

        public BaseFilterComponent(Type input, Type output)
        {
            Input = input;
            Output = output;
        }

        // Present so that it can be disabled. 
        public void Update()
        { }

        public abstract object Eval(object input);

        public object EvalInput()
        {
            if (InputFilter != null)
                return InputFilter.Eval();
            var previous = this.GetPreviousComponent<BaseFilterComponent>();
            return previous != null 
                ? previous.Eval() 
                : null;
        }

       public object Eval()
            =>  !enabled 
                ? EvalInput() 
                : Eval(EvalInput());
            

    }

    public abstract class FilterComponent<TInput, TOutput> : BaseFilterComponent
    {
        protected FilterComponent()
            : base(typeof(TInput), typeof(TOutput))
        { }

        public abstract TOutput EvalImpl(TInput input);

        public override object Eval(object input) 
            => EvalImpl((TInput)input);
    }
}