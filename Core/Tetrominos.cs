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
            bodies = new List<bool[,]>()
            {
                new bool[,]
                {
                    { false, false, false, false },
                    { true, true, true, true },
                    { false, false, false, false },
                    { false, false, false, false }
                },
                new bool [,]
                {
                    { false, false, true, false },
                    { false, false, true, false },
                    { false, false, true, false },
                    { false, false, true, false }
                },
                new bool[,]
                {
                    { false, false, false, false },
                    { false, false, false, false },
                    { true, true, true, true },
                    { false, false, false, false }
                },
                new bool [,]
                {
                    { false, true, false, false },
                    { false, true, false, false },
                    { false, true, false, false },
                    { false, true, false, false }
                }
            };
            Body = bodies[rotation];
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
            bodies = new List<bool[,]>()
            {
                new bool[,]
                {
                    { false, true, false },
                    { true, true, true },
                    { false, false, false }
                },
                new bool[,]
                {
                    { false, true, false },
                    { false, true, true },
                    { false, true, false }
                },
                new bool[,]
                {
                    { false, false, false },
                    { true, true, true },
                    { false, true, false }
                },
                new bool[,]
                {
                    { false, true, false },
                    { true, true, false },
                    { false, true, false }
                }
            };

            Body = bodies[rotation];
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

    public class L : Tetromino
    {
        public L()
        {
            bodies = new List<bool[,]>()
            {
                new bool[,]
                {
                    { true, false, false},
                    { true, true, true },
                    { false, false, false }
                },
                new bool[,]
                {
                    { false, true, true },
                    { false, true, false },
                    { false, true, false }
                },
                new bool[,]
                {
                    { false, false, false },
                    { true, true, true },
                    { false, false, true }
                },
                new bool[,]
                {
                    { false, true, false },
                    { false, true, false },
                    { true, true, false }
                }
            };

            Body = bodies[rotation];
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

    public class J : Tetromino
    {
        public J()
        {
            bodies = new List<bool[,]>
            {
                new bool[,]
                {
                    { false, false, true},
                    { true, true, true },
                    { false, false, false }
                },
                new bool[,]
                {
                    { false, true, false },
                    { false, true, false },
                    { false, true, true }
                },
                new bool[,]
                {
                    { false, false, false },
                    { true, true, true },
                    { true, false , false }
                },
                new bool[,]
                {
                    { true, true, false },
                    { false, true, false },
                    { false, true, false }
                }
            };

            Body = bodies[rotation];
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
            bodies = new List<bool[,]>()
            {
                new bool[,]
                {
                    { false, true, true },
                    { true, true, false },
                    { false, false, false }
                },
                new bool[,]
                {
                    { false, true, false },
                    { false, true, true },
                    { false, false, true }
                },
                new bool[,]
                {
                    { false, false, false },
                    { false, true, true },
                    { true, true, false }
                },
                new bool[,]
                {
                    { true, false, false },
                    { true, true, false },
                    { false, true, false }
                }
            };

            Body = bodies[rotation];

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
            bodies = new List<bool[,]>()
            {
                new bool[,]
                {
                    { true, true, false },
                    { false, true, true },
                    { false, false, false }
                },
                new bool[,]
                {
                    { false, false, true },
                    { false, true, true },
                    { false, true, false }
                },
                new bool[,]
                {
                    { false, false, false },
                    { true, true, false },
                    { false, true, true }
                },
                new bool[,]
                {
                    { false, true, false},
                    { true, true, false },
                    { true, false, false }
                }
            };

            Body = bodies[rotation];

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