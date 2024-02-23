using System.Collections;

namespace LinkedList
{
    public class DoublyCircularLinkedList<T> : ICollection<T>
    {
        private readonly Node<T> sentinel = new (default);

        public DoublyCircularLinkedList()
        {
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
            sentinel.List = this;
        }

        public DoublyCircularLinkedList(IEnumerable<T> collection) : this()
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (var item in collection)
            {
                AddLast(item);
            }
        }

        public Node<T>? First { get => Count == 0 ? null : sentinel.Next; }

        public Node<T>? Last { get => Count == 0 ? null : sentinel.Previous; }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public Node<T> AddAfter(Node<T> node, T value)
        {
            ValidateNode(node);
            Node<T> newNode = new (value);
            AddBefore(node.Next!, newNode);

            return newNode;
        }

        public void AddAfter(Node<T> node, Node<T> newNode)
        {
            ValidateNode(node);
            AddBefore(node.Next!, newNode);
        }

        public Node<T> AddBefore(Node<T> node, T value)
        {
            Node<T> newNode = new (value);
            AddBefore(node, newNode);

            return newNode;
        }

        public void AddBefore(Node<T> node, Node<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);

            newNode.Next = node;
            newNode.Previous = node.Previous;
            node.Previous!.Next = newNode;
            node.Previous = newNode;

            newNode.List = this;
            Count++;
        }

        public Node<T> AddFirst(T value)
        {
            Node<T> newNode = new (value);
            AddAfter(sentinel, newNode);

            return newNode;
        }

        public void AddFirst(Node<T> newNode)
        {
            AddAfter(sentinel, newNode);
        }

        public Node<T> AddLast(T value)
        {
            Node<T> newNode = new (value);
            AddBefore(sentinel, newNode);

            return newNode;
        }

        public void AddLast(Node<T> newNode)
        {
            AddBefore(sentinel, newNode);
        }

        public void Clear()
        {
            sentinel.Next = sentinel;
            sentinel.Previous = sentinel;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateArray(array);
            ValidateArrayIndex(arrayIndex);
            ValidateArrayAvailableSpace(array, arrayIndex);
            CopyItems(array, arrayIndex);
        }

        public Node<T>? Find(T value)
        {
            for (Node<T> current = sentinel.Next!; current != sentinel; current = current.Next!)
            {
                if (current!.Value!.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        public Node<T>? FindLast(T value)
        {
            for (Node<T> current = sentinel.Previous!; current != sentinel; current = current.Previous!)
            {
                if (current!.Value!.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> current = sentinel.Next!; current != sentinel; current = current.Next!)
            {
                yield return current.Value!;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(T item)
        {
            Node<T>? targetNode = Find(item);

            if (targetNode == null)
            {
                return false;
            }

            Remove(targetNode);
            return true;
        }

        public void Remove(Node<T> node)
        {
            ValidateNode(node);
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
            node.Invalidate();
            Count--;
        }

        public void RemoveFirst()
        {
            ValidateList();
            Remove(First);
        }

        public void RemoveLast()
        {
            ValidateList();
            Remove(Last);
        }

        private static void ValidateArray(T[] array)
        {
            ArgumentNullException.ThrowIfNull(array);
        }

        private static void ValidateArrayIndex(int arrayIndex)
        {
            if (arrayIndex >= 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        private static void ValidateNewNode(Node<T> node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.List == null)
            {
                return;
            }

            throw new InvalidOperationException("Node belongs to another LinkedList<T>.");
        }

        private void ValidateArrayAvailableSpace(T[] array, int arrayIndex)
        {
            if (Count <= (array.Length - arrayIndex))
            {
                return;
            }

            throw new ArgumentException("The number of elements in the source LinkedList<T> is greater than the available" +
                "space from arrayIndex to the end of the destination array.");
        }

        private void CopyItems(T[] array, int arrayIndex)
        {
            for (Node<T> current = sentinel.Next!; current != sentinel; current = current.Next!)
            {
                array[arrayIndex] = current!.Value!;
                arrayIndex++;
            }
        }

        private void ValidateList()
        {
            if (Count > 0)
            {
                return;
            }

            throw new InvalidOperationException("List is empty");
        }

        private void ValidateNode(Node<T> node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.List == this)
            {
                return;
            }

            throw new InvalidOperationException("Node does not belong to the current list");
        }
    }
}