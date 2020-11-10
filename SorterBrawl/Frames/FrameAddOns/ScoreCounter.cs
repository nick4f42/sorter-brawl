using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SorterBrawl.Frames.FrameAddOns
{
    class ScoreCounter : IFrameAddOn
    {
        public int WinScore { get; set; }

        const float AreaScorePortion = 0.3f;

        int panelHeight;

        SolidBrush forwardBrush;
        SolidBrush reverseBrush;

        int[] sortedArray;
        int maxArrayArea;

        public ScoreCounter(int[] array, int winScore, int panelHeight, Color forwardColor, Color reverseColor)
        {
            sortedArray = (int[])array.Clone();
            Array.Sort(sortedArray);

            for (int i = 0; i < array.Length / 2; i++)
            {
                maxArrayArea += sortedArray[i] - sortedArray[array.Length - 1 - i];
            }
            maxArrayArea *= 2;

            WinScore = WinScore;
            this.panelHeight = panelHeight;
            forwardBrush = new SolidBrush(forwardColor);
            reverseBrush = new SolidBrush(reverseColor);
        }

        public void AfterDraw(int[] array, Graphics graphics, Bitmap bitmap, int frameCount)
        {
            float scorePercent = GetScorePercent(array);

            int barWidth = (int)(bitmap.Width * (scorePercent + 1) / 2);

            graphics.FillRectangle(forwardBrush,
                new Rectangle(0, 0, barWidth, panelHeight));
            graphics.FillRectangle(reverseBrush,
                new Rectangle(barWidth, 0, bitmap.Width - barWidth, panelHeight));
        }

        public void BeforeDraw(int[] array, Graphics graphics, Bitmap bitmap, int frameCount)
        {
        }

        float GetOrderScorePercent(int[] array)
        {
            int orderCount = 0;
            for (int i = 0; i < array.Length - 1; i++)
                orderCount += array[i] < array[i + 1] ? 1 : -1;

            return (float)orderCount / (array.Length - 1);
        }

        float GetAreaScorePercent(int[] array)
        {
            int area = 0;
            for (int i = 0; i < array.Length; i++)
            {
                area += Math.Abs(array[i] - sortedArray[i]);
                area -= Math.Abs(array[i] - sortedArray[array.Length - 1 - i]);
            }
            return (float)area / maxArrayArea;
        }

        float GetScorePercent(int[] array)
        {
            return AreaScorePortion * GetAreaScorePercent(array) + (1 - AreaScorePortion) * GetOrderScorePercent(array);
        }
    }
}
