using System;

namespace Tetris.Core
{
    public static class ExtensionMethods
    {
        public static int RoundUp(this int i, double roundBy)
        {
            return (int)(Math.Ceiling(i / roundBy) * roundBy);
        }
    }

    public static class Utility
    {
        public static int GetCenteredX(int backWidth, int frontWidth)
        {
            return (backWidth - frontWidth) / 2;
        }
    }
}