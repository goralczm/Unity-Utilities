using UnityEngine;

namespace Utilities.BehaviourTree.Demo
{
    public class FollowTargetStrategy : IStrategy
    {
        private readonly NPC npc;
        private readonly Transform target;

        private const float DISTANCE_TRESHOLD = .2f;

        private float Distance => Vector2.Distance(npc.transform.position, target.transform.position);

        public FollowTargetStrategy(NPC npc, Transform target)
        {
            this.npc = npc;
            this.target = target;
        }

        public Node.Status Process()
        {
            if (Distance <= DISTANCE_TRESHOLD)
                return Node.Status.Success;

            npc.SetTargetPosition(target.transform.position);

            return Node.Status.Running;
        }
    }
}
