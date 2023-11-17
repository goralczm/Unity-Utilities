using UnityEngine;

public class WaitCommand : Command
{
    private readonly float _waitTime;
    private float _startTime;

    public override bool IsFinished => Time.time - _startTime >= _waitTime;

    public WaitCommand(float waitTime)
    {
        _waitTime = waitTime;
    }

    public override void Execute()
    {
        _startTime = Time.time;
    }

    public override void Tick()
    {

    }

    public override void Undo()
    {
        _startTime = Time.time;
    }
}
