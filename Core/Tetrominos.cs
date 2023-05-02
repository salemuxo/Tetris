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
        }

        public override void Rotate()
        {
            RotateMatrix(Body);
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
        }

        public override void Rotate()
        {
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
        }

        public override void Rotate()
        {
            ResetCells();
            switch (rotation)
            {
                case 0:
                    RotateAndMove(X + 1, Y - 1);
                    break;

                case 1:
                    RotateAndMove(X - 1, Y);
                    break;

                case 2:
                    RotateAndMove(X, Y);
                    break;

                case 3:
                    RotateAndMove(X, Y + 1);
                    break;
            }
        }
    }

    public class J : Tetromino
    {
        public J()
        {
            Body = new bool[,]
            {
                { true, false, false },
                { true, true, true }
            };
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
        }
    }

    public class L : Tetromino
    {
        public L()
        {
            Body = new bool[,]
            {
                { false, false, true },
                { true, true, true }
            };
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
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
    }
}