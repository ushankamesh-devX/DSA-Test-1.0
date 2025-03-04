using System;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store(); // Create Store object
            InventorySection inventorySection = new InventorySection(store); // Create Inventory Section
            ExpireSection expireSection = new ExpireSection(store);
            CheckedOutItems checkedOutItems = new CheckedOutItems();
            BillingSection billingSection = new BillingSection(store, checkedOutItems);
            SalesSection salesSection = new SalesSection(checkedOutItems);
            //Console.WriteLine($"Looking for file at: {Path.GetFullPath(FilePath)}");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("         WELCOME TO SYSTEM         ");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Expire Section");
                Console.WriteLine("2. Inventory Section");
                Console.WriteLine("3. Billing Section");
                Console.WriteLine("4. Sales Section");
                Console.WriteLine("5. Exit");
                Console.WriteLine("====================================");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        expireSection.DisplayExpireMenu();
                        break;
                    case "2":
                        inventorySection.DisplayInventoryMenu(); // Open Inventory Section
                        break;
                    case "3":
                        billingSection.StartBillingProcess();
                        break;
                    case "4":
                        salesSection.DisplaySalesMenu();
                        break;
                    case "5":
                        Console.WriteLine("Exiting the system... Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ExpireSection()
        {
            Console.Clear();
            Console.WriteLine("=== Expire Section ===");
            Console.WriteLine("Manage product expiry details here.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void InventorySection()
        {
            Console.Clear();
            Console.WriteLine("=== Inventory Section ===");
            Console.WriteLine("Manage and track inventory details here.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void BillingSection()
        {
            Console.Clear();
            Console.WriteLine("=== Billing Section ===");
            Console.WriteLine("Process and manage billing details here.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void SalesSection()
        {
            Console.Clear();
            Console.WriteLine("=== Sales Section ===");
            Console.WriteLine("Track and analyze sales records here.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
