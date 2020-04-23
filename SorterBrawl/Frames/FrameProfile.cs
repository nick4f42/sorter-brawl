using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Frames.Stylers;

namespace SorterBrawl.Frames
{
    class FrameProfile
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public IStyler Styler { get; set; } = new BarStyler();

        public int FrameLimit { get; set; } = 1_000;

        public int FrameCountDownscale { get; set; } = 1;
    }
}
