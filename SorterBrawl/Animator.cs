using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Linq;

using SorterBrawl.Sorters;
using SorterBrawl.Audio;
using SorterBrawl.Frames;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;

namespace SorterBrawl
{
    using FlagList = List<Tuple<int, FlagType>>;

    /// <summary>
    /// Saves animations of sorting algorithms sorting the same array.
    /// </summary>
    partial class Animator
    {
        /// <summary>
        /// Sorters that will concurrently sort the array.
        /// </summary>
        public Sorter[] Sorters { get; set; }

        int[] arrayTemplate;

        int[] array;

        Profile profile;

        FrameMaker frameMaker;
        AudioMaker audioMaker;

        /// <summary>
        /// First element true if the animation has finished.
        /// </summary>
        /// <remarks>
        /// Boolean value in array so it can be shared across threads.
        /// </remarks>
        readonly bool[] finished = new bool[1];

        /// <summary>
        /// Stores the index of the sorter who will make the next move.
        /// </summary>
        readonly int[] currentSorter = new int[1];

        /// <summary>
        /// Constructs Animator.
        /// </summary>
        /// <param name="arrayTemplate">The array used as a template for sorting.</param>
        /// <param name="sorters">The sorters to use for sorting.</param>
        /// <param name="profile">The profile to use for making the frames and audio.</param>
        public Animator(int[] arrayTemplate, Sorter[] sorters, Profile profile)
        {
            this.arrayTemplate = arrayTemplate;

            Sorters = sorters;

            this.profile = profile;
        }

        /// <summary>
        /// Saves the animation to a specified folder.
        /// </summary>
        /// <param name="folderPath">The folder to save the animation to.</param>
        public void SaveAnimation(string folderPath)
        {
            string animPath = CreateAnimDirectory(folderPath);

            PrepareNewAnimation(animPath);

            foreach (Sorter sorter in Sorters)
                sorter.FlagIndices += SorterFlagHandler;

            Thread[] threads = new Thread[Sorters.Length];
            for (int i = 0; i < threads.Length; i++)
            {
                int iCopy = i;
                threads[i] = new Thread(() => SorterLoop(Sorters[iCopy]));
                threads[i].Start();
            }

            foreach (Thread thread in threads)
                thread.Join();

            foreach (Sorter sorter in Sorters)
                sorter.FlagIndices -= SorterFlagHandler;

            frameMaker?.Finish();
            audioMaker?.Finish();
        }

        /// <summary>
        /// Creates a new animation directory and returns its path.
        /// </summary>
        /// <param name="path">The path to create the directory in.</param>
        /// <returns>The path of the newly created directory.</returns>
        string CreateAnimDirectory(string path)
        {
            string animPath = path + @"\Anim_" + DateTime.Now.ToString("MM-dd-yy_HH-mm-ss");

            Directory.CreateDirectory(animPath);

            return animPath;
        }

        /// <summary>
        /// Prepares all fields for saving a new animation.
        /// </summary>
        /// <param name="animationPath">The animation folder's path.</param>
        void PrepareNewAnimation(string animationPath)
        {
            array = CopyTemplateArray();

            if (profile.Frames != null)
                frameMaker = new FrameMaker(animationPath, profile, array);
            else
                frameMaker = null;

            if (profile.Audio != null)
                audioMaker = new AudioMaker(animationPath, profile, array);
            else
                audioMaker = null;

            currentSorter[0] = 0;

            finished[0] = false;
        }

        /// <summary>
        /// Handles a <see cref="Sorter.FlagIndices"/> event.
        /// </summary>
        void SorterFlagHandler(Sorter sender, FlagList flagList)
        {
            lock (finished)
            {
                if (finished[0])
                    return;
                else if ((frameMaker?.HasCompleted() ?? false) || (audioMaker?.HasCompleted() ?? false))
                {
                    finished[0] = true;
                    return;
                }
            }

            frameMaker?.UpdateFrame(sender, flagList);
            audioMaker?.UpdateFrame(sender, flagList);

            int index;
            lock (currentSorter)
            {
                index = currentSorter[0];
                if (currentSorter[0] == Sorters.Length - 1)
                    currentSorter[0] = 0;
                else
                    currentSorter[0]++;
            }

            SpinWait.SpinUntil(() =>
            {
                lock (currentSorter)
                    return currentSorter[0] == index;
            }, 10);
        }

        /// <summary>
        /// Sorts the array until it is sorted or animation has completed.
        /// </summary>
        /// <param name="sorter">The sorter to loop with.</param>
        void SorterLoop(Sorter sorter)
        {
            while (!finished[0])
            {
                if (frameMaker != null && frameMaker.HasCompleted())
                    break;
                if (audioMaker != null && audioMaker.HasCompleted())
                    break;

                sorter.Sort(array);
                if (sorter.IsSorted(array))
                    break;
            }

            finished[0] = true;
        }

        /// <summary>
        /// Returns a copy of the template array.
        /// </summary>
        /// <returns>
        /// A copy of the template int array.
        /// </returns>
        int[] CopyTemplateArray()
        {
            int[] copy = new int[arrayTemplate.Length];
            arrayTemplate.CopyTo(copy, 0);
            return copy;
        }
    }
}
