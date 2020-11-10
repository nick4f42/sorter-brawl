using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using static System.Math;

using SorterBrawl.Sorters;
using SorterBrawl.Audio;
using SorterBrawl.Frames;
using SorterBrawl.Frames.FrameAddOns;
using System.Runtime.CompilerServices;

namespace SorterBrawl
{
    using FlagList = List<Tuple<int, FlagType>>;

    partial class Animator
    {
        private class FrameMaker : Maker
        {
            public const string ImageFilePrefix = "image_";

            public string ImagePath { get; set; }

            readonly Profile profile;

            readonly Bitmap bitmap;

            readonly Graphics graphics;

            readonly Dictionary<int, FlagData> indexFlags = new Dictionary<int, FlagData>();

            public FrameMaker(string savePath, Profile profile, int[] array)
              : base(savePath, array)
            {
                ImagePath = savePath + @"images\";
                Directory.CreateDirectory(ImagePath);

                this.profile = profile;

                bitmap = new Bitmap(profile.Frames.Width, profile.Frames.Height);
                graphics = Graphics.FromImage(bitmap);

                graphics.Clear(Color.Black);

                foreach (var addOn in profile.Frames.AddOns)
                {
                    addOn.BeforeDraw(array, graphics, bitmap, FrameCount);
                }
            }

            public override void FinalUpdate()
            {
                base.FinalUpdate();

                indexFlags.Clear();
            }

            public override void Update(Sorter sender, FlagList flagList)
            {
                lock (array) lock (graphics) lock (bitmap)
                        {
                            UpdateIndexFlags(sender, flagList);

                            if (!lastUpdate && ComparisonCount++ % profile.FrameCountDownscale != 0)
                                return;

                            profile.Frames.Styler.Clear(graphics, profile.Frames);

                            for (int i = 0; i < array.Length; i++)
                            {
                                FlagData flagData;
                                lock (indexFlags)
                                    indexFlags.TryGetValue(i, out flagData);

                                DrawData data = new DrawData(graphics, flagData.sorter, profile.Frames, i,
                                    array.Length, array[i], minValue, maxValue, flagData.flagType);

                                profile.Frames.Styler.DrawElement(data);
                            }

                        }

                SaveFrame();
            }

            void SaveFrame()
            {
                foreach (var addOn in profile.Frames.AddOns) {
                    addOn.AfterDraw(array, graphics, bitmap, FrameCount);
                }

                lock (bitmap)
                    bitmap.Save(ImagePath + @$"frame_{++FrameCount}.png", ImageFormat.Png);

                graphics.Clear(Color.Black);

                foreach (var addOn in profile.Frames.AddOns)
                {
                    addOn.BeforeDraw(array, graphics, bitmap, FrameCount);
                }
            }

            public override void Finish()
            {
                graphics.Dispose();
                bitmap.Dispose();
            }

            public override bool HasCompleted()
            {
                return FrameCount >= profile.NonBlankFramelimit;
            }

            void UpdateIndexFlags(Sorter sender, FlagList flagList)
            {
                lock (indexFlags)
                {
                    var itemsToRemove = indexFlags.Where(x => x.Value.sorter == sender).ToArray();
                    foreach (var item in itemsToRemove)
                        indexFlags.Remove(item.Key);

                    foreach (var flag in flagList)
                        indexFlags[flag.Item1] = new FlagData()
                        {
                            flagType = flag.Item2,
                            sorter = sender
                        };
                }
            }
        }
    }
}
