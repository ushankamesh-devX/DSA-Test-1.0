using System;

namespace InventoryManagementSystem
{
    public class ExpireSection
    {
        private Store store;

        public ExpireSection(Store store)
        {
            this.store = store;
        }

        public void DisplayExpireMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========== EXPIRE SECTION ===========");
                Console.WriteLine("1. Find Expired Items by Date");
                Console.WriteLine("2. Go Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        FindExpiredItems();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void FindExpiredItems()
        {
            Console.Write("Enter the date (yyyy-mm-dd) to check expired items: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime inputDate))
            {
                Console.WriteLine("Invalid date format. Try again.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"User entered date: {inputDate.ToShortDateString()}");

            AVLTree expiredTree = new AVLTree();
            store.FindExpiredItems(inputDate, expiredTree);

            Console.Clear();
            expiredTree.DisplayExpiredItems();

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

    }
}
