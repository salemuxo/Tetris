using RLNET;
using Tetris.Core;

namespace Tetris.Systems
{
    public abstract class StatManager
    {
        public int Level { get; protected set; }
        public int Score { get; set; }
        public int Lines { get; protected set; }
        public double Time { get; protected set; }

        protected int _combo = -1;

        public abstract void Update(double deltaTime);

        public abstract void Draw(RLConsole console);

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