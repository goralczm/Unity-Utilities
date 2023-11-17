using UnityEngine;

public class MoveCommand : Command
{
    public override bool IsFinished => (Vector2)_target.transform.position == _destination ||
                                       (Vector2)_target.transform.position == _origin;

    private readonly Vector2 _origin;
    private readonly Vector2 _destination;
    private readonly MovementAgent _target;

    public MoveCommand(Vector2 origin, Vector2 destination, MovementAgent player)
    {
        _origin = origin;
        _destination = destination;
        _target = player;
    }


    public override void Execute()
    {
        _target.SetDestination(_destination);
    }

    public override void Undo()
    {
        _target.SetDestination(_origin);
    }
}
