using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class Factory<T> where T : FactoryItem
{
    private static Dictionary<string, Type> _itemsByName;
    private static bool IsInitialized => _itemsByName != null;

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

    public static T GetItem(string name)
    {
        InitializeFactory();

        if (!_itemsByName.ContainsKey(name))
            return null;

        Type type = _itemsByName[name];
        var item = Activator.CreateInstance(type) as T;
        return item;
    }

    internal static IEnumerable<string> GetItemNames()
    {
        InitializeFactory();
        return _itemsByName.Keys;
    }
}
