using System;

namespace binary_tree
{
    internal sealed class BiTree<T>
    {
        private T[] _items;
        private int _count;
        public BiTree(int capacity)
        {
            _items = new T[capacity];
        }

        public bool Add(T item)
        {
            if (_count >= _items.Length)
                return false;
            _items[_count++] = item;
            return true;
        }
        public void PreorderTraversal() => PreorderTraversal(0);
        //前序遍历
        void PreorderTraversal(int index)
        {
            if (index < _items.Length && _items[index] != null)
            {
                Console.Write(" " + _items[index]);
                PreorderTraversal(index * 2 + 1);
                PreorderTraversal(index * 2 + 2);
            }
        }
        public void InorderTraversal() => InorderTraversal(0);
        //中序遍历
        void InorderTraversal(int index)
        {
            if (index < _items.Length && _items[index] != null)
            {
                InorderTraversal(index * 2 + 1);
                Console.Write(" " + _items[index]);
                InorderTraversal(index * 2 + 2);
            }
        }

         public void PostorderTraversal() => PostorderTraversal(0);
        //后序遍历
        void PostorderTraversal(int index)
        {
            if (index < _items.Length && _items[index] != null)
            {
                PostorderTraversal(index * 2 + 1);
                PostorderTraversal(index * 2 + 2);
                Console.Write(" " + _items[index]);
            }
        }

        //层序遍历
        public void SequenceTraversal()=> 
            Console.WriteLine(string.Join(" ",_items));
    }
}
