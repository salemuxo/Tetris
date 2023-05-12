﻿using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Core;

namespace Tetris.Systems
{
    public class StatManager
    {
        public int Level { get; private set; }
        public int Score { get; private set; }
        public int Lines { get; private set; }

        public StatManager()
        {
            Level = 1;
            Score = 0;
        }

        public void ClearedLines(int lines)
        {
            int oldLines = Lines;

            Lines += lines;
            switch (lines)
            {
                case 1:
                    Score += 100 * Level;
                    Game.MessageLog.Add($"Single +{100 * Level}", Palette.Red);
                    break;

                case 2:
                    Score += 300 * Level;
                    Game.MessageLog.Add($"Double +{300 * Level}", Palette.Blue);
                    break;

                case 3:
                    Score += 500 * Level;
                    Game.MessageLog.Add($"Triple +{500 * Level}", Palette.Green);
                    break;

                case 4:
                    Score += 800 * Level;
                    Game.MessageLog.Add($"Tetris +{800 * Level}", Palette.Purple);
                    break;
            }
            int nearestLevel = oldLines.RoundUp();
            if (oldLines < nearestLevel && Lines > nearestLevel)
            {
                IncreaseLevel();
            }
        }

        public void Draw(RLConsole console)
        {
            console.Print(0, 0, "SCORE", RLColor.White);
            console.Print(0, 1, Score.ToString(), RLColor.White);

            console.Print(0, 3, "LEVEL", RLColor.White);
            console.Print(0, 4, Level.ToString(), RLColor.White);

            console.Print(0, 6, "LINES", RLColor.White);
            console.Print(0, 7, Lines.ToString(), RLColor.White);
        }

        private void IncreaseLevel()
        {
            Level++;
            Game.TimeManager.SetUpdateTime();
            Game.MessageLog.Add($"Leveled up to level {Level}", Palette.Cyan);
        }
    }
}