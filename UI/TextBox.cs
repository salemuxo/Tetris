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

        // if box isnt full and character isnt space, add to text
        public void Add(char? character)
        {
            if (character != null && character != '\u0020' && Text.Length < Width)
            {
                Text += character;
            }
        }

        // remove last character from text
        public void Remove()
        {
            if (Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }
        }
    }
}