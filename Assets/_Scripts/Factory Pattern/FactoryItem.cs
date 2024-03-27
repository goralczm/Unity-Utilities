namespace Utilities.FactoryPattern
{
    /// <summary>
    /// Factory item containing unique name as an identifier.
    /// </summary>
    public abstract class FactoryItem
    {
        public abstract string Name { get; }
    }
}