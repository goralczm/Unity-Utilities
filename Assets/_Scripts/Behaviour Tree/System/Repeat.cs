using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Utilities.BehaviourTree
{
    public class Repeat : Node
    {
        private int counter;
        private readonly int times;

        public Repeat(string name, Node node, int times, int priority = 0) : base(name, priority)
        {
            this.times = times;
            AddChild(node);
        }

        public override Status Process()
        {
            if (children[0].Process() == Status.Success)
            {
                counter++;
                base.Reset();
            }

            if (counter == times)
            {
                base.Reset();
                return Status.Success;
            }

            return Status.Running;
        }

        public override void Reset()
        {
            base.Reset();
            counter = 0;
        }
    }
}
