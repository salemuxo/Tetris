using RLNET;
using Tetris.UI;

namespace Tetris.Core
{
    public class TextBox : UIElement
    {
        public string Text { get; set; }

        public TextBox(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Text = "";
        }

        public override void Draw(RLConsole console)
        {
            console.Print(X, Y, Text, Palette.Text);
            UserInterface.DrawSingleBorder(console, X, Y, Width, Height, Palette.Text);
        }

        public void Add(char? text)
        {
            if (text != null && Text.Length < Width)
            {
                Text += text;
            }
        }

        public void Remove()
        {
            if (Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }
        }
    }
}