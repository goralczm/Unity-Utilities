using UnityEngine;

namespace Utilities.BehaviourTree.Demo
{
    public class WaitStrategy : IStrategy
    {
        public readonly float delay;

        private float startTime;
        private bool isInitialized = false;

        public WaitStrategy(float delay)
        {
            this.delay = delay;
        }

        public Node.Status Process()
        {
            if (!isInitialized)
            {
                startTime = Time.time;
                isInitialized = true;
            }

            if (Time.time >= startTime + delay)
                return Node.Status.Success;

            return Node.Status.Running;
        }

        public void Reset()
        {
            isInitialized = false;
        }
    }
}
