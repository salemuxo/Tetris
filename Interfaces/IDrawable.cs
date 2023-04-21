using RLNET;

namespace Tetris.Interfaces
{
    public interface IDrawable
    {
        int X { get; set; }
        int Y { get; set; }
        RLColor Color { get; set; }
        char Symbol { get; set; }

        void Draw(RLConsole console);
    }
}