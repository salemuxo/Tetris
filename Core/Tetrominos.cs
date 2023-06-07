namespace Tetris.Core
{
    public class I : Tetromino
    {
        public I()
        {
            Body = new bool[,]
            {
                { true },
                { true },
                { true },
                { true }
            };
            SetRotationOffsets(2, -1, -2, 2, 1, -2, -1, 1);
            Color = Palette.Cyan;
            StartingX = 3;
        }

        public override string ToString()
        {
            return "I";
        }

        public override Tetromino Clone()
        {
            return new I();
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
            SetRotationOffsets(0, 0, 0, 0, 0, 0, 0, 0);
            Color = Palette.Yellow;
            StartingX = 4;
        }

        public override string ToString()
        {
            return "O";
        }

        public override Tetromino Clone()
        {
            return new O();
        }
    }

    public class T : Tetromino
    {
        public T()
        {
            Body = new bool[,]
            {
                { false, true },
                { true, true },
                { false, true }
            };
            SetRotationOffsets(1, 0, -1, 1, 0, -1, 0, 0);
            Color = Palette.Purple;
            StartingX = 3;
        }

        public override string ToString()
        {
            return "T";
        }

        public override Tetromino Clone()
        {
            return new T();
        }
    }

    public class J : Tetromino
    {
        public J()
        {
            Body = new bool[,]
            {
                { true, true },
                { false, true },
                { false, true }
            };
            SetRotationOffsets(1, 0, -1, 1, 0, -1, 0, 0);
            Color = Palette.Blue;
            StartingX = 3;
        }

        public override string ToString()
        {
            return "J";
        }

        public override Tetromino Clone()
        {
            return new J();
        }
    }

    public class L : Tetromino
    {
        public L()
        {
            Body = new bool[,]
            {
                { false, true },
                { false, true },
                { true, true }
            };
            SetRotationOffsets(1, 0, -1, 1, 0, -1, 0, 0);
            Color = Palette.Orange;
            StartingX = 3;
        }

        public override string ToString()
        {
            return "L";
        }

        public override Tetromino Clone()
        {
            return new L();
        }
    }

    public class S : Tetromino
    {
        public S()
        {
            Body = new bool[,]
            {
                { false, true },
                { true, true },
                { true, false }
            };
            SetRotationOffsets(1, 0, -1, 1, 0, -1, 0, 0);
            Color = Palette.Green;
            StartingX = 3;
        }

        public override string ToString()
        {
            return "S";
        }

        public override Tetromino Clone()
        {
            return new S();
        }
    }

    public class Z : Tetromino
    {
        public Z()
        {
            Body = new bool[,]
            {
                { true, false },
                { true, true },
                { false, true }
            };
            SetRotationOffsets(1, 0, -1, 1, 0, -1, 0, 0);

            Color = Palette.Red;
            StartingX = 3;
        }

        public override string ToString()
        {
            return "Z";
        }

        public override Tetromino Clone()
        {
            return new Z();
        }
    }
}