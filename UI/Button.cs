using RLNET;
using Tetris.Core;
using System;

namespace Tetris.UI
{
    public class Button : UIElement
    {
        public event EventHandler Click;

        private bool _drawBorder;
        private readonly string _text;
        private RLColor _textColor;
        private RLColor _borderColor;

        public Button(int x, int y, int w, string text, RLColor textColor, RLColor borderColor)
        {
            X = x;
            Y = y;
            Width = w;
            Height = 1;
            _text = text;
            _textColor = textColor;
            _borderColor = borderColor;
            _drawBorder = true;
        }

        public Button(int x, int y, int w, string text,
            RLColor textColor, bool drawBorder)
        {
            X = x;
            Y = y;
            Width = w;
            Height = 1;
            _text = text;
            _textColor = textColor;
            _borderColor = Palette.Text;
            _drawBorder = drawBorder;
        }

        public override void Draw(RLConsole console)
        {
            if (_drawBorder)
            {
                // create border
                for (int x = 0; x < Width; x++)
                {
                    console.Set(X + x, Y - 1, _borderColor, null, 205);
                    console.Set(X + x, Y + 1, _borderColor, null, 205);
                }
                console.Set(X - 1, Y, _borderColor, null, 186);
                console.Set(X + Width, Y, _borderColor, null, 186);
                console.Set(X - 1, Y - 1, _borderColor, null, 201);
                console.Set(X - 1, Y + 1, _borderColor, null, 200);
                console.Set(X + Width, Y - 1, _borderColor, null, 187);
                console.Set(X + Width, Y + 1, _borderColor, null, 188);
            }

            console.Print(X, Y, _text, _textColor);
        }

        public void CheckClick(int x, int y)
        {
            if (x >= X - 1 && x <= X + Width &&
                y >= Y - 1 && y <= Y + Height)
            {
                OnClick();
            }
        }

        protected virtual void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}