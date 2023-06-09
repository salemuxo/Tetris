using RLNET;
using System;
using System.Diagnostics;
using Tetris.Core;
using Tetris.Systems;

namespace Tetris.Modes
{
    public class UltraStatManager : StatManager
    {
        public UltraStatManager()
        {
            Level = 1;
            Score = 0;
        }

        // calculate lines cleared, score, combo when piece is dropped
        public override void ClearedLines(int lines)
        {
            //Debug.WriteLine(lines);
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

            // calculate combo
            if (lines != 0)
            {
                _combo++;
                if (_combo > 0)
                {
                    Game.MessageLog.Add($"Combo x{_combo + 1} +{50 * _combo * Level}");
                }
            }
            else
            {
                _combo = -1;
            }
        }

        public override void Draw(RLConsole console)
        {
            console.Print(0, 0, "TOP", Palette.Text);
            console.Print(0, 1, Program.Leaderboard.UltraScores[0].Score.ToString(), Palette.Text);

            console.Print(0, 3, "SCORE", Palette.Text);
            console.Print(0, 4, Score.ToString(), Palette.Text);

            console.Print(0, 6, "TIME", Palette.Text);
            console.Print(0, 7, Utility.TimeToString(Time), Palette.Text);

            console.Print(0, 9, "LINES", Palette.Text);
            console.Print(0, 10, Lines.ToString(), Palette.Text);
        }

        // calculate hard drop score
        public override void HardDrop(int cells)
        {
            Score += 2 * cells;
        }

        // update time and check if time is up
        public override void Update(double deltaTime)
        {
            if (Game.IsPlaying)
            {
                Time += deltaTime;
            }

            if (Time >= 120000)
            {
                Program.Game.GameOver();
            }
        }
    }
}