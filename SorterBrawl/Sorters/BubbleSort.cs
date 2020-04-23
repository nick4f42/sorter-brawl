using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    class BubbleSort : Sorter
    {
        public BubbleSort(SorterTheme theme = null) : base(theme) { }

        public override void Sort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                bool sorted = true;
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    OnFlaggedIndices(this, new FlagList()
          {
            new Tuple<int, FlagType>(j, FlagType.Compared),
            new Tuple<int, FlagType>(j + 1, FlagType.Compared)
          });
                    if (array[j] > array[j + 1])
                    {
                        sorted = false;
                        SwapElements(array, j, j + 1);
                    }
                }
                if (sorted)
                    return;
            }
        }
    }
}
