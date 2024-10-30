using UnityEngine;

namespace Utilities.CommandPattern.Demo
{
    public class ChangeColorCommand : ICommand
    {
        public bool IsFinished => true;

        private Color _startColor;
        private Color _newColor;
        private SpriteRenderer _rend;

        public ChangeColorCommand(Color startColor, Color newColor, SpriteRenderer rend)
        {
            _startColor = startColor;
            _newColor = newColor;
            _rend = rend;
        }

        public void Execute()
        {
            _rend.color = _newColor;
        }

        public void Tick()
        {

        }

        public void Undo()
        {
            _rend.color = _startColor;
        }
    }
}
