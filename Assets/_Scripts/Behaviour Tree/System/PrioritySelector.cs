using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.BehaviourTree
{
    public class PrioritySelector : Selector
    {
        private List<Node> sortedChildren;
        private List<Node> SortedChildren => sortedChildren ??= SortChildren();

        protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.priority).ToList();

        public PrioritySelector(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    default:
                        continue;
                }
            }

            Reset();
            return Status.Failure;
        }

        public override void Reset()
        {
            base.Reset();
            sortedChildren = null;
        }
    }
}