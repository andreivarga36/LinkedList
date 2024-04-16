using DoublyCircularLinkedList.Classes;

namespace LinkedList
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public Node<T>? Next { get; internal set; }

        public Node<T>? Previous { get; internal set; }

        public T? Value { get; internal set; }

        public DoublyCircularLinkedList<T>? List { get; internal set; }

        public void Invalidate()
        {
            Next = null;
            Previous = null;
            Value = default;
            List = null;
        }
    }
}
