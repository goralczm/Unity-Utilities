namespace Utilities.CommandPattern
{
    public abstract class Command
    {
        /// <summary>
        /// Executed once a command is initiated.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Executed every frame until the command is finished.
        /// </summary>
        public abstract void Tick();

        /// <summary>
        /// Undos the command procedure.
        /// </summary>
        public abstract void Undo();

        /// <summary>
        /// Has the command been executed and finished?
        /// </summary>
        public abstract bool IsFinished { get; }
    }
}
