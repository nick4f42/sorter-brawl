using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    class MergeSortReverse : Sorter
    {
        public MergeSortReverse(SorterTheme theme = null) : base(theme, true) { }

        public override void Sort(int[] array)
        {
            _Sort(array, 0, array.Length);
        }

        void _Sort(int[] array, int left, int right)
        {
            if (left > right - 2)
                return;

            int middle = left + (right - left) / 2;
            _Sort(array, middle, right);
            _Sort(array, left, middle);

            Merge(array, left, middle, right);
        }

        void Merge(int[] array, int left, int middle, int right)
        {
            for (int i = right - 1; i >= middle; i--)
            {
                OnFlaggedIndices(this, new FlagList()
                {
                    new Tuple<int, FlagType>(i, FlagType.Compared),
                    new Tuple<int, FlagType>(middle, FlagType.Pivot)
                });

                if (array[i] > array[middle - 1])
                {
                    SwapElements(array, i, middle - 1);
                    for (int j = middle - 1; j > left && array[j] > array[j - 1]; j--)
                        SwapElements(array, j, j - 1);
                }
            }
        }
    }
}
