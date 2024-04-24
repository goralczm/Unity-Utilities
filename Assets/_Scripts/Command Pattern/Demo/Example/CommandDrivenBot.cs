using UnityEngine;
using Utilities.Utilities.Shapes;

namespace Utilities.CommandPattern.Demo
{
    public class CommandDrivenBot : CommandProcessor
    {
        [Header("Settings")]
        [SerializeField] private float _searchRange;

        [Header("Instances")]
        [SerializeField] private Inventory _botInventory;
        [SerializeField] private Rectangle _rectangle;

        private Camera _cam;
        private SpriteRenderer _rend;

        private void Start()
        {
            _cam = Camera.main;
            _rend = GetComponent<SpriteRenderer>();
        }

        protected override void ListenForCommands()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _currentCommand = null;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                EnqueueMoveToMouse();
                EnqueueWait(1f);
            }

            if (_commands.Count > 0)
                return;

            if (!IsCurrentCommandFinished)
                return;

            ItemPickup nearestItem = FindNearestItem();
            if (nearestItem != null)
            {
                EnqueuePickupSequence(nearestItem);
                return;
            }

            EnqueueRandomDestination();
            EnqueueWait(1f);
        }

        private void EnqueueMoveToMouse()
        {
            Vector2 mouseWorldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            EnqueueMove(mouseWorldPos);
        }

        private void EnqueueMove(Vector2 position)
        {
            MoveCommand moveCommand = new MoveCommand(transform.position, position, transform, 3f);
            EnqueueCommand(moveCommand);
        }

        private void EnqueueWait(float delay)
        {
            WaitCommand waitCommand = new WaitCommand(delay);
            EnqueueCommand(waitCommand);
        }

        private void EnqueuePickupSequence(ItemPickup item)
        {
            EnqueueMove(item.transform.position);

            EnqueueWait(1f);

            EnqueuePickupItem(item);

            EnqueueRandomColorChange();

            EnqueueWait(1f);
        }

        private void EnqueuePickupItem(ItemPickup item)
        {
            PickupCommand pickupCommand = new PickupCommand(_botInventory, item);
            EnqueueCommand(pickupCommand);
        }

        private ItemPickup FindNearestItem()
        {
            ItemPickup pickup = null;
            float currentMinDistance = int.MaxValue;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _searchRange);
            foreach (Collider2D hit in hits)
            {
                ItemPickup item = hit.GetComponent<ItemPickup>();
                if (item == null)
                    continue;

                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < currentMinDistance)
                {
                    currentMinDistance = distance;
                    pickup = item;
                }
            }

            return pickup;
        }

        private void EnqueueRandomColorChange()
        {
            Color newColor = GetRandomColor();

            ChangeColorCommand changeColorCommand = new ChangeColorCommand(_rend.color, newColor, _rend);
            EnqueueCommand(changeColorCommand);
        }

        private Color GetRandomColor()
        {
            return new Color(Random.Range(0f, 1f),
                             Random.Range(0f, 1f),
                             Random.Range(0f, 1f));
        }

        private void EnqueueRandomDestination()
        {
            Vector2 randomDestination = _rectangle.GetRandomPositionInside();
            EnqueueMove(randomDestination);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _searchRange);
        }
    }
}
