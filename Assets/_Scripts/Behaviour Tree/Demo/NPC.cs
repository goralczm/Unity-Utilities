using UnityEngine;
using Utilities.Utilities.Input;

namespace Utilities.BehaviourTree.Demo
{
    public class NPC : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _normalSpeed = 3f;
        [SerializeField] private float _chaseRadius = 4f;
        [SerializeField] private Transform _healthpod;
        [SerializeField] private Health _health;

        [Header("Patrol Settings")]
        [SerializeField] private Transform[] _patrolPoints;

        private SpriteRenderer _rend;
        private BehaviourTree _tree;

        private Vector2 _targetPosition;
        private float _speed;

        public bool IsNearMouse() => Vector2.Distance(transform.position, MouseInput.MouseWorldPos) <= _chaseRadius;

        private void Awake()
        {
            _rend = GetComponent<SpriteRenderer>();

            _tree = new BehaviourTree("NPC", true);

            PrioritySelector actions = new PrioritySelector("NPC Logic");

            Sequence retreatSeq = new Sequence("Retreat Sequence", 200);
            retreatSeq.AddChild(new Leaf("isLowHealth?", new ConditionStrategy(() => _health.GetHealth() <= 3)));
            retreatSeq.AddChild(new Leaf("Move To Safety", new MoveToPositionStrategy(this, _healthpod.transform.position)));
            retreatSeq.AddChild(new Leaf("Wait", new WaitStrategy(1f)));

            Sequence chaseSeq = new Sequence("Chase Sequence", 100);
            bool IsChasing()
            {
                if (!IsNearMouse())
                {
                    chaseSeq.Reset();
                    return false;
                }

                return true;
            }
            chaseSeq.AddChild(new Leaf("isChasing?", new ConditionStrategy(IsChasing)));
            chaseSeq.AddChild(new Leaf("Chase Mouse", new ChaseMouseStrategy(this, _chaseRadius)));

            Sequence patrolSeq = new Sequence("Patrol Sequence", 50);
            patrolSeq.AddChild(new Leaf("Patrol", new PatrolStrategy(this, _patrolPoints, true)));

            actions.AddChild(retreatSeq);
            actions.AddChild(chaseSeq);
            actions.AddChild(patrolSeq);

            _tree.AddChild(actions);
        }

        private void Update()
        {
            _speed = _normalSpeed * (1 / (_health.GetHealth() / 10f));

            _tree.Process();

            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);
        }

        public void SetTargetPosition(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseRadius);
        }
    }
}