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

        public float FrameRate { get; set; }

        public int NonBlankFramelimit => FrameLimit - Convert.ToInt32(LastFrameBlank);

        public int FrameCountDownscale { get; set; }

        public bool LastFrameBlank { get; set; }

        public Profile(FrameProfile frameProfile, AudioProfile audioProfile,
            int frameLimit = 1_000, float frameRate = 30, int frameCountDownscale = 1, bool lastFrameBlank = true)
        {
            Frames = frameProfile;
            Audio = audioProfile;

            FrameLimit = frameLimit;
            FrameRate = frameRate;

            FrameCountDownscale = frameCountDownscale;

            LastFrameBlank = lastFrameBlank;
        }

        public Profile(AudioProfile audioProfile, int frameLimit = 1_000, float frameRate = 30,
                int frameCountDownscale = 1, bool lastFrameBlank = true)
            : this(null, audioProfile, frameLimit, frameRate, frameCountDownscale, lastFrameBlank) { }

        public Profile(FrameProfile frameProfile, int frameLimit = 1_000, float frameRate = 30,
                int frameCountDownscale = 1, bool lastFrameBlank = true)
            : this(frameProfile, null, frameLimit, frameRate, frameCountDownscale, lastFrameBlank) { }
    }
}
