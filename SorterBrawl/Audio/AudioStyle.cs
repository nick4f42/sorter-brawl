using System;
using System.Collections.Generic;
using System.Text;

using static System.Math;

namespace SorterBrawl.Audio
{
    static class AudioStyle
    {
        public delegate double AmplPortioner(int i, int samplesPerFrame);

        public static double DefaultPortioner(int i, int samplesPerFrame)
        {
            return SoftBeep(i, samplesPerFrame);
        }

        public static double SoftBeep(int i, int samplesPerFrame)
        {
            return Pow((double)i * (samplesPerFrame - i) * 4.0 / (samplesPerFrame * samplesPerFrame), 2.0);
        }

        public static double QuickBeep(int i, int samplesPerFrame)
        {
            return Pow((double)i * (samplesPerFrame - i) * 4.0 / (samplesPerFrame * samplesPerFrame), 6.0);
        }
    }
}
