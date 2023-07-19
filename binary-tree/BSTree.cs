using System;
using System.Xml;

namespace binary_tree
{
    internal sealed class BSTree<T> where T : IComparable
    {
        private BSNode<T>? _root;

        public bool Add(T item)
        {
            if (item == null)
                return false;
            BSNode<T> newNode = new BSNode<T>(item);
            if (_root == null)
                _root = newNode;
            else
            {
                BSNode<T> temp = _root;
                while (true)
                {
                    if (item.CompareTo(temp.Item) < 0)//放在temp左边
                    {
                        if (temp.LeftChild != null)
                            temp = temp.LeftChild;
                        else
                        {
                            temp.LeftChild = newNode;
                            newNode.Parent = temp;
                            break;
                        }
                    }
                    else//放在temp左边
                    {
                        if (temp.RightChild != null)
                            temp = temp.RightChild;
                        else
                        {
                            temp.RightChild = newNode;
                            newNode.Parent = temp;
                            break;
                        }
                    }
                }
            }
            return true;
        }

        //中序遍历
        public void InorderTraversal() => InorderTraversal(_root);

        private void InorderTraversal(BSNode<T>? node)
        {
            if (node == null)
                return;
            InorderTraversal(node.LeftChild);
            Console.Write(" " + node.Item);
            InorderTraversal(node.RightChild);
        }

        public bool Find(T? item)
        {
            if (item == null || _root == null)
                return false;
            else
                return Find(item, _root);
        }
        private bool Find(T item, BSNode<T> node)
        {
            if (item.CompareTo(node.Item) == 0)
                return true;
            else if (item.CompareTo(node.Item) < 0)
            {
                if (node.LeftChild != null)
                    return Find(item, node.LeftChild);
                else
                    return false;
            }
            else
            {
                if (node.RightChild != null)
                    return Find(item, node.RightChild);
                else
                    return false;
            }
        }

        //private bool Delete(BSNode<T> node)
        //{
        //    if (node.LeftChild == null && node.RightChild == null)
        //    {
        //        if (node.Item.CompareTo(node.Parent.Item) < 0)
        //        {
        //            node.Parent.LeftChild = null;
        //        }
        //        else
        //        {
        //            node.Parent.RightChild = null;
        //        }
        //        return true;
        //    }
        //    else if (node.LeftChild == null && node.RightChild != null)
        //    {
        //        node.Item = node.RightChild.Item;
        //        node.RightChild = null;
        //    }
        //    else if (node.LeftChild != null && node.RightChild == null)
        //    {
        //        node.Item = node.LeftChild.Item;
        //        node.LeftChild = null;
        //    }
        //    else
        //    { 
            
        //    }
        //}
    }
}
