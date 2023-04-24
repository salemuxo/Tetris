using RLNET;
using Tetris.Interfaces;

namespace Tetris.Core
{
    public class Cell : IDrawable
    {
        public bool IsTile { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Color = RLColor.Red;
            Symbol = 'T';

            IsTile = false;
        }

        // IDrawable implementation
        public int X { get; set; }
        public int Y { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }

        public void Draw(RLConsole console)
        {
            if (IsTile)
            {
                console.Set(X, Y, Color, RLColor.Black, 8);
            }
            else
            {
                console.Set(X, Y, Color, RLColor.Black, 7);
            }
        }
    }
}