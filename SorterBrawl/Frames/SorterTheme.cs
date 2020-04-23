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

        public static readonly Dictionary<FlagType, Color> defaultFlagColors = new Dictionary<FlagType, Color>()
        {
            [FlagType.None] = Color.FromArgb(0x7fab9daa)
        };

        public SorterTheme()
        {
            flagColors = defaultFlagColors.ToDictionary(
              entry => entry.Key, entry => entry.Value);
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
            get => new SorterTheme(new Dictionary<FlagType, Color>
            {
                [FlagType.Compared] = Color.FromArgb(0x7fe32727),
                [FlagType.Pivot] = Color.FromArgb(0x7fa8872c),
                [FlagType.InPlace] = Color.FromArgb(0x7fe88574),
                [FlagType.SweepCheck] = Color.FromArgb(0x7fe84343),
                [FlagType.SweepFailed] = Color.FromArgb(0x7f590d0d)
            });
        }

        public static SorterTheme Cold
        {
            get => new SorterTheme(new Dictionary<FlagType, Color>
            {
                [FlagType.Compared] = Color.FromArgb(0x7f295bd9),
                [FlagType.Pivot] = Color.FromArgb(0x7f9c45de),
                [FlagType.InPlace] = Color.FromArgb(0x7f3da1db),
                [FlagType.SweepCheck] = Color.FromArgb(0x7f1f87f0),
                [FlagType.SweepFailed] = Color.FromArgb(0x7f45045e)
            });
        }
    }
}
