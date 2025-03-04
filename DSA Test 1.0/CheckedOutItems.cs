using System;
using System.IO;

namespace InventoryManagementSystem
{
    public class CheckedOutItems
    {
        public class CheckedOutNode
        {
            public int ID;
            public string Name;
            public int Quantity;
            public double Price;
            public DateTime SoldDate;
            public CheckedOutNode Next;

            public CheckedOutNode(int id, string name, int quantity, double price, DateTime soldDate)
            {
                ID = id;
                Name = name;
                Quantity = quantity;
                Price = price;
                SoldDate = soldDate;
                Next = null;
            }
        }

        public CheckedOutNode Head { get;  set; } //head public

        private const string FilePath = "checked_out_items.txt";

        public void AddCheckedOutItem(int id, string name, int quantity, double price, DateTime soldDate)
        {
            CheckedOutNode newNode = new CheckedOutNode(id, name, quantity, price, soldDate);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                CheckedOutNode temp = Head;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = newNode;
            }
        }

        public void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                CheckedOutNode current = Head;
                while (current != null)
                {
                    writer.WriteLine($"{current.ID},{current.Name},{current.Quantity},{current.Price},{current.SoldDate:yyyy-MM-dd}");
                    current = current.Next;
                }
            }
        }

        public CheckedOutItems LoadFromFile()
        {
            CheckedOutItems salesList = new CheckedOutItems();

            if (!File.Exists(FilePath))
            {
                Console.WriteLine("No sales data found.");
                return salesList;
            }

            string[] lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 5)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    int quantity = int.Parse(parts[2]);
                    double price = double.Parse(parts[3]);
                    DateTime soldDate = DateTime.Parse(parts[4]);

                    salesList.AddCheckedOutItem(id, name, quantity, price, soldDate); //apend in order
                }
            }

            return salesList;
        }
    }
}
