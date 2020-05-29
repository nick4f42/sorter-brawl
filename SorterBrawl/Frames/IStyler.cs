using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using SorterBrawl.Sorters;
using SorterBrawl.Frames;
using SorterBrawl.Audio;

namespace SorterBrawl.Frames
{
    interface IStyler
    {
        public void Clear(Graphics graphics);

        public void DrawElement(DrawData data);

        protected static Color GetFlagColor(Dictionary<FlagType, Color> flagColors,
          FlagType flagType)
        {
            Color color = Color.Pink;
            if (flagColors is null || !flagColors.TryGetValue(flagType, out color))
                SorterTheme.defaultFlagColors.TryGetValue(flagType, out color);

            return color;
        }
    }
}
