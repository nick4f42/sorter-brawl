using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using SorterBrawl.Sorters;

namespace SorterBrawl.Frames
{
    struct DrawData
    {
        public Graphics graphics;

        public Sorter sorter;

        public readonly int width;
        public readonly int height;

        public readonly int i;
        public readonly int length;

        public readonly int value;
        public readonly int minValue;
        public readonly int maxValue;

        public readonly FlagType flagType;

        public DrawData(Graphics graphics, Sorter sorter, int width, int height, int i, int length,
                        int value, int minValue, int maxValue, FlagType flagType)
        {
            this.graphics = graphics;

            this.sorter = sorter;

            this.width = width;
            this.height = height;

            this.i = i;
            this.length = length;

            this.value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;

            this.flagType = flagType;
        }
    }
}
