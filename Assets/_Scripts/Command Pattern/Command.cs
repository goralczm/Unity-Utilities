public abstract class Command
{
    public abstract void Execute();
    public abstract void Tick();
    public abstract void Undo();
    public abstract bool IsFinished { get; }
}
