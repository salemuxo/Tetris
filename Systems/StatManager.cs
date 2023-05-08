using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class StatManager
    {
        public static int Level { get; private set; }
        public static int Score { get; private set; }
        public static int Lines { get; private set; }

        private static int Width;
        private static int Height;

        public static void Initialize(int w, int h)
        {
            Level = 1;
            Score = 0;
            Width = w;
            Height = h;
        }

        public static void ClearedLines(int lines)
        {
            Lines += lines;

            switch (lines)
            {
                case 1:
                    Score += 100 * Level;
                    break;

                case 2:
                    Score += 300 * Level;
                    break;

                case 3:
                    Score += 500 * Level;
                    break;

                case 4:
                    Score += 800 * Level;
                    break;
            }
        }

        public static void Draw(RLConsole console)
        {
            console.Print(0, 0, "Score", RLColor.White);
            console.Print(0, 1, Score.ToString(), RLColor.White);

            console.Print(0, 3, "Level", RLColor.White);
            console.Print(0, 4, Level.ToString(), RLColor.White);

            console.Print(0, 6, "Lines", RLColor.White);
            console.Print(0, 7, Lines.ToString(), RLColor.White);
        }

        private static void IncreaseLevel()
        {
            Level++;
            TimeManager.UpdateTime *= .9;
        }
    }
}