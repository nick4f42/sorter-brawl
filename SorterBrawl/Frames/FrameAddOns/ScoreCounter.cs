using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SorterBrawl.Frames.FrameAddOns
{
    class ScoreCounter : IFrameAddOn
    {
        public int WinScore { get; set; }

        int score;

        public ScoreCounter(int winScore)
        {
            WinScore = WinScore;
        }

        public void AfterDraw(int[] array, Graphics graphics, Bitmap bitmap, int frameCount)
        {
            throw new NotImplementedException();
        }

        public void BeforeDraw(int[] array, Graphics graphics, Bitmap bitmap, int frameCount)
        {
            throw new NotImplementedException();
        }
    }
}
