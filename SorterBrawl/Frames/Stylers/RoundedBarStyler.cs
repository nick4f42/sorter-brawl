using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

using SorterBrawl.Sorters;

namespace SorterBrawl.Frames.Stylers
{
    class RoundedBarStyler : Styler
    {
        public Color Background { get; set; } = Color.Black;

        public Color EdgeColor { get; set; } = Color.Black;

        public int Radius { get; set; }

        public int EdgeWidth { get; set; }

        public RoundedBarStyler(int radius = 25, int edgeWidth = 5)
        {
            Radius = radius;
            EdgeWidth = edgeWidth;
        }

        public RoundedBarStyler(Color noneColor, int radius = 25, int edgeWidth = 5)
            : base(noneColor)
        {
            Radius = radius;
            EdgeWidth = edgeWidth;
        }

        public RoundedBarStyler(Color noneColor, Color background, int radius = 25, int edgeWidth = 5)
            : base(noneColor)
        {
            Radius = radius;
            EdgeWidth = edgeWidth;

            Background = background;
            EdgeColor = Background;
        }

        public RoundedBarStyler(Color noneColor, Color background, Color edgeColor, int radius = 25, int edgeWidth = 5)
            : base(noneColor)
        {
            Radius = radius;
            EdgeWidth = edgeWidth;

            Background = background;
            EdgeColor = edgeColor;
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

            // TODO: Add a cleaner solution for this problem
            //       (rounded radius larger than bar)
            if (rectWidth / 2 < Radius)
            {
                Radius = rectWidth / 2;
            }

            int rectHeight = data.profile.ViewBox.Height
                * (data.value - data.minValue + 1) / (data.maxValue - data.minValue + 1);

            Color color = GetFlagColor(data.sorter?.Theme.flagColors, data.flagType);

            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddLine(x, data.profile.ViewBox.Bottom,
                         x, data.profile.ViewBox.Bottom - rectHeight + Radius);

            var rect = new Rectangle(x, data.profile.ViewBox.Bottom - rectHeight, 2 * Radius, 2 * Radius);
            path.AddArc(rect, 180, 90);

            rect.X += rectWidth - 2 * Radius;
            path.AddArc(rect, -90, 90);

            path.AddLine(x + rectWidth, data.profile.ViewBox.Bottom - rectHeight + Radius,
                         x + rectWidth, data.profile.ViewBox.Bottom);

            path.CloseFigure();

            lock (data.graphics)
            {
                data.graphics.FillPath(new SolidBrush(color), path);
                data.graphics.DrawPath(new Pen(EdgeColor, EdgeWidth), path);
            }
        }
    }
}
