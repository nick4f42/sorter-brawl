using System;
using System.Collections.Generic;
using System.Text;

using static System.Math;

namespace SorterBrawl.Audio
{
    class AudioStyle
    {
        public delegate double AmplPortioner(int i, int samplesPerFrame);

        public AmplPortioner Portion;

        public AudioStyle(AmplPortioner portioner)
        {
            Portion = portioner;
        }

        public static AudioStyle DefaultPortioner
        {
            get => SoftBeep;
        }

        public static AudioStyle SoftBeep
        {
            get
            {
                return new AudioStyle((int i, int samplesPerFrame) =>
                {
                    return Pow((double)i * (samplesPerFrame - i) * 4.0 / (samplesPerFrame * samplesPerFrame), 2.0);
                });
            }
        }

        public static AudioStyle QuickBeep
        {
            get
            {
                return new AudioStyle((int i, int samplesPerFrame) =>
                {
                    return Pow((double)i * (samplesPerFrame - i) * 4.0 / (samplesPerFrame * samplesPerFrame), 6.0);
                });
            }
        }
    }
}
