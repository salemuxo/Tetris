using RLNET;

namespace Tetris.Core
{
    public class TextBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Text { get; set; }

        public TextBox(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Text = "";
        }

        public void Draw(RLConsole console)
        {
            console.Print(X, Y, Text, RLColor.White);
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