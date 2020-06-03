using System;
using System.Collections.Generic;
using System.Linq;

using SorterBrawl.Sorters;

namespace SorterBrawl
{
    using FlagList = List<Tuple<int, FlagType>>;

    partial class Animator
    {
        /// <summary>
        /// Base class for making and saving visualizations.
        /// </summary>
        private abstract class Maker
        {
            public int FrameCount { get; protected set; }

            public int ComparisonCount { get; protected set; }


            protected string savePath;

            protected int[] array;

            protected int maxValue;
            protected int minValue;

            protected bool lastUpdate;

            protected Maker(string savePath, int[] array)
            {
                this.savePath = savePath;

                this.array = array;

                minValue = array.Min();
                maxValue = array.Max();
            }

            public virtual void FinalUpdate()
            {
                lastUpdate = true;
            }

            public abstract void Update(Sorter sender, FlagList flagList);

            public abstract void Finish();

            public abstract bool HasCompleted();
        }
    }
}
