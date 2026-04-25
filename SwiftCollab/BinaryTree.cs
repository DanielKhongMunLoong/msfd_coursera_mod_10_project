using System;

public class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    // Stores node height for balancing.
    public int Height;

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
        Height = 1;
    }
}

public class BinaryTree
{
    public Node Root;

    // Public insert method.
    public void Insert(int value)
    {
        Root = InsertBalanced(Root, value);
    }

    // AVL insertion keeps the tree balanced after every insert.
    private Node InsertBalanced(Node node, int value)
    {
        if (node == null)
            return new Node(value);

        if (value < node.Value)
            node.Left = InsertBalanced(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertBalanced(node.Right, value);
        else
            return node; // Avoid duplicate priorities.

        // Update height after insertion.
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        // Check balance factor.
        int balance = GetBalance(node);

        // Left Left case.
        if (balance > 1 && value < node.Left.Value)
            return RotateRight(node);

        // Right Right case.
        if (balance < -1 && value > node.Right.Value)
            return RotateLeft(node);

        // Left Right case.
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Right Left case.
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    // Efficient search method: O(log n) average/well-balanced.
    public bool Search(int value)
    {
        Node current = Root;

        while (current != null)
        {
            if (value == current.Value)
                return true;

            if (value < current.Value)
                current = current.Left;
            else
                current = current.Right;
        }

        return false;
    }

    private int GetHeight(Node node)
    {
        return node == null ? 0 : node.Height;
    }

    private int GetBalance(Node node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }

    // Right rotation fixes left-heavy imbalance.
    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node temp = x.Right;

        x.Right = y;
        y.Left = temp;

        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

        return x;
    }

    // Left rotation fixes right-heavy imbalance.
    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node temp = y.Left;

        y.Left = x;
        x.Right = temp;

        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    // Prints values in sorted priority order.
    public void PrintInOrder(Node node)
    {
        if (node == null) return;

        PrintInOrder(node.Left);
        Console.Write(node.Value + " ");
        PrintInOrder(node.Right);
    }
}