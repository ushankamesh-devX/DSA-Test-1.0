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

       
        private int Height(Node node) => node == null ? 0 : node.Height;

        
        private int GetBalance(Node node) => node == null ? 0 : Height(node.Left) - Height(node.Right);

        // Rotete Right
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

        //  Rote Left
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

        //  insert expired item decending order by expire date
        private Node Insert(Node node, Item data)
        {
            if (node == null) return new Node(data);

            if (data.ExpireDate > node.Data.ExpireDate)  // show recnt expired first
                node.Left = Insert(node.Left, data);
            else
                node.Right = Insert(node.Right, data);

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            //  right Rot
            if (balance > 1 && data.ExpireDate > node.Left.Data.ExpireDate)
                return RotateRight(node);

            //  left rot
            if (balance < -1 && data.ExpireDate <= node.Right.Data.ExpireDate)
                return RotateLeft(node);

            //  left Rote + right rota
            if (balance > 1 && data.ExpireDate <= node.Left.Data.ExpireDate)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // right rotation + left rota
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
            Console.WriteLine($"Added to AVL Tree: {item.Name} (Expired on {item.ExpireDate.ToShortDateString()})");
        }


        // display expird items ascend
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
