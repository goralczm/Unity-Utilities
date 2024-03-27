using System;

namespace Utilities.Debug
{
    /// <summary>
    /// Stores the unique ID, description and format of the command.
    /// </summary>
    public abstract class DebugCommandBase
    {
        public string CommandID => _commandId;
        public string CommandDescription => _commandDescription;
        public string CommandFormat => _commandFormat;

        private string _commandId;
        private string _commandDescription;
        private string _commandFormat;

        public DebugCommandBase(string id, string description, string format)
        {
            _commandId = id;
            _commandDescription = description;
            _commandFormat = format;
        }
    }

    /// <summary>
    /// Extends the <see cref="DebugCommandBase"/> with storing an action that can be invoked.
    /// </summary>
    public class DebugCommand : DebugCommandBase
    {
        private Action _command;

        public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
        {
            _command = command;
        }

        /// <summary>
        /// Invokes the stored action.
        /// </summary>
        public void Invoke()
        {
            _command.Invoke();
        }
    }

    /// <summary>
    /// Generic extension of the <see cref="DebugCommandBase"/> allowing passing a parameter to the command.
    /// </summary>
    /// <typeparam name="T1">The generic type of command parameter.</typeparam>
    public class DebugCommand<T1> : DebugCommandBase
    {
        private Action<T1> command;

        public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke(T1 value)
        {
            command.Invoke(value);
        }
    }
}