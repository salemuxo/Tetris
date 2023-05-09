using RLNET;

namespace Tetris.Core
{
    public class Cell
    {
        public bool IsTile { get; private set; }
        public bool IsGhost { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public RLColor Color { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Color = Palette.Grey;
            IsTile = false;
            IsGhost = false;
        }

        public void Draw(RLConsole console)
        {
            if (IsTile)
            {
                console.Set(X, Y, Color, RLColor.Black, 8);
            }
            else if (IsGhost)
            {
                console.Set(X, Y, Color, RLColor.Black, 9);
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

        public void SetGhost(RLColor color)
        {
            Color = color;
            IsGhost = true;
        }

        public void RemoveTile()
        {
            if (!IsGhost)
            {
                Color = Palette.Grey;
            }
            IsTile = false;
        }

        public void RemoveGhost()
        {
            if (!IsTile)
            {
                Color = Palette.Grey;
            }
            IsGhost = false;
        }

        public void ClearCell()
        {
            Color = Palette.Grey;
            IsTile = false;
            IsGhost = false;
        }

        public Cell Clone()
        {
            return this.MemberwiseClone() as Cell;
        }
    }
}