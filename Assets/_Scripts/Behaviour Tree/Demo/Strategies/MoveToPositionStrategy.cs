using UnityEngine;

namespace Utilities.BehaviourTree.Demo
{
    public class MoveToPositionStrategy : IStrategy
    {
        private readonly NPC npc;
        private readonly Vector2 targetPosition;

        private const float DISTANCE_TRESHOLD = .2f;

        private float Distance => Vector2.Distance(npc.transform.position, targetPosition);

        public MoveToPositionStrategy(NPC npc, Vector2 targetPosition)
        {
            this.npc = npc;
            this.targetPosition = targetPosition;
        }

        public Node.Status Process()
        {
            npc.SetTargetPosition(targetPosition);

            if (Distance <= DISTANCE_TRESHOLD)
                return Node.Status.Success;

            return Node.Status.Running;
        }
    }
}
