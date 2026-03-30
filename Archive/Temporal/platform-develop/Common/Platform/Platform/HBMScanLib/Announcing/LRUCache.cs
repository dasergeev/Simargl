// <copyright file="LruCache.cs" company="Hottinger Baldwin Messtechnik GmbH">
//
// SharpScan, a library for scanning and configuring HBM devices.
//
// The MIT License (MIT)
//
// Copyright (C) Hottinger Baldwin Messtechnik GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
// ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// </copyright>

[assembly: InternalsVisibleTo("SharpScanTests")]

namespace Hbm.Devices.Scan.Announcing
{
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
}
