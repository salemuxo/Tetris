using Tetris.Core;

namespace Tetris.Systems
{
    public class MarathonStatManager : StatManager
    {
        // add score for lines cleared and check for combo and level up
        public override void ClearedLines(int lines)
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

        // give points for hard drop
        public override void HardDrop(int cells)
        {
            Score += 2 * cells;
            //Game.MessageLog.Add($"Drop +{2 * cells}");
        }
    }
}