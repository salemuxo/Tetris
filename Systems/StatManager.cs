using RLNET;
using Tetris.Core;

namespace Tetris.Systems
{
    public abstract class StatManager
    {
        public int Level { get; protected set; }
        public int Score { get; set; }
        public int Lines { get; protected set; }

        protected int _combo = -1;

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

        public abstract void ClearedLines(int lines);

        public abstract void HardDrop(int cells);

        // level up and speed up time
        protected void IncreaseLevel()
        {
            Level++;
            Game.TimeManager.SetUpdateTime();
            Game.MessageLog.Add($"Leveled up to level {Level}", Palette.Cyan);
        }
    }
}