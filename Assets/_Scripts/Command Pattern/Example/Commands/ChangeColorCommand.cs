using UnityEngine;

public class ChangeColorCommand : Command
{
    public override bool IsFinished => true;

    private Color _startColor;
    private Color _newColor;
    private SpriteRenderer _rend;

    public ChangeColorCommand(Color startColor, Color newColor, SpriteRenderer rend)
    {
        _startColor = startColor;
        _newColor = newColor;
        _rend = rend;
    }

    public override void Execute()
    {
        _rend.color = _newColor;
    }

    public override void Tick()
    {

    }

    public override void Undo()
    {
        _rend.color = _startColor;
    }
}
