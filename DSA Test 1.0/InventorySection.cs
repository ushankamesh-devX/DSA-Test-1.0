using System;

namespace InventoryManagementSystem
{
    public class InventorySection
    {
        private Store store;

        public InventorySection(Store store)
        {
            this.store = store;
        }

        public void DisplayInventoryMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========== INVENTORY SECTION ===========");
                Console.WriteLine("1. Show All Items");
                Console.WriteLine("2. Search for an Item");
                Console.WriteLine("3. Add New Item");
                Console.WriteLine("4. Delete Item by ID");
                Console.WriteLine("5. Go Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAllItems();
                        break;
                    case "2":
                        SearchItem();
                        break;
                    case "3":
                        AddNewItem();
                        break;
                    case "4":
                        DeleteItemById();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }


        //inventory display method
        private void ShowAllItems()
        {
            Console.Clear();
            if (store.Head == null)
            {
                Console.WriteLine("Inventory is empty.");
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                return;
            }

            //measure sort time with ticks
            double executionTime = MergeSort.MeasureExecutionTime(store.Head);
            store.Head = MergeSort.Sort(store.Head);

            Console.WriteLine("\n======= Sorted Store Inventory (By Quantity) =======");
            Console.WriteLine($"(Sorting Execution Time: {executionTime:F6} ms)");
            Console.WriteLine("ID\tName\tExpire Date\tQuantity\tPrice\tBuy Date\tDealer");

            Item current = store.Head;
            while (current != null)
            {
                Console.WriteLine($"{current.ID}\t{current.Name}\t{current.ExpireDate.ToShortDateString()}\t{current.Quantity}\t{current.Price}\t{current.BuyDate.ToShortDateString()}\t{current.Dealer}");
                current = current.Next;
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void DeleteItemById()
        {
            Console.Clear();
            Console.WriteLine("======= DELETE ITEM =======");

            try
            {
                Console.Write("Enter Item ID to delete: ");
                int id = int.Parse(Console.ReadLine());

                //call delete method
                store.RemoveItem(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }



        // search for item by id or name
        private void SearchItem()
        {
            Console.Write("Enter Item ID or Name to search: ");
            string input = Console.ReadLine();

            Item foundItem = store.FindItem(input);
            if (foundItem != null)
            {
                Console.Clear();
                Console.WriteLine("\n======= ITEM DETAILS =======");
                Console.WriteLine($"ID          : {foundItem.ID}");
                Console.WriteLine($"Name        : {foundItem.Name}");
                Console.WriteLine($"Expire Date : {foundItem.ExpireDate.ToShortDateString()}");
                Console.WriteLine($"Quantity    : {foundItem.Quantity}");
                Console.WriteLine($"Price       : ${foundItem.Price}");
                Console.WriteLine($"Buy Date    : {foundItem.BuyDate.ToShortDateString()}");
                Console.WriteLine($"Dealer      : {foundItem.Dealer}");
                Console.WriteLine("============================\n");
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        private void AddNewItem()
        {
            Console.Clear();
            Console.WriteLine("======= ADD NEW ITEM =======");

            try
            {
                Console.Write("Enter Item ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter Item Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Expiry Date (yyyy-mm-dd): ");
                DateTime expireDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("Enter Price: ");
                double price = double.Parse(Console.ReadLine());

                Console.Write("Enter Buy Date (yyyy-mm-dd): ");
                DateTime buyDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Dealer: ");
                string dealer = Console.ReadLine();

                store.AddItem(id, name, expireDate, quantity, price, buyDate, dealer);
                Console.WriteLine("\n item added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

    }
}
