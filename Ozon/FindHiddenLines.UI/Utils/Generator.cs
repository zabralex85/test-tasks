using System;

namespace FindHiddenLines.UI.Utils
{
    public class Generator
    {
        public static int[] RandomInts(Random rand, int pointCount, int min, int max)
        {
            int[] numArray = new int[pointCount];
            for (int index = 0; index < pointCount; index++)
            {
                numArray[index] = rand.Next(min, max);
            }

            return numArray;
        }
    }
}
