using UnityEngine;

namespace Utilities.CommandPattern.Demo
{
    public class MoveCommand : Command
    {
        public override bool IsFinished => (Vector2)_target.position == _destination ||
                                           (Vector2)_target.position == _origin;

        private readonly Vector2 _origin;
        private readonly Vector2 _destination;
        private readonly Transform _target;
        private readonly float _speed;

        private bool _isRewinding;

        public MoveCommand(Vector2 origin, Vector2 destination, Transform target, float speed)
        {
            _origin = origin;
            _destination = destination;
            _target = target;
            _speed = speed;
        }


        public override void Execute()
        {
            _isRewinding = false;
        }

        public override void Tick()
        {
            Vector2 moveTarget = !_isRewinding ? _destination : _origin;
            _target.transform.position = Vector2.MoveTowards(_target.transform.position, moveTarget, Time.deltaTime * _speed);
        }

        public override void Undo()
        {
            _isRewinding = true;
        }
    }
}
