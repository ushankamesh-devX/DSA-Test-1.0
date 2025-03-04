using System;
using System.Collections.Generic;

namespace InventoryManagementSystem
{
    public class BillingSection
    {
        private Store store;
        private CheckedOutItems checkedOutItems;
        private List<(int ID, string Name, int AvailableQty, int SelectedQty)> selectedItems;
        private string prescription = "";

        public BillingSection(Store store, CheckedOutItems checkedOutItems)
        {
            this.store = store;
            this.checkedOutItems = checkedOutItems;
            this.selectedItems = new List<(int, string, int, int)>();
        }

        public void StartBillingProcess()
        {
            Console.Clear();
            Console.WriteLine("======= BILLING SECTION =======");

            // Step 1: Add Prescription
            AddPrescription();

            // Step 2: Show Available Items
            ShowAvailableItems();

            // Step 3: Select Quantity
            SelectQuantity();

            // Step 4: Confirm Purchase
            ConfirmPurchase();

            // Step 5: Checkout
            Checkout();
        }

        private void AddPrescription()
        {
            Console.Clear();
            Console.WriteLine("======= ADD PRESCRIPTION =======");
            Console.Write("Enter Prescription (e.g., Paracetamol Ibuprofen): ");
            prescription = Console.ReadLine();
            Console.WriteLine("\n✅ Prescription Added: " + prescription);
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();
        }

        private void ShowAvailableItems()
        {
            Console.Clear();
            Console.WriteLine("======= AVAILABLE ITEMS =======");
            Console.WriteLine("ID\tName\t\tQuantity");

            string[] requestedItems = prescription.Split(' ');

            foreach (string itemName in requestedItems)
            {
                Item foundItem = store.FindItem(itemName);
                if (foundItem != null)
                {
                    selectedItems.Add((foundItem.ID, foundItem.Name, foundItem.Quantity, 0)); // Default SelectedQty = 0
                    Console.WriteLine($"{foundItem.ID}\t{foundItem.Name}\t\t{foundItem.Quantity}");
                }
                else
                {
                    Console.WriteLine($"❌ Item '{itemName}' not found in inventory.");
                }
            }

            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();
        }

        private void SelectQuantity()
        {
            Console.Clear();
            Console.WriteLine("======= SELECT QUANTITY =======");

            for (int i = 0; i < selectedItems.Count; i++)
            {
                var (id, name, availableQty, _) = selectedItems[i];

                Console.Write($"{name} (Available: {availableQty}) - Enter Quantity: ");
                int selectedQty;
                while (!int.TryParse(Console.ReadLine(), out selectedQty) || selectedQty <= 0 || selectedQty > availableQty)
                {
                    Console.Write("❌ Invalid input. Enter a valid quantity: ");
                }

                selectedItems[i] = (id, name, availableQty, selectedQty); // Update selected quantity
            }

            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();
        }

        private void ConfirmPurchase()
        {
            Console.Clear();
            Console.WriteLine("======= CONFIRM PURCHASE =======");
            Console.WriteLine("ID\tName\tQuantity");

            foreach (var (id, name, _, selectedQty) in selectedItems)
            {
                Console.WriteLine($"{id}\t{name}\t{selectedQty}");
            }

            Console.Write("\nConfirm purchase? (Y/N): ");
            string confirmation = Console.ReadLine();
            if (confirmation.ToUpper() != "Y")
            {
                Console.WriteLine("\n❌ Purchase Cancelled. Returning to Billing Section.");
                Console.ReadKey();
                return;
            }

            // Reduce inventory stock
            foreach (var (id, _, _, selectedQty) in selectedItems)
            {
                store.ReduceQuantity(id, selectedQty);
            }

            Console.WriteLine("\n✅ Purchase Confirmed! Proceeding to Checkout.");
            Console.WriteLine("\nPress any key to proceed...");
            Console.ReadKey();
        }

        private void Checkout()
        {
            Console.Clear();
            Console.WriteLine("======= CHECKOUT =======");
            Console.WriteLine("ID\tName\tQuantity\tPrice\tSold Date");

            DateTime soldDate = DateTime.Now;
            foreach (var (id, name, _, selectedQty) in selectedItems)
            {
                Item foundItem = store.FindItem(id.ToString());
                if (foundItem != null)
                {
                    double totalPrice = selectedQty * foundItem.Price;
                    checkedOutItems.AddCheckedOutItem(id, foundItem.Name, selectedQty, totalPrice, soldDate);
                    Console.WriteLine($"{id}\t{foundItem.Name}\t{selectedQty}\t${totalPrice}\t{soldDate.ToShortDateString()}");
                }
            }

            checkedOutItems.SaveToFile();
            selectedItems.Clear(); // Clear temporary list after checkout
            Console.WriteLine("\n✅ Checkout Successful!");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
