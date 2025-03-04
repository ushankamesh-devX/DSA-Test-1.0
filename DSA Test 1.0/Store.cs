using System;
using System.IO;
using System.Text;

namespace InventoryManagementSystem
{
    public class Store
    {
        private Item head;
        private const string FilePath = "store_inventory.txt";

        public Item Head { get { return head; } set { head = value; } }

        public Store()
        {
            head = null;
            LoadInventoryFromFile(); // load data file on startup
        }

        // add a new item to sottre
        public void AddItem(int id, string name, DateTime expireDate, int quantity, double price, DateTime buyDate, string dealer)
        {
            Item newItem = new Item(id, name, expireDate, quantity, price, buyDate, dealer);

            if (head == null)
            {
                head = newItem;
            }
            else
            {
                Item current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newItem;
            }

            SaveInventoryToFile();
            Console.WriteLine($"Item {name} added successfully!");
        }

        // remove  item by id
        public void RemoveItem(int id)
        {
            if (head == null)
            {
                Console.WriteLine("No items in inventory.");
                return;
            }

            if (head.ID == id)
            {
                head = head.Next;
                SaveInventoryToFile();
                Console.WriteLine($"Item with ID {id} removed.");
                return;
            }

            Item current = head;
            Item previous = null;

            while (current != null && current.ID != id)
            {
                previous = current;
                current = current.Next;
            }

            if (current == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            previous.Next = current.Next;
            SaveInventoryToFile();
            Console.WriteLine($"Item with ID {id} removed.");
        }

        // display all itms
        public void DisplayInventory()
        {
            if (head == null)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            Console.WriteLine("\n======= Store Inventory =======");
            Console.WriteLine("ID\tName\tExpire Date\tQuantity\tPrice\tBuy Date\tDealer");

            Item current = head;
            while (current != null)
            {
                Console.WriteLine($"{current.ID}\t{current.Name}\t{current.ExpireDate.ToShortDateString()}\t{current.Quantity}\t{current.Price}\t{current.BuyDate.ToShortDateString()}\t{current.Dealer}");
                current = current.Next;
            }
        }
        //find expire 

        public void FindExpiredItems(DateTime inputDate, AVLTree expiredTree)
        {
            Item current = head;
            bool found = false; // check if any epired available

            while (current != null)
            {
                Console.WriteLine($"Checking: {current.Name} - Expiry Date: {current.ExpireDate.ToShortDateString()}");

                if (current.ExpireDate <= inputDate) // check wheather expired
                {
                    Console.WriteLine($"Adding to AVL Tree: {current.Name} (Expired on {current.ExpireDate.ToShortDateString()})");
                    expiredTree.AddExpiredItem(current);
                    found = true;
                }
                current = current.Next;
            }

            if (!found)
            {
                Console.WriteLine("No expired items found!");
            }
        }

        
        private void SaveInventoryToFile()
        {
            StringBuilder sb = new StringBuilder();
            Item current = head;

            while (current != null)
            {
                sb.AppendLine($"{current.ID},{current.Name},{current.ExpireDate.ToShortDateString()},{current.Quantity},{current.Price},{current.BuyDate.ToShortDateString()},{current.Dealer}");
                current = current.Next;
            }

            File.WriteAllText(FilePath, sb.ToString());
        }

        // load inventory from file
        private void LoadInventoryFromFile()
        {
            if (!File.Exists(FilePath))
                return;

            string[] lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 7)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    DateTime expireDate = DateTime.Parse(parts[2]);
                    int quantity = int.Parse(parts[3]);
                    double price = double.Parse(parts[4]);
                    DateTime buyDate = DateTime.Parse(parts[5]);
                    string dealer = parts[6];

                    AddItem(id, name, expireDate, quantity, price, buyDate, dealer);
                }
            }
        }

        // find by name or id e
        public Item FindItem(string input)
        {
            Item current = head;
            while (current != null)
            {
                if (current.ID.ToString() == input || current.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public void ReduceQuantity(int id, int quantity)
        {
            Item current = head;
            while (current != null)
            {
                if (current.ID == id)
                {
                    current.Quantity -= quantity;
                    if (current.Quantity < 0) current.Quantity = 0; //prevent negative qnt
                    SaveInventoryToFile(); // save updt inventory
                    return;
                }
                current = current.Next;
            }
        }



    }
}
