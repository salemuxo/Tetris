using RLNET;
using Tetris.Interfaces;

namespace Tetris.Core
{
    public class Tile : IDrawable
    {
        public bool IsFalling { get; set; }
        public Tetromino ParentTetromino { get; set; }

        public Tile(int x, int y, RLColor color, char symbol, Tetromino parentTetromino)
        {
            X = x;
            Y = y;
            Color = color;
            Symbol = symbol;
            IsFalling = true;
            ParentTetromino = parentTetromino;
        }

        public void RemoveParent()
        {
            IsFalling = false;
            ParentTetromino = null;
        }

        // IDrawable implementation
        public int X { get; set; }
        public int Y { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }

        public void Draw(RLConsole console)
        {
            if (IsFalling)
            {
            }
            else
            {
                console.Set(X, Y, Color, RLColor.Black, Symbol);
            }
        }
    }
}