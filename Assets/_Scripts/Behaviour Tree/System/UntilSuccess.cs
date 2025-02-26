namespace Utilities.BehaviourTree
{
    public class UntilSuccess : Node
    {
        public UntilSuccess(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            if (children[0].Process() == Status.Success)
            {
                Reset();
                return Status.Success;
            }

            return Status.Running;
        }
    }
}
