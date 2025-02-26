namespace Utilities.BehaviourTree
{
    public interface IStrategy
    {
        Node.Status Process();
        public void Reset()
        {
            // Noop
        }
    }
}
