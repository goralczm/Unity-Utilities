using UnityEngine;

namespace Utilities.BehaviourTree
{
    public class BehaviourTree : Node
    {
        private readonly bool runForever;

        public BehaviourTree(string name, bool runForver = false) : base(name)
        {
            this.runForever = runForver;
        }

        public override Status Process()
        {
            if (runForever && currentChild == children.Count)
                Reset();

            while (currentChild < children.Count)
            {
                Status status = children[currentChild].Process();
                if (status != Status.Success)
                    return status;

                currentChild++;
            }

            return Status.Success;
        }
    }
}
