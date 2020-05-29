using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using SorterBrawl.Sorters;

namespace SorterBrawl.Frames
{
    struct DrawData
    {
        public readonly Graphics graphics;

        public readonly Sorter sorter;

        public readonly FrameProfile profile;

        public readonly int i;
        public readonly int length;

        public readonly int value;
        public readonly int minValue;
        public readonly int maxValue;

        public readonly FlagType flagType;

        public DrawData(Graphics graphics, Sorter sorter, FrameProfile profile, int i, int length,
                        int value, int minValue, int maxValue, FlagType flagType)
        {
            this.graphics = graphics;

            this.sorter = sorter;

            this.profile = profile;

            this.i = i;
            this.length = length;

            this.value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;

            this.flagType = flagType;
        }
    }
}
