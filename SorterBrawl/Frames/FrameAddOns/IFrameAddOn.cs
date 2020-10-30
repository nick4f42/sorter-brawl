using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SorterBrawl.Frames.FrameAddOns
{
    interface IFrameAddOn
    {
        void BeforeDraw(int[] array, Graphics graphics, Bitmap bitmap, int frameCount);

        void AfterDraw(int[] array, Graphics graphics, Bitmap bitmap, int frameCount);
    }
}
