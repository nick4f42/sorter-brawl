using System;
using System.Collections.Generic;
using System.Text;

using SorterBrawl.Audio;
using SorterBrawl.Frames;

using SorterBrawl.Frames.Stylers;

namespace SorterBrawl
{
    class Profile
    {
        public FrameProfile Frames { get; set; }
        public AudioProfile Audio { get; set; }

        public int FrameLimit { get; set; }

        public int FrameCountDownscale { get; set; }

        public Profile(FrameProfile frameProfile, AudioProfile audioProfile,
            int frameLimit = 1_000, int frameCountDownscale = 1)
        {
            Frames = frameProfile;
            Audio = audioProfile;
            FrameLimit = frameLimit;
            FrameCountDownscale = frameCountDownscale;
        }

        public Profile(AudioProfile audioProfile, int frameLimit = 1_000, int frameCountDownscale = 1)
            : this(null, audioProfile, frameLimit, frameCountDownscale) { }

        public Profile(FrameProfile frameProfile, int frameLimit = 1_000, int frameCountDownscale = 1)
            : this(frameProfile, null, frameLimit, frameCountDownscale) { }
    }
}
