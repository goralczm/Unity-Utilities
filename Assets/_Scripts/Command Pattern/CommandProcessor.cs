using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    protected Queue<Command> _commands = new Queue<Command>();
    protected Stack<Command> _history = new Stack<Command>();

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
            return;

        SetAndExecuteCommand(_commands.Dequeue());
    }

    protected void SetAndExecuteCommand(Command command)
    {
        _currentCommand = command;
        ExecuteCommand(_currentCommand);
    }

    protected void ExecuteCommand(Command command)
    {
        command.Execute();
        _history.Push(command);
    }

    protected void EnqueueCommand(Command command)
    {
        _commands.Enqueue(command);
    }

    protected void UndoLastCommand()
    {
        if (_history.Count == 0)
            return;

        _currentCommand = _history.Pop();
        _currentCommand.Undo();
    }
}