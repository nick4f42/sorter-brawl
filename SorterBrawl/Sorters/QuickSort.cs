using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    class QuickSort : Sorter
    {
        public QuickSort(SorterTheme theme = null) : base(theme) { }

        public override void Sort(int[] array)
        {
            Sort(array, 0, array.Length);
        }

        void Sort(int[] array, int left, int right)
        {
            int pivot = Partition(array, left, right);
            if (pivot > left + 1)
                Sort(array, left, pivot);
            if (pivot < right - 2)
                Sort(array, pivot + 1, right);
        }

        int Partition(int[] array, int left, int right)
        {
            int median = FindApproxMedian(array, left, right, 5);
            lock (array)
                SwapElements(array, median, right - 1);

            int i = left - 1;
            for (int j = left; j < right - 1; j++)
            {
                OnFlaggedIndices(this, new FlagList()
        {
          new Tuple<int, FlagType>(j, FlagType.Compared),
          new Tuple<int, FlagType>(i, FlagType.Compared),
          new Tuple<int, FlagType>(right - 1, FlagType.Pivot)
        });
                if (array[j] <= array[right - 1])
                {
                    i++;
                    lock (array)
                        SwapElements(array, i, j);
                }
            }
            lock (array)
                SwapElements(array, i + 1, right - 1);

            return i + 1;
        }
    }
}
