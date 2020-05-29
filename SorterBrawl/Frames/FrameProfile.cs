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
    }
}
