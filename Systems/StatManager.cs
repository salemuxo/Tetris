using RLNET;
using Tetris.Core;

namespace Tetris.Systems
{
    public class StatManager
    {
        public int Level { get; private set; }
        public int Score { get; set; }
        public int Lines { get; private set; }

        private int _combo = -1;

        public StatManager()
        {
            Level = 1;
            Score = 0;
        }

        public void Draw(RLConsole console)
        {
            console.Print(0, 0, "TOP", Palette.Text);
            console.Print(0, 1, Program.Leaderboard.HighScores[0].Score.ToString(), Palette.Text);

            console.Print(0, 3, "SCORE", Palette.Text);
            console.Print(0, 4, Score.ToString(), Palette.Text);

            console.Print(0, 6, "LEVEL", Palette.Text);
            console.Print(0, 7, Level.ToString(), Palette.Text);

            console.Print(0, 9, "LINES", Palette.Text);
            console.Print(0, 10, Lines.ToString(), Palette.Text);
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
            int nearestLevel = oldLines.RoundUp(15);
            if (oldLines < nearestLevel && Lines > nearestLevel)
            {
                IncreaseLevel();
            }

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

        public void HardDrop(int cells)
        {
            Score += 2 * cells;
            //Game.MessageLog.Add($"Drop +{2 * cells}");
        }

        private void IncreaseLevel()
        {
            Level++;
            Game.TimeManager.SetUpdateTime();
            Game.MessageLog.Add($"Leveled up to level {Level}", Palette.Cyan);
        }
    }
}