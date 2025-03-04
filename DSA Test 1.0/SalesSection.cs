using System;

namespace InventoryManagementSystem
{
    public class SalesSection
    {
        private CheckedOutItems checkedOutItems;

        public SalesSection(CheckedOutItems checkedOutItems)
        {
            this.checkedOutItems = checkedOutItems;
        }

        public void DisplaySalesMenu()
        {
            Console.Clear();
            Console.WriteLine("======= SALES SECTION =======");
            Console.WriteLine("1. Display All Sales");
            Console.WriteLine("2. Go Back to Main Menu");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    DisplaySales();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey();
                    DisplaySalesMenu();
                    break;
            }
        }

        private void DisplaySales()
        {
            Console.Clear();
            Console.WriteLine("======= SALES REPORT =======");

            CheckedOutItems tempSalesList = checkedOutItems.LoadFromFile();

            // ✅ Measure sorting time
            double executionTime = BubbleSortSales.MeasureExecutionTime(tempSalesList);

            Console.WriteLine($"(Sorting Execution Time: {executionTime:F6} ms)"); // Display with 6 decimal places
            Console.WriteLine("ID   Name             Quantity   Price   Total Price   Sold Date");
            Console.WriteLine("----------------------------------------------------------------");

            var current = tempSalesList.Head;
            double totalSales = 0;
            while (current != null)
            {
                double totalPrice = current.Quantity * current.Price;
                totalSales += totalPrice;
                Console.WriteLine($"{current.ID,-4} {current.Name,-15} {current.Quantity,-9} ${current.Price,-7} ${totalPrice,-11} {current.SoldDate.ToShortDateString()}");
                current = current.Next;
            }

            Console.WriteLine("\n=====================================");
            Console.WriteLine($"TOTAL SALES REVENUE: ${totalSales}");
            Console.WriteLine("=====================================");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
