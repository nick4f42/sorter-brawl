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

        public Profile(FrameProfile frameProfile, AudioProfile audioProfile)
        {
            Frames = frameProfile;
            Audio = audioProfile;
        }

        public Profile(AudioProfile audioProfile) : this(null, audioProfile) { }

        public Profile(FrameProfile frameProfile) : this(frameProfile, null) { }
    }
}
