using System;

namespace Utilities.BehaviourTree
{
    public class ConditionStrategy : IStrategy
    {
        readonly Func<bool> predicate;

        public ConditionStrategy(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public Node.Status Process() => predicate() ? Node.Status.Success : Node.Status.Failure;
    }
}