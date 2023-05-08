using RLNET;

namespace Tetris.Core
{
    public class Cell
    {
        public bool IsTile { get; set; }
        public bool IsBorder { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Color = Palette.Grey;
            IsTile = false;
            IsBorder = false;
        }

        public void SetBorder()
        {
            IsBorder = true;
            IsTile = true;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public RLColor Color { get; set; }

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

        public void SetTile(RLColor color)
        {
            Color = color;
            IsTile = true;
        }

        public void RemoveTile()
        {
            Color = Palette.Grey;
            IsTile = false;
        }

        public Cell Clone()
        {
            return this.MemberwiseClone() as Cell;
        }
    }
}