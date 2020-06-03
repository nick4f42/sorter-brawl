using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using SorterBrawl.Frames;

namespace SorterBrawl.Sorters
{
    using FlagList = List<Tuple<int, FlagType>>;

    enum FlagType
    {
        None,
        Compared,
        Pivot,
        InPlace,
        SweepCheck,
        SweepFailed
    }

    struct FlagData
    {
        public Sorter sorter;
        public FlagType flagType;
    }

    abstract class Sorter
    {
        public SorterTheme Theme { get; set; }

        public Sorter(SorterTheme theme = null, bool reverse = false)
        {
            Theme = theme ?? new SorterTheme();

            IsSorted = (int[] array) =>
            {
                if (reverse)
                    return _IsSortedReverse(array);
                else
                    return _IsSorted(array);
            };
        }

        public delegate void FlagHandler(Sorter sender, FlagList flags);

        public event FlagHandler FlagIndices;

        protected virtual void OnFlaggedIndices(Sorter sender, FlagList flags)
        {
            FlagIndices?.Invoke(sender, flags);
        }

        public abstract void Sort(int[] array);

        public Func<int[], bool> IsSorted;

        private bool _IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                bool inOrder = array[i] <= array[i + 1];

                FlagType flagType = inOrder ? FlagType.SweepCheck : FlagType.SweepFailed;

                OnFlaggedIndices(this, new FlagList()
                {
                    new Tuple<int, FlagType>(i, flagType),
                    new Tuple<int, FlagType>(i + 1, flagType)
                });

                if (!inOrder)
                    return false;
            }

            return true;
        }

        private bool _IsSortedReverse(int[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                bool inOrder = array[i] <= array[i - 1];

                FlagType flagType = inOrder ? FlagType.SweepCheck : FlagType.SweepFailed;

                OnFlaggedIndices(this, new FlagList()
                    {
                        new Tuple<int, FlagType>(i, flagType),
                        new Tuple<int, FlagType>(i - 1, flagType)
                    });

                if (!inOrder)
                    return false;
            }

            return true;
        }

        protected static void SwapElements(int[] array, int a, int b)
        {
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        protected static int FindApproxMedian(int[] array, int left, int right, int n)
        {
            Tuple<int, int>[] samples = new Tuple<int, int>[n];

            for (int i = 0; i < n; i++)
            {
                int mainIndex = left + (right - 1 - left) * i / n;
                samples[i] = new Tuple<int, int>(mainIndex, array[mainIndex]);
            }

            Array.Sort(samples, (a, b) => a.Item2.CompareTo(b.Item2));

            return samples[n / 2].Item1;
        }
    }
}
