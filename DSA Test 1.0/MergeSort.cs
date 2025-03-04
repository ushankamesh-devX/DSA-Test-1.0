using System;
using System.Diagnostics;

namespace InventoryManagementSystem
{
    public static class MergeSort
    {
        public static Item Sort(Item head)
        {
            if (head == null || head.Next == null)
                return head;

            Item middle = GetMiddle(head);
            Item nextToMiddle = middle.Next;
            middle.Next = null;

            Item left = Sort(head);
            Item right = Sort(nextToMiddle);

            return Merge(left, right);
        }

        private static Item Merge(Item left, Item right)
        {
            if (left == null) return right;
            if (right == null) return left;

            if (left.Quantity >= right.Quantity)
            {
                left.Next = Merge(left.Next, right);
                return left;
            }
            else
            {
                right.Next = Merge(left, right.Next);
                return right;
            }
        }

        private static Item GetMiddle(Item head)
        {
            if (head == null) return head;

            Item slow = head, fast = head.Next;
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }

        // time measure with tick
        public static double MeasureExecutionTime(Item head)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sort(head);
            stopwatch.Stop();
            return (double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000;
        }
    }
}
