﻿using RLNET;
using System;
using Tetris.Core;
using Tetris.Systems;

namespace Tetris.Modes
{
    public class SprintStatManager : StatManager
    {
        public SprintStatManager()
        {
            Level = 1;
            Score = 0;
        }

        public override void Update(double deltaTime)
        {
            if (Game.IsPlaying)
            {
                Time += deltaTime;
            }
        }

        public override void Draw(RLConsole console)
        {
            console.Print(0, 0, "TOP", Palette.Text);
            //console.Print(0, 1, Utility.TimeToString(Program.Leaderboard.SprintScores[0].Time), Palette.Text);

            console.Print(0, 3, "TIME", Palette.Text);
            console.Print(0, 4, Utility.TimeToString(Time), Palette.Text);

            console.Print(0, 6, "LINES", Palette.Text);
            console.Print(0, 7, Lines.ToString(), Palette.Text);
        }

        public override void ClearedLines(int lines)
        {
            Lines += lines;
            if (Lines >= 40)
            {
                Program.Game.GameOver();
            }
        }

        public override void HardDrop(int cells)
        {
            //throw new NotImplementedException();
        }
    }
}