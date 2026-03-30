using System.Collections.Generic;

namespace Hbm.Devices.Scan.Announcing;

internal class LruCache<TKey, TValue> where TKey : notnull
{
    private readonly IDictionary<TKey, LinkedListNode<Node>> map;

    private readonly LinkedList<Node> list;

    private readonly int capacity;

    internal LruCache(int capacity)
    {
        map = new Dictionary<TKey, LinkedListNode<Node>>(capacity);
        list = new LinkedList<Node>();
        this.capacity = capacity;
    }

    public int Capacity
    {
        get
        {
            return capacity;
        }
    }

    public int Count
    {
        get
        {
            return map.Count;
        }
    }

    internal void Add(TKey key, TValue value)
    {
        if (map.Count >= capacity)
        {
            LinkedListNode<Node>? nodeToRemove = list.Last;
            if (nodeToRemove != null)
            {
                list.RemoveLast();
                Node node = nodeToRemove.Value;
                map.Remove(node.Key);
            }
        }

        Node newNode = new(key, value);
        LinkedListNode<Node> listNode = new(newNode);
        map.Add(key, listNode);
        list.AddFirst(listNode);
    }

    internal bool TryGetValue(TKey key, out TValue? value)
    {
        if (map.TryGetValue(key, out LinkedListNode<LruCache<TKey, TValue>.Node>? node))
        {
            list.Remove(node);
            list.AddFirst(node);
            value = node.Value.Value;
            return true;
        }
        else
        {
            value = default(TValue);
            return false;
        }
    }

    internal void Remove(TKey key)
    {
        if (map.TryGetValue(key, out LinkedListNode<LruCache<TKey, TValue>.Node>? node))
        {
            map.Remove(key);
            list.Remove(node);
        }
    }

    internal class Node
    {
        private readonly TKey key;

        private readonly TValue value;

        internal Node(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public TKey Key
        {
            get
            {
                return key;
            }
        }

        public TValue Value
        {
            get
            {
                return value;
            }
        }
    }
}
