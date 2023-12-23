using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public static DebugCommand HELP;

    private List<object> _commandList;

    private List<string> _commandHistory;
    private Vector2 _scroll;
    private bool _isShown;
    private bool _showHelp;
    private string _input;

    private void Awake()
    {
        _commandHistory = new List<string>();

        HELP = new DebugCommand("help", "Shows list of all commands with description", "help", () =>
        {
            _showHelp = !_showHelp;
        });

        _commandList = new List<object>
        {
            HELP,
        };
    }

    private void Update()
    {
        if (Input.GetButtonDown("DebugConsole"))
            _isShown = !_isShown;
    }

    private void OnGUI()
    {
        if (!_isShown)
            return;

        float y = 0f;

        if (_showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * _commandList.Count);

            _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), _scroll, viewport);

            for (int i = 0; i < _commandList.Count; i++)
            {
                DebugCommandBase command = _commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100f;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        if (Event.current.keyCode == KeyCode.Tab)
        {
            foreach (object command in _commandList)
            {
                DebugCommandBase commandBase = command as DebugCommandBase;

                if (commandBase.commandId.Contains(_input))
                {
                    _input = commandBase.commandId;
                    break;
                }
            }
        }

        if (Event.current.character == '`' || Event.current.keyCode == KeyCode.Escape)
        {
            _input = "";
            _isShown = false;
            return;
        }

        GUI.SetNextControlName("Console");
        _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
        GUI.FocusControl("Console");

        if (Event.current.keyCode == KeyCode.Return)
        {
            HandleInput();
            _input = "";
        }

        if (_commandHistory.Count > 0 && Event.current.keyCode == KeyCode.UpArrow)
            _input = _commandHistory[_commandHistory.Count - 1];
    }

    private void HandleInput()
    {
        if (_input == "")
            return;

        string[] properties = _input.Split(' ');

        for (int i = 0; i < _commandList.Count; i++)
        {
            DebugCommandBase commandBase = _commandList[i] as DebugCommandBase;

            if (_input.Contains(commandBase.commandId))
            {
                if (_commandList[i] as DebugCommand != null)
                {
                    (_commandList[i] as DebugCommand).Invoke();
                }
                else if (_commandList[i] as DebugCommand<int> != null)
                {
                    if (properties.Length < 2)
                        continue;

                    try
                    {
                        (_commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                    }
                    catch
                    {
                        (_commandList[i] as DebugCommand<int>).Invoke(int.MaxValue - 1);
                    }
                }
                else if (_commandList[i] as DebugCommand<float> != null)
                {
                    if (properties.Length < 2)
                        continue;

                    (_commandList[i] as DebugCommand<float>).Invoke(float.Parse(properties[1]));
                }
                else if (_commandList[i] as DebugCommand<string> != null)
                {
                    if (properties.Length < 2)
                        continue;

                    StringBuilder output = new StringBuilder();
                    for (int j = 1; j < properties.Length; j++)
                    {
                        output.Append(properties[j]);
                        output.Append(' ');
                    }

                    (_commandList[i] as DebugCommand<string>).Invoke(output.ToString());
                }
                AddInputToHistory(_input);
            }
        }
    }

    private void AddInputToHistory(string input)
    {
        _commandHistory.Add(input);

        if (_commandHistory.Count == 10)
            _commandHistory.RemoveAt(0);
    }
}
