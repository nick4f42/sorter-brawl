using System;
using System.Collections.Generic;
using System.IO;

using static System.Math;

using SorterBrawl.Sorters;

namespace SorterBrawl
{
    using FlagList = List<Tuple<int, FlagType>>;

    partial class Animator
    {
        private class AudioMaker : Maker
        {
            public const string AudioFileName = "audio.wav";

            string wavFilePath;

            readonly Profile profile;

            readonly FileStream stream;

            readonly BinaryWriter writer;

            int samplesPerFrame;

            const int SamplesPerSecond = 44100;

            public AudioMaker(string savePath, Profile profile, int[] array)
              : base(savePath, array)
            {
                this.profile = profile;

                wavFilePath = savePath + AudioFileName;

                samplesPerFrame = (int)(SamplesPerSecond / profile.FrameRate);

                stream = new FileStream(wavFilePath, FileMode.Create);
                writer = new BinaryWriter(stream);

                writer.Write(GetWavHeaderBytes(0.0).ToArray());
            }

            public override void Update(Sorter sender, FlagList flagList)
            {
                lock (writer)
                    if (!lastUpdate && ComparisonCount++ % profile.FrameCountDownscale != 0)
                        return;

                SaveAudioFrame(flagList);
            }

            void SaveAudioFrame(FlagList flagList)
            {
                for (int i = 0; i < samplesPerFrame; i++)
                {
                    double t = (double)i / SamplesPerSecond;
                    short s = 0;
                    foreach (var flag in flagList)
                    {
                        if (flag.Item1 < 0 || flag.Item1 >= array.Length)
                            continue;

                        double amplFraction = profile.Audio.Style.Portion(i, samplesPerFrame);

                        double freq = ExponentialMap(1.005, array[flag.Item1], minValue, maxValue,
                          profile.Audio.MinFrequency, profile.Audio.MaxFrequency);

                        s += (short)(profile.Audio.Amplitude * amplFraction * Sin(t * freq * 2.0 * PI));
                    }

                    lock (writer)
                        writer.Write(s);
                }

                FrameCount++;
            }

            public override void Finish()
            {
                writer.Dispose();
                stream.Dispose();

                byte[] wavBytes = File.ReadAllBytes(wavFilePath);

                double duration = (double)FrameCount / profile.FrameRate;

                GetWavHeaderBytes(duration).CopyTo(wavBytes);

                File.WriteAllBytes(wavFilePath, wavBytes);
            }

            public override bool HasCompleted()
            {
                return FrameCount >= profile.NonBlankFramelimit;
            }

            const int RIFF = 0x46464952;
            const int WAVE = 0x45564157;
            const int formatChunkSize = 16;
            const int headerSize = 8;
            const int format = 0x20746D66;
            const short formatType = 1;
            const short tracks = 1;
            const short bitsPerSample = 16;
            const int data = 0x61746164;
            const int waveSize = 4;
            const short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            const int bytesPerSecond = SamplesPerSecond * frameSize;

            static List<byte> GetWavHeaderBytes(double duration)
            {
                int samples = (int)(SamplesPerSecond * duration);
                int dataChunkSize = samples * frameSize;
                int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;

                List<byte> byteList = new List<byte>();

                byteList.AddRange(BitConverter.GetBytes(RIFF));
                byteList.AddRange(BitConverter.GetBytes(fileSize));
                byteList.AddRange(BitConverter.GetBytes(WAVE));
                byteList.AddRange(BitConverter.GetBytes(format));
                byteList.AddRange(BitConverter.GetBytes(formatChunkSize));
                byteList.AddRange(BitConverter.GetBytes(formatType));
                byteList.AddRange(BitConverter.GetBytes(tracks));
                byteList.AddRange(BitConverter.GetBytes(SamplesPerSecond));
                byteList.AddRange(BitConverter.GetBytes(bytesPerSecond));
                byteList.AddRange(BitConverter.GetBytes(frameSize));
                byteList.AddRange(BitConverter.GetBytes(bitsPerSample));
                byteList.AddRange(BitConverter.GetBytes(data));
                byteList.AddRange(BitConverter.GetBytes(dataChunkSize));

                return byteList;
            }

            static double ExponentialMap(double b, double x, double x1, double x2, double y1, double y2)
            {
                return y1 + (y2 - y1) * (Pow(b, x) - Pow(b, x1)) / (Pow(b, x2) - Pow(b, x1));
            }
        }
    }
}
