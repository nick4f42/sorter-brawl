using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using SorterBrawl.Sorters;

namespace SorterBrawl.Frames.Stylers
{
    class BarStyler : Styler
    {
        public Color Background { get; set; } = Color.Black;

        public BarStyler() { }

        public BarStyler(Color defaultColor)
            : base(defaultColor)
        { }

        public BarStyler(Color defaultColor, Color background)
            : base(defaultColor)
        {
            Background = background;
        }

        public override void Clear(Graphics graphics)
        {
            lock (graphics)
                graphics.Clear(Background);
        }

        public override void DrawElement(DrawData data)
        {
            int x = data.profile.ViewBox.X + (int)((double)data.i / data.length * data.profile.ViewBox.Width);

            int rectWidth = (int)((double)(data.i + 1) / data.length * data.profile.ViewBox.Width) - x + data.profile.ViewBox.X;

            int rectHeight = data.profile.ViewBox.Height
                * (data.value - data.minValue + 1) / (data.maxValue - data.minValue + 1);

            Color color = GetFlagColor(data.sorter?.Theme.flagColors, data.flagType);

            lock (data.graphics)
                data.graphics.FillRectangle(new SolidBrush(color),
                  new Rectangle(x, 
                                data.profile.ViewBox.Y + data.profile.ViewBox.Height - rectHeight,
                                rectWidth, rectHeight));
        }
    }
}
