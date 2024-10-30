using UnityEngine;

namespace Utilities.CommandPattern.Demo
{
    public class WaitCommand : ICommand
    {
        private readonly float _waitTime;
        private float _startTime;

        public bool IsFinished => Time.time - _startTime >= _waitTime;

        public WaitCommand(float waitTime)
        {
            _waitTime = waitTime;
        }

        public void Execute()
        {
            _startTime = Time.time;
        }

        public void Tick()
        {

        }

        public void Undo()
        {
            _startTime = Time.time;
        }
    }
}
