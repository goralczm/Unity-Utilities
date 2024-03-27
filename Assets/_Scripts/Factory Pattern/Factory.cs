using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities.FactoryPattern
{
    /// <summary>
    /// Factory allowing instantiation of its factory item classes by a unique name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Factory<T> where T : FactoryItem
    {
        private static Dictionary<string, Type> _itemsByName;
        private static bool IsInitialized => _itemsByName != null;

        /// <summary>
        /// Initializes the factory, caching all the factory items types with a name.
        /// </summary>
        private static void InitializeFactory()
        {
            if (IsInitialized)
                return;

            var itemType = Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));

            _itemsByName = new Dictionary<string, Type>();

            foreach (var type in itemType)
            {
                var tempItem = Activator.CreateInstance(type) as T;
                _itemsByName.Add(tempItem.Name, type);
            }
        }

        /// <summary>
        /// Retrieves the factory item based on unique name.
        /// </summary>
        /// <param name="name">The factory item name.</param>
        /// <returns>
        /// The retrieved class.
        /// <see cref="null"/> if not found.
        /// </returns>
        public static T GetItem(string name)
        {
            InitializeFactory();

            if (!_itemsByName.ContainsKey(name))
                return null;

            Type type = _itemsByName[name];
            var item = Activator.CreateInstance(type) as T;
            return item;
        }

        /// <summary>
        /// Returns all the cached factory items names.
        /// </summary>
        /// <returns>The enumerator with string names.</returns>
        internal static IEnumerable<string> GetItemNames()
        {
            InitializeFactory();
            return _itemsByName.Keys;
        }
    }
}
