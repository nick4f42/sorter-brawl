using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    class MergeSort : Sorter
    {
        public MergeSort(SorterTheme theme = null) : base(theme) { }

        public override void Sort(int[] array)
        {
            _Sort(array, 0, array.Length);
        }

        void _Sort(int[] array, int left, int right)
        {
            if (left > right - 2)
                return;

            int middle = left + (right - left) / 2;
            _Sort(array, left, middle);
            _Sort(array, middle, right);

            Merge(array, left, middle, right);
        }

        void Merge(int[] array, int left, int middle, int right)
        {
            for (int i = left; i < middle; i++)
            {
                OnFlaggedIndices(this, new FlagList()
                {
                    new Tuple<int, FlagType>(i, FlagType.Compared),
                    new Tuple<int, FlagType>(middle, FlagType.Pivot)
                });

                if (array[i] > array[middle])
                {
                    SwapElements(array, i, middle);
                    for (int j = middle; j < right - 1 && array[j] > array[j + 1]; j++)
                        SwapElements(array, j, j + 1);
                }
            }
        }
    }
}
