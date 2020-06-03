using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    class BubbleSortReverse : Sorter
    {
        public BubbleSortReverse(SorterTheme theme = null) : base(theme, true) { }

        public override void Sort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                bool sorted = true;
                for (int j = array.Length - 1; j > i; j--)
                {
                    OnFlaggedIndices(this, new FlagList()
                    {
                        new Tuple<int, FlagType>(j, FlagType.Compared),
                        new Tuple<int, FlagType>(j - 1, FlagType.Compared)
                    });

                    if (array[j] > array[j - 1])
                    {
                        sorted = false;
                        SwapElements(array, j, j - 1);
                    }
                }
                if (sorted)
                    return;
            }
        }
    }
}
