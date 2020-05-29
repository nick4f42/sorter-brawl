using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using SorterBrawl.Sorters;

namespace SorterBrawl.Frames.Stylers
{
    class BarStyler : IStyler
    {
        public Color background = Color.Black;

        public BarStyler() { }

        public BarStyler(Color background)
        {
            this.background = background;
        }

        public void Clear(Graphics graphics)
        {
            lock (graphics)
                graphics.Clear(background);
        }

        public void DrawElement(DrawData data)
        {
            int x = (int)((double)data.i / data.length * data.profile.Width);

            int rectWidth = (int)((double)(data.i + 1) / data.length * data.profile.Width) - x;

            int rectHeight = data.profile.Height
                * (data.value - data.minValue + 1) / (data.maxValue - data.minValue + 1);

            Color color = IStyler.GetFlagColor(data.sorter?.Theme.flagColors, data.flagType);

            lock (data.graphics)
                data.graphics.FillRectangle(new SolidBrush(color),
                  new Rectangle(x, data.profile.Height - rectHeight, rectWidth, rectHeight));
        }
    }
}
