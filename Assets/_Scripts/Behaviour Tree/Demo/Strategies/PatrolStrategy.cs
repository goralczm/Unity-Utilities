using UnityEngine;

namespace Utilities.BehaviourTree.Demo
{
    public class PatrolStrategy : IStrategy
    {
        private readonly NPC npc;
        private readonly Transform[] patrolPoints;
        private readonly bool random;

        private IStrategy moveStrategy;
        private IStrategy waitStrategy;
        private int currentPoint = -1;

        public PatrolStrategy(NPC npc, Transform[] patrolPoints, bool random = false)
        {
            this.npc = npc;
            this.patrolPoints = patrolPoints;
            this.random = random;
            SetNextPoint();
            waitStrategy = new WaitStrategy(0);
        }

        public Node.Status Process()
        {
            if (waitStrategy.Process() == Node.Status.Running)
                return Node.Status.Running;

            if (moveStrategy.Process() == Node.Status.Success)
            {
                waitStrategy = new WaitStrategy(1f);
                SetNextPoint();
            }

            return Node.Status.Running;
        }

        private void SetNextPoint()
        {
            if (random)
            {
                int newPoint = currentPoint;
                while (currentPoint == newPoint)
                    newPoint = Random.Range(0, patrolPoints.Length);

                currentPoint = newPoint;
            }
            else
                currentPoint = (currentPoint + 1) % patrolPoints.Length;

            moveStrategy = new MoveToPositionStrategy(npc, patrolPoints[currentPoint].transform.position);
        }

        public void Reset()
        {
            currentPoint = -1;
            SetNextPoint();
        }
    }
}
