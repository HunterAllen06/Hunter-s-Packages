using System;

namespace HunterAllen.StateMachine
{
    /// <summary>
    /// Contains a boolean lambda function that can be evaluated.
    /// </summary>
    public class Condition : ICondition
    {
        readonly Func<bool> _condition;
        
        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }
        
        public bool EvaluateCondition()
        {
            return _condition.Invoke();
        }
    }
}