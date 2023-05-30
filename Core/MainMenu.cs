using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.UI;

namespace Tetris.Core
{
    public class MainMenu
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<UIElement> Children { get; set; }

        public MainMenu(int width, int height)
        {
            Width = width;
            Height = height;
            Children = new List<UIElement>();
        }

        public void Draw(RLConsole console)
        {
            //console.Print(12, 2, "TETRIS", Palette.Text);
            DrawLogo(console, 12, 2);
            console.Print(11, 3, "by Salem", Palette.Text);
            console.Print(12, 6, "START!", Palette.Text);

            //foreach (var child in Children)
            //{
            //    child.Draw(console);
            //}
        }

        public void Clicked(int x, int y)
        {
            //Debug.WriteLine($"{x}, {y}");
            if (x >= 12 && x <= 16 && y == 6)
            {
                Program.StartGame();
            }
        }

        private void DrawLogo(RLConsole console, int x, int y)
        {
            console.Set(x, y, Palette.Red, null, 'T');
            console.Set(x + 1, y, Palette.Orange, null, 'E');
            console.Set(x + 2, y, Palette.Yellow, null, 'T');
            console.Set(x + 3, y, Palette.Green, null, 'R');
            console.Set(x + 4, y, Palette.Blue, null, 'I');
            console.Set(x + 5, y, Palette.Purple, null, 'S');
        }
    }
}