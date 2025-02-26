using System.Collections.Generic;
using System.Linq;
using Utilities.BehaviourTree.Utils;

namespace Utilities.BehaviourTree
{
    public class RandomSelector : PrioritySelector
    {
        protected override List<Node> SortChildren() => children.Shuffle().ToList();

        public RandomSelector(string name, int priority = 0) : base(name, priority) { }
    }
}
