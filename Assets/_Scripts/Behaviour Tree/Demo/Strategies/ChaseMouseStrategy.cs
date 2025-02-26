using UnityEngine;
using Utilities.Utilities.Input;

namespace Utilities.BehaviourTree.Demo
{
    public class ChaseMouseStrategy : IStrategy
    {
        private readonly NPC npc;
        private readonly float chaseRadius;

        private const float DISTANCE_TRESHOLD = .2f;

        private float ignoreTimer;

        private float Distance => Vector2.Distance(npc.transform.position, MouseInput.MouseWorldPos);

        public ChaseMouseStrategy(NPC npc, float chaseRadius)
        {
            this.npc = npc;
            this.chaseRadius = chaseRadius;
        }

        public Node.Status Process()
        {
            if (Distance > chaseRadius || Time.time < ignoreTimer)
                return Node.Status.Failure;

            if (Distance <= DISTANCE_TRESHOLD)
            {
                ignoreTimer = Time.time + 2f;
                return Node.Status.Success;
            }

            npc.SetTargetPosition(MouseInput.MouseWorldPos);

            return Node.Status.Running;
        }
    }
}
