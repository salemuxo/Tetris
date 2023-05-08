using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public class I : Tetromino
    {
        public I()
        {
            Body = new bool[,]
            {
                { true, true, true, true }
            };
            Color = Palette.Cyan;
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 2, Y - 1);
                    break;

                case 1:
                    RotateAndMove(X - 2, Y + 2);
                    break;

                case 2:
                    RotateAndMove(X + 1, Y - 2);
                    break;

                case 3:
                    RotateAndMove(X - 1, Y + 1);
                    break;
            }
        }

        public override string ToString()
        {
            return "I";
        }
    }

    public class O : Tetromino
    {
        public O()
        {
            Body = new bool[,]
            {
                { true, true },
                { true, true }
            };
            Color = Palette.Yellow;
        }

        public override void Rotate()
        {
            rotation++;
        }

        public override string ToString()
        {
            return "O";
        }
    }

    public class T : Tetromino
    {
        public T()
        {
            Body = new bool[,]
            {
                { true, true, true },
                { false, true, false }
            };
            Color = Palette.Purple;
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 1, Y);
                    break;

                case 1:
                    RotateAndMove(X - 1, Y + 1);
                    break;

                case 2:
                    RotateAndMove(X, Y - 1);
                    break;

                case 3:
                    RotateAndMove(X, Y);
                    break;
            }
        }

        public override string ToString()
        {
            return "T";
        }
    }

    public class J : Tetromino
    {
        public J()
        {
            Body = new bool[,]
            {
                { true, true, true },
                { true, false, false }
            };
            Color = Palette.Blue;
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 1, Y);
                    break;

                case 1:
                    RotateAndMove(X - 1, Y + 1);
                    break;

                case 2:
                    RotateAndMove(X, Y - 1);
                    break;

                case 3:
                    RotateAndMove(X, Y);
                    break;
            }
        }

        public override string ToString()
        {
            return "J";
        }
    }

    public class L : Tetromino
    {
        public L()
        {
            Body = new bool[,]
            {
                { true, true, true },
                { false, false, true }
            };
            Color = Palette.Orange;
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 1, Y);
                    break;

                case 1:
                    RotateAndMove(X - 1, Y + 1);
                    break;

                case 2:
                    RotateAndMove(X, Y - 1);
                    break;

                case 3:
                    RotateAndMove(X, Y);
                    break;
            }
        }

        public override string ToString()
        {
            return "L";
        }
    }

    public class S : Tetromino
    {
        public S()
        {
            Body = new bool[,]
            {
                { true, true, false },
                { false, true, true }
            };
            Color = Palette.Green;
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 1, Y);
                    break;

                case 1:
                    RotateAndMove(X - 1, Y + 1);
                    break;

                case 2:
                    RotateAndMove(X, Y - 1);
                    break;

                case 3:
                    RotateAndMove(X, Y);
                    break;
            }
        }

        public override string ToString()
        {
            return "S";
        }
    }

    public class Z : Tetromino
    {
        public Z()
        {
            Body = new bool[,]
            {
                { false, true, true },
                { true, true, false }
            };
            Color = Palette.Red;
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 1, Y);
                    break;

                case 1:
                    RotateAndMove(X - 1, Y + 1);
                    break;

                case 2:
                    RotateAndMove(X, Y - 1);
                    break;

                case 3:
                    RotateAndMove(X, Y);
                    break;
            }
        }

        public override string ToString()
        {
            return "Z";
        }
    }
}