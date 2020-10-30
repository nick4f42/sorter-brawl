using System;
using System.IO;
using System.Drawing;

using SorterBrawl.Sorters;
using SorterBrawl.Audio;
using SorterBrawl.Frames;
using SorterBrawl.Frames.Stylers;

namespace SorterBrawl
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = GetRandomArray(100);

            Sorter[] sorters = new Sorter[] {
                new MergeSort(SorterTheme.Warm),
                new QuickSortReverse(SorterTheme.Cold)
            };

            var profile = new Profile(
                new FrameProfile(1920, 1080, new Rectangle(0, 250, 1920, 1080 - 250), 
                                 new RoundedBarStyler(Color.FromArgb(105, 104, 104), Color.FromArgb(24, 25, 26), 10, 1)),
                new AudioProfile { },
                frameLimit: 30 * 5,
                frameCountDownscale: 1
            );

            Animator animator = new Animator(array, sorters, profile);

            string animationsDir = @"..\..\..\Animations\";
            Directory.CreateDirectory(animationsDir);

            animator.SaveAnimation(animationsDir);
        }

        static int[] GetRandomArray(int size, int? seed = null)
        {
            int[] array = new int[size];

            Random rand = seed.HasValue ? new Random(seed.Value) : new Random();

            for (int i = 1; i <= size; i++)
            {
                int j;
                do
                {
                    j = rand.Next(0, size);
                } while (array[j] != 0);
                array[j] = i;
            }

            return array;
        }

        static int[] GetReverseArray(int size)
        {
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
                result[i] = size - i;
            return result;
        }

        static void RandomizeArray(int[] array, int? seed = null)
        {
            Random rand = seed.HasValue ? new Random(seed.Value) : new Random();

            for (int i = 0; i < array.Length; i++)
            {
                int j = rand.Next(0, array.Length);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
