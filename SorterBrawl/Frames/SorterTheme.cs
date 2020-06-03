using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using SorterBrawl.Sorters;

namespace SorterBrawl.Frames
{
    class SorterTheme
    {
        public Dictionary<FlagType, Color> flagColors;

        public SorterTheme()
        {
            flagColors = new Dictionary<FlagType, Color>();
        }

        public SorterTheme(Dictionary<FlagType, Color> flagColors)
        {
            this.flagColors = flagColors;
        }

        static Color SaturizeColor(Color color, float amount)
        {
            return Color.FromArgb(
              (int)((255 - color.R) * amount + color.R),
              (int)((255 - color.G) * amount + color.G),
              (int)((255 - color.B) * amount + color.B));
        }

        public static SorterTheme Warm
        {
            get
            {
                return new SorterTheme(new Dictionary<FlagType, Color>
                {
                    [FlagType.Compared] = Color.FromArgb(207, 72, 72),
                    [FlagType.Pivot] = Color.FromArgb(209, 114, 36),
                    [FlagType.InPlace] = Color.FromArgb(163, 126, 126),
                    [FlagType.SweepCheck] = Color.FromArgb(156, 40, 88),
                    [FlagType.SweepFailed] = Color.FromArgb(224, 34, 97)
                });
            }
        }

        public static SorterTheme Cold
        {
            get
            {
                return new SorterTheme(new Dictionary<FlagType, Color>
                {
                    [FlagType.Compared] = Color.FromArgb(52, 109, 224),
                    [FlagType.Pivot] = Color.FromArgb(77, 172, 196),
                    [FlagType.InPlace] = Color.FromArgb(129, 129, 166),
                    [FlagType.SweepCheck] = Color.FromArgb(54, 160, 163),
                    [FlagType.SweepFailed] = Color.FromArgb(26, 214, 219)
                });
            }
        }
    }
}
