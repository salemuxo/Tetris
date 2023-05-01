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
            throw new NotImplementedException();
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
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
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
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
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
                { true, false, false },
                { true, true, true }
            };
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
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
                { false, false, true },
                { true, true, true }
            };
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
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
                { false, true, true },
                { true, true, false }
            };
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
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
                { true, true, false },
                { false, true, true }
            };
        }

        public override void Rotate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Z";
        }
    }
}