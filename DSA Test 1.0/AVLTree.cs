using System;

namespace InventoryManagementSystem
{
    public class AVLTree
    {
        private class Node
        {
            public Item Data;
            public Node Left, Right;
            public int Height;

            public Node(Item data)
            {
                Data = data;
                Left = Right = null;
                Height = 1;
            }
        }

        private Node root;

        // ✅ Get Height of a Node
        private int Height(Node node) => node == null ? 0 : node.Height;

        // ✅ Get Balance Factor
        private int GetBalance(Node node) => node == null ? 0 : Height(node.Left) - Height(node.Right);

        // ✅ Rotate Right
        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        // ✅ Rotate Left
        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        // ✅ Insert Expired Item (Sorted by ExpireDate DESCENDING)
        private Node Insert(Node node, Item data)
        {
            if (node == null) return new Node(data);

            if (data.ExpireDate > node.Data.ExpireDate)  // Most recent expired first
                node.Left = Insert(node.Left, data);
            else
                node.Right = Insert(node.Right, data);

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            // Left Heavy (Right Rotation)
            if (balance > 1 && data.ExpireDate > node.Left.Data.ExpireDate)
                return RotateRight(node);

            // Right Heavy (Left Rotation)
            if (balance < -1 && data.ExpireDate <= node.Right.Data.ExpireDate)
                return RotateLeft(node);

            // Left-Right Case (Left Rotation + Right Rotation)
            if (balance > 1 && data.ExpireDate <= node.Left.Data.ExpireDate)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Right-Left Case (Right Rotation + Left Rotation)
            if (balance < -1 && data.ExpireDate > node.Right.Data.ExpireDate)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public void AddExpiredItem(Item item)
        {
            root = Insert(root, item);
            Console.WriteLine($"✅ Added to AVL Tree: {item.Name} (Expired on {item.ExpireDate.ToShortDateString()})");
        }


        // ✅ Display Expired Items (Most Recent First)
        public void DisplayExpiredItems()
        {
            if (root == null)
            {
                Console.WriteLine("No expired items.");
                return;
            }
            Console.WriteLine("\n======= Expired Items (Recent to Oldest) =======");
            Console.WriteLine("ID\tName\tExpire Date\tQuantity\tDealer");
            InOrderDisplay(root);
        }

        private void InOrderDisplay(Node node)
        {
            if (node == null) return;

            InOrderDisplay(node.Left);
            Console.WriteLine($"{node.Data.ID}\t{node.Data.Name}\t{node.Data.ExpireDate.ToShortDateString()}\t{node.Data.Quantity}\t{node.Data.Dealer}");
            InOrderDisplay(node.Right);
        }
    }
}
