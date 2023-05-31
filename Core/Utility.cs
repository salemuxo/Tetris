using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public static class ExtensionMethods
    {
        public static int RoundUp(this int i)
        {
            return (int)(Math.Ceiling(i / 10.0) * 10);
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