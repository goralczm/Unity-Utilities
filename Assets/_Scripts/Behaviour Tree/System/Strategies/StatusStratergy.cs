using System;

namespace Utilities.BehaviourTree
{
    public class StatusStrategy : IStrategy
    {
        readonly Node.Status status;

        public StatusStrategy(Node.Status status)
        {
            this.status = status;
        }

        public Node.Status Process()
        {
            return status;
        }
    }
}