using UnityEngine;

namespace Utilities.BehaviourTree
{
    public class UntilFailure : Node
    {
        public UntilFailure(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            if (children[0].Process() == Status.Failure)
            {
                Reset();
                return Status.Failure;
            }

            return Status.Running;
        }
    }
}
