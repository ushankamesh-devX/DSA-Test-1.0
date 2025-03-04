using System;
using System.Diagnostics;

namespace InventoryManagementSystem
{
    public static class BubbleSortSales
    {
        public static void SortByTotalPrice(CheckedOutItems salesList)
        {
            if (salesList.Head == null || salesList.Head.Next == null) return;

            bool swapped;
            do
            {
                swapped = false;
                CheckedOutItems.CheckedOutNode prev = null;
                CheckedOutItems.CheckedOutNode current = salesList.Head;
                CheckedOutItems.CheckedOutNode next = current.Next;

                while (next != null)
                {
                    double currentTotal = current.Quantity * current.Price;
                    double nextTotal = next.Quantity * next.Price;

                    if (currentTotal < nextTotal) // 🔹 Swap nodes
                    {
                        if (prev == null) // Swapping Head node
                        {
                            salesList.Head = next;
                        }
                        else
                        {
                            prev.Next = next;
                        }

                        current.Next = next.Next;
                        next.Next = current;

                        // 🔹 Update Pointers
                        prev = next;
                        next = current.Next;
                        swapped = true;
                    }
                    else
                    {
                        prev = current;
                        current = next;
                        next = next.Next;
                    }
                }
            } while (swapped);
        }

        // ✅ High-Precision Execution Time Measurement
        public static double MeasureExecutionTime(CheckedOutItems salesList)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            SortByTotalPrice(salesList);
            stopwatch.Stop();
            return (double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000; // Convert to milliseconds
        }
    }
}
