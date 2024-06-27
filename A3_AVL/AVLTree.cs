using A3_AVL;
using System.Text;

internal class AVLTree
{
    public Node Root { get; set; }

    public AVLTree()
    {
        Root = null;
    }

    public void Clear()
    {
        Root = null;
    }

    #region Read words from a file and store them in BST
    public void ReadWordsFromFile(string filePath)
    {
        try
        {
            List<string> words = new List<string>(File.ReadAllLines(filePath));
            Clear();

            foreach (string word in words)
            {
                if (!word.StartsWith("#") && !string.IsNullOrWhiteSpace(word))
                {
                    Add(word); 
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("File not found: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }
    #endregion

    #region Insert Operation
    public void Add(string word)
    {//UI Call
        int alength = word.Length; 
        Node node = new Node(word, alength);

        if (Root == null)
        {
            Root = node;
        }
        else
        {
            Root = InsertNode(Root, node);
        }
    }

    private Node InsertNode(Node tree, Node node) 
    {
        //1. Current sub-tree node is empty, insert node here
        if (tree == null)
        {
            tree = node;
            return tree;
        }
        else if (string.Compare(node.Word, tree.Word) < 0)
        {// 2. Traverse left side, insert when null (step 1), then balance tree
            tree.Left = InsertNode(tree.Left, node);
            tree = BalanceTree(tree);
        }

        else if (string.Compare(node.Word, tree.Word) > 0)
        {// 2. Traverse right side, insert when null (step 1), then balance tree
            tree.Right = InsertNode(tree.Right, node);
            tree = BalanceTree(tree);
        }
        return tree;
    }

    private Node BalanceTree(Node current)
    {//1. Obtain a balance reference from height of both left and right sub-trees from current node
        int b_factor = BalanceFactor(current);
        if (b_factor > 1)
        {// 2. Left side of Tree is Unbalanced Decide a left or right rotation
            if (BalanceFactor(current.Left) > 0)
            {//3. Left side requires rotaion, perform a left sub- tree rotation
                current = RotateLL(current);
            }
            else
            {//4. Right side requires rotaion, perform a right sub- tree rotation
                current = RotateLR(current);
            }

        }
        else if (b_factor < -1)
        {// 5. Right side of Tree is Unbalanced Decide a left or right rotation
            if (BalanceFactor(current.Right) > 0)
            {//6. Left side requires rotaion, perform a left sub- tree rotation
                current = RotateRL(current);
            }
            else
            {//7. Right side requires rotaion, perform a right sub- tree rotation
                current = RotateRR(current);
            }
        }
        return current;
    }

    private Node RotateRR(Node parent)
    {  // Perform a right rotation on the right side of the sub-tree by
       // swapping the nodes around based on reassigning the parent node
       //to the right side of the sub-tree.
        Node pivot = parent.Right;
        parent.Right = pivot.Left;
        pivot.Left = parent;
        return pivot;
    }

    private Node RotateRL(Node parent)
    {   // Perform a left rotation on the right side of the sub-tree by
        // swapping the nodes around based on performing a left rotation
        //to the right side of the sub-tree.
        Node pivot = parent.Right;
        parent.Right = RotateLL(pivot);
        return RotateRR(parent);
    }

    private Node RotateLL(Node parent)
    {   // Perform a left rotation on the left side of the sub-tree by
        // swapping the nodes around based on reassigning the parent node
        //to the left side of the sub-tree.
        Node pivot = parent.Left;
        parent.Left = pivot.Right;
        pivot.Right = parent;
        return pivot;
    }

    private Node RotateLR(Node parent)
    {   // Perform a right rotation on the left side of the sub-tree by
        // swapping the nodes around based on performing a left rotation
        //to the left side of the sub-tree.
        Node pivot = parent.Left;
        parent.Left = RotateRR(pivot);
        return RotateLL(parent);
    }

    private int Max(int left, int right)
    {
        return left > right ? left : right;
    }
    private int GetHeight(Node current)
    {//Determine the height of the current sub-tree
        int height = 0;
        if (current != null)
        {
            int left = GetHeight(current.Left);
            int right = GetHeight(current.Right);
            int max = Max(left, right);
            height = max + 1;
        }
        return height;
    }

    private int BalanceFactor(Node current)
    {//Determine if the sub-tree needs to ratate left or right by finding the height of the left and right
     //sides of subtree, and then taking the difference between the left and right.
     //A balance factor greater than 1 (+2) indicate the left side is unbalanced. Abalance factor less than
     //-1 (-2) indicates the right side is unbalanced. Every other balance factor does not require rotation
        int left = GetHeight(current.Left);
        int right = GetHeight(current.Right);
        int b_factor = left - right;
        return b_factor;

    }
    #endregion

    #region Print Order
    //Pre Order
    private string TraversePreOrder(Node node)
    {
        StringBuilder sb = new StringBuilder();
        if (node != null)
        {
            sb.AppendLine(NodeToString(node)); 
            sb.Append(TraversePreOrder(node.Left));
            sb.Append(TraversePreOrder(node.Right));
        }
        return sb.ToString();
    }

    public string PreOrder()
    {
        StringBuilder sb = new StringBuilder();
        if (Root == null)
        {
            sb.Append("TREE is EMPTY");
        }
        else
        {
            sb.Append(TraversePreOrder(Root));
        }
        return sb.ToString();
    }
    //In Order
    private string TraverseInOrder(Node node)
    {
        StringBuilder sb = new StringBuilder();
        if (node != null)
        {
            sb.Append(TraverseInOrder(node.Left));
            sb.AppendLine(NodeToString(node));
            sb.Append(TraverseInOrder(node.Right));
        }
        return sb.ToString();
    }

    public string InOrder()
    {
        StringBuilder sb = new StringBuilder();
        if (Root == null)
        {
            sb.Append("TREE is EMPTY");
        }
        else
        {
            sb.Append(TraverseInOrder(Root));
        }
        return sb.ToString();
    }
    //Post Order
    private string TraversePostOrder(Node node)
    {
        StringBuilder sb = new StringBuilder();
        if (node != null)
        {
            sb.Append(TraversePostOrder(node.Left));
            sb.Append(TraversePostOrder(node.Right));
            sb.AppendLine(NodeToString(node));
        }
        return sb.ToString();
    }

    public string PostOrder()
    {
        StringBuilder sb = new StringBuilder();
        if (Root == null)
        {
            sb.Append("TREE is EMPTY");
        }
        else
        {
            sb.Append(TraversePostOrder(Root));
        }
        return sb.ToString();
    }
    #endregion

    #region Search Operation
    private Node Search(Node tree, Node node)
    {
        if (tree != null)
        {//1. have not reached the end of a brancj
            if (node.Word == tree.Word)
            {//2. found node
                return tree;
            }
            if (string.Compare(node.Word, tree.Word) < 0)
            {//3. traverse left side
                return Search(tree.Left, node);
            }
            else
            {//4. traverse right side
                return Search(tree.Right, node);
            }

        }
        return null;
    }

    public string Find(string word)
    {//UI Method call
        Node node = new Node(word, word.Length);
        node = Search(Root, node);
        if (node != null)
        {
            return "Target: " + word.ToString() + ", \n NODE found => " + NodeToString(node);

        }
        else
        {
            return "Target: " + word.ToString() + ", \n NODE NOT found or Tree is empty.";
        }
    }
    #endregion

    #region Delete Operation
    private Node Delete(Node current, Node target)
    {
        Node parent = null; //pivot node
        if (current == null)
        {// reached bottom of tree path, reverse stack order
            return null;
        }
        else
        {
            //Refer to code snippet 2
            if (string.Compare(target.Word, current.Word) < 0)
            {
                current.Left = Delete(current.Left, target);
                if (BalanceFactor(current) == -2)
                {//after possible deletion have to check and rebalance tree
                    if (BalanceFactor(current.Right) <= 0)
                    {
                        current = RotateRR(current);
                    }
                    else
                    {
                        current = RotateRL(current);
                    }
                }
            }
            else if (string.Compare(target.Word, current.Word) > 0)
            {// traverse right side of sub-tree

                current.Right = Delete(current.Right, target);
                if (BalanceFactor(current) == 2)
                {//after possible deletion have to check and rebalance tree
                    if (BalanceFactor(current.Left) >= 0)
                    {
                        current = RotateLL(current);
                    }
                    else
                    {
                        current = RotateLR(current);
                    }
                }
            }
            else
            { // target found!
                if (current.Right != null)
                {   // delete its inorder successor, similar to BST deletion.
                    // Find smallest value node on the right side of tree.
                    parent = current.Right;
                    while (parent.Left != null)
                    {
                        parent = parent.Left;
                    }
                    current.Word = parent.Word;
                    current.Right = Delete(current.Right, parent);
                    if (BalanceFactor(current) == 2)
                    {// must rebalance tree
                        if (BalanceFactor(current.Left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {  //left side not null
                    return current.Left;
                }

            }
        }
        return current;
    }

    public string Remove(string word)
    {//UI Method Call
        Node node = new Node(word, word.Length);
        node = Search(Root, node); // Optional

        if (node != null)
        {
            Root = Delete(Root, node);
            return "Target Word: " + word.ToString() + " => NODE removed.";

        }
        else
        {
            return "Target Word: " + word.ToString() + ", NODE NOT found or Tree empty.";
        }

    }
    #endregion

    #region TreeDetails 
    private int MaxTreeDepth(Node tree)
    {
        if (tree == null) return 0;
        int left = MaxTreeDepth(tree.Left);
        int right = MaxTreeDepth(tree.Right);

        return Math.Max(left, right) + 1;
    }

    public string TreeDetails()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("******* Tree Details *******");
        sb.AppendLine($"- Root node:   " + Root.ToString());
        sb.AppendLine($"- Max Tree Depth:   " + MaxTreeDepth(Root));
       

        return sb.ToString();
    }
    #endregion

    #region Count nodes
    public int GetNodeCount()
    {//UI Call
        return CountNodes(Root);
    }

    private int CountNodes(Node node)
    {
        if (node == null)
            return 0;

        return 1 + CountNodes(node.Left) + CountNodes(node.Right);
    }
    #endregion

    #region Node Height and Depth Operations
     public int NodeDepth(string word)
    {
        Node node = new Node(word, word.Length);
        return GetNodeDepth(Root, node);
    }


    private int GetNodeDepth(Node tree, Node node)
    {
        if (tree == null)
            return -1; // Node not found

        if (node.Word == tree.Word)
            return GetDepth(tree);

        if (string.Compare(node.Word, tree.Word) < 0)
            return GetNodeDepth(tree.Left, node);
        else
            return GetNodeDepth(tree.Right, node);
    }
    private int GetDepth(Node node)
    {
        if (node == null)
            return -1;

        int leftHeight = GetDepth(node.Left);
        int rightHeight = GetDepth(node.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }

    public int NodeHeight(string word)
    {
        Node node = new Node(word, word.Length);
        return GetNodeHeight(Root, node, 0);
    }

    private int GetNodeHeight(Node tree, Node node, int depth)
    {
        if (tree == null)
            return -1; // Node not found

        if (node.Word == tree.Word)
            return depth;

        if (string.Compare(node.Word, tree.Word) < 0)
            return GetNodeHeight(tree.Left, node, depth + 1);
        else
            return GetNodeHeight(tree.Right, node, depth + 1);
    }

    #endregion
    private string NodeToString(Node node)
    {
        int height = GetNodeHeight(Root, node, 0);
        int depth = GetNodeDepth(Root, node);
        return $"Word: {node.Word,-10}, Length: {node.ALength,-10}, Height: {height,-10}, Depth: {depth,-10}";
    }

}



