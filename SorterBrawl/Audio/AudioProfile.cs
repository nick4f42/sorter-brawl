using System;
using System.Collections.Generic;
using System.Text;

namespace SorterBrawl.Audio
{
    class AudioProfile
    {
        public int FramesPerSecond { get; set; } = 30;

        public double Amplitude { get; set; } = 7_000;

        public double MinFrequency { get; set; } = 300;

        public double MaxFrequency { get; set; } = 1_000;

        public AudioStyle Style { get; set; } = AudioStyle.DefaultPortioner;
    }
}
