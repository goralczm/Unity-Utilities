using UnityEngine;

public class CommandDrivenBot : CommandProcessor
{
    [Header("Instances")]
    [SerializeField] private MovementAgent _movementAgent;
    [SerializeField] private Inventory _botInventory;

    private SpriteRenderer _rend;
    private bool _isRewinding;

    private void Start()
    {
        _rend = GetComponent<SpriteRenderer>();

        ItemPickup itemPickup = FindObjectOfType<ItemPickup>();

        MoveCommand moveCommand = new MoveCommand(_movementAgent.Destination, itemPickup.transform.position, _movementAgent);
        EnqueueCommand(moveCommand);

        PickupCommand pickupCommand = new PickupCommand(_botInventory, itemPickup);
        EnqueueCommand(pickupCommand);
    }

    protected override void ListenForCommands()
    {
        return;

        CheckRewindCondition();

        if (_isRewinding)
        {
            Rewind();
            return;
        }

        if (_commands.Count > 0)
            return;

        if (!IsCurrentCommandFinished)
            return;

        _rend.color = Color.white;
        SetRandomDestination();
        Wait(1f);
    }

    private void CheckRewindCondition()
    {
        if (_history.Count == 0)
            _isRewinding = false;

        if (_history.Count >= 5)
            _isRewinding = true;
    }

    private void Rewind()
    {
        if (!IsCurrentCommandFinished || _commands.Count > 0)
            return;

        _rend.color = Color.magenta;
        UndoLastCommand();
    }

    private void SetRandomDestination()
    {
        Vector2 randomDestination = GetRandomPosition();
        MoveCommand moveCommand = new MoveCommand(_movementAgent.Destination, randomDestination, _movementAgent);
        EnqueueCommand(moveCommand);
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-8f, 8f),
                           Random.Range(-4f, 4f));
    }

    private void Wait(float delay)
    {
        WaitCommand waitCommand = new WaitCommand(delay);
        EnqueueCommand(waitCommand);
    }
}
