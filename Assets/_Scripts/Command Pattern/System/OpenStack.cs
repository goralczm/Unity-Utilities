using System.Collections.Generic;

namespace Utilities.CommandPattern
{
    public class OpenStack<T>
    {
        private List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Push(T item)
        {
            _items.Add(item);
        }

        public T Pop()
        {
            if (_items.Count == 0)
                return default(T);

            T it = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            return it;
        }

        public T PopLast()
        {
            if (_items.Count == 0)
                return default(T);

            T it = _items[0];
            _items.RemoveAt(0);

            return it;
        }
    }
}
