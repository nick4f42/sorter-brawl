using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using SorterBrawl.Frames.Stylers;

namespace SorterBrawl.Frames
{
    class FrameProfile
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle ViewBox { get; set; }

        public Styler Styler { get; set; }

        public FrameProfile(int width, int height, Rectangle? viewBox = null, Styler styler = null)
        {
            Width = width;
            Height = height;

            if (viewBox.HasValue)
                ViewBox = viewBox.Value;
            else
                ViewBox = new Rectangle(0, 0, Width, Height);

            if (styler is object)
                Styler = styler;
            else
                Styler = new BarStyler();
        }
    }
}
