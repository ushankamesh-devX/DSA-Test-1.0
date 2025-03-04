using System;

namespace InventoryManagementSystem
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime BuyDate { get; set; }
        public string Dealer { get; set; }
        public Item Next { get; set; } // Pointer to next node in LinkedList

        public Item(int id, string name, DateTime expireDate, int quantity, double price, DateTime buyDate, string dealer)
        {
            ID = id;
            Name = name;
            ExpireDate = expireDate;
            Quantity = quantity;
            Price = price;
            BuyDate = buyDate;
            Dealer = dealer;
            Next = null; // Initially, next is null
        }
    }
}
