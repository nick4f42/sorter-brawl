using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using SorterBrawl.Frames.Stylers;
using SorterBrawl.Frames.FrameAddOns;

namespace SorterBrawl.Frames
{
    class FrameProfile
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle ViewBox { get; set; }

        public Styler Styler { get; set; }

        public List<IFrameAddOn> AddOns { get; set; }

        public FrameProfile(int width, int height, Rectangle? viewBox = null, Styler styler = null, List<IFrameAddOn> addOns = null)
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

            if (addOns is object)
                AddOns = addOns;
            else
                AddOns = new List<IFrameAddOn>();
        }
    }
}
