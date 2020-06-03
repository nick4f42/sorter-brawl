using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using SorterBrawl.Sorters;
using SorterBrawl.Frames;
using SorterBrawl.Audio;

namespace SorterBrawl.Frames
{
    abstract class Styler
    {
        public Color DefaultColor { get; set; } = Color.FromArgb(158, 158, 158);

        public Styler() { }

        public Styler(Color defaultColor)
        {
            DefaultColor = defaultColor;
        }

        public abstract void Clear(Graphics graphics);

        public abstract void DrawElement(DrawData data);

        protected Color GetFlagColor(Dictionary<FlagType, Color> flagColors,
            FlagType flagType)
        {
            if (flagType == FlagType.None)
                return DefaultColor;

            Color color = Color.Empty;
            if (!flagColors?.TryGetValue(flagType, out color) ?? true)
                color = Color.Pink;

            return color.IsEmpty ? DefaultColor : color;
        }
    }
}
