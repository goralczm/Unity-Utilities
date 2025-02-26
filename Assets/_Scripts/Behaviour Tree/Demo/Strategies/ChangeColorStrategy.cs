using UnityEngine;

namespace Utilities.BehaviourTree.Demo
{
    public class ChangeColorStrategy : IStrategy
    {
        readonly SpriteRenderer spriteRendrer;
        readonly Color color;

        public ChangeColorStrategy(SpriteRenderer spriteRendrer, Color color)
        {
            this.spriteRendrer = spriteRendrer;
            this.color = color;
        }

        public Node.Status Process()
        {
            spriteRendrer.color = color;

            return Node.Status.Success;
        }
    }
}
