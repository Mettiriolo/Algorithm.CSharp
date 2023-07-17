// See https://aka.ms/new-console-template for more information

using binary_tree;

char[] characters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
BiTree<char> tree = new BiTree<char>(characters.Length);
for (int i = 0; i < characters.Length; i++)
{
    tree.Add(characters[i]);
}
Console.WriteLine("前序遍历：");
tree.PreorderTraversal();
Console.WriteLine();
Console.WriteLine("中序遍历：");
tree.InorderTraversal();
Console.WriteLine();
Console.WriteLine("后序遍历：");
tree.PostorderTraversal();
Console.WriteLine();
Console.WriteLine("层序遍历：");
tree.SequenceTraversal();

BSTree<int> bSTree = new BSTree<int>();
int[] numbers = { 62, 58, 88, 47, 73, 99, 35, 51, 93, 37 };
foreach (int number in numbers)
{
    bSTree.Add(number);
}
bSTree.InorderTraversal();
Console.WriteLine();
foreach (int number in numbers)
{
    Console.Write(bSTree.Find(number)+" ");
}
Console.ReadKey();