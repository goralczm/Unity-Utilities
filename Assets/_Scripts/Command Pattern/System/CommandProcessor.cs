using System.Collections.Generic;
using UnityEngine;

namespace Utilities.CommandPattern
{
    public class CommandProcessor : MonoBehaviour
    {
        [SerializeField] private int _maxHistoryCount = 15;

        protected Queue<Command> _commands = new Queue<Command>();
        protected OpenStack<Command> _history = new OpenStack<Command>();
        protected Stack<Command> _undoHistory = new Stack<Command>();

        protected Command _currentCommand;
        protected bool IsCurrentCommandFinished => _currentCommand == null || _currentCommand.IsFinished;

        protected virtual void Update()
        {
            ListenForCommands();
            ProcessCommands();
        }

        protected virtual void ListenForCommands()
        {

        }

        protected virtual void ProcessCommands()
        {
            if (_commands.Count == 0)
                return;

            if (!IsCurrentCommandFinished)
            {
                _currentCommand?.Tick();
                return;
            }

            SetAndExecuteCommand(_commands.Dequeue());
        }

        public void SetAndExecuteCommand(Command command)
        {
            _currentCommand = command;
            ExecuteCommand(_currentCommand);
        }

        public void ExecuteCommand(Command command)
        {
            command.Execute();
            command.Tick();

            if (_history.Count == _maxHistoryCount)
                _history.PopLast();

            _history.Push(command);
        }

        public void EnqueueCommand(Command command)
        {
            _commands.Enqueue(command);
            _undoHistory.Clear();
        }

        public void UndoLastCommand()
        {
            if (_history.Count == 0)
                return;

            _currentCommand = _history.Pop();
            _currentCommand.Undo();
            _undoHistory.Push(_currentCommand);
        }

        public void RedoLastUndo()
        {
            if (_undoHistory.Count == 0)
                return;

            SetAndExecuteCommand(_undoHistory.Pop());
        }
    }
}