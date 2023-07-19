
namespace binary_tree
{
    internal sealed class BSNode<T> where T : IComparable
    {
        public BSNode<T>? LeftChild { get; set; }
        public BSNode<T>? RightChild { get; set; }
        public BSNode<T>? Parent { get; set; }
        public T? Item { get;  set; }

        public BSNode()
        {

        }
        public BSNode(T? item)
        {
            if(item !=null)
                this.Item = item;
            else
                throw new InvalidOperationException("null is not allowed");
        }
    }
}
