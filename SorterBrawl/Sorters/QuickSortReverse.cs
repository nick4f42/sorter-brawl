using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    class QuickSortReverse : Sorter
    {
        public QuickSortReverse(SorterTheme theme = null) : base(theme) { }

        public override void Sort(int[] array)
        {
            Sort(array, 0, array.Length);
        }

        public override bool IsSorted(int[] array)
        {
            return base.IsSorted(array, reverse: true);
        }

        void Sort(int[] array, int left, int right)
        {
            int pivot = Partition(array, left, right);
            if (pivot < right - 2)
                Sort(array, pivot + 1, right);
            if (pivot > left + 1)
                Sort(array, left, pivot);
        }

        int Partition(int[] array, int left, int right)
        {
            int median = FindApproxMedian(array, left, right, 5);
            lock (array)
                SwapElements(array, median, left);

            int i = right;
            for (int j = right - 1; j > left; j--)
            {
                OnFlaggedIndices(this, new FlagList()
                {
                    new Tuple<int, FlagType>(j, FlagType.Compared),
                    new Tuple<int, FlagType>(i, FlagType.Compared),
                    new Tuple<int, FlagType>(left, FlagType.Pivot)
                });

                if (array[j] <= array[left])
                {
                    i--;
                    lock (array)
                        SwapElements(array, i, j);
                }
            }
            lock (array)
                SwapElements(array, i - 1, left);

            return i - 1;
        }
    }
}
