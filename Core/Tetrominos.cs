using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public static class Tetrominos
    {
        public static readonly Tetromino I =
            new Tetromino(new bool[,]
            {
                { true, true, true, true }
            });

        public static readonly Tetromino O =
            new Tetromino(new bool[,]
            {
                { true, true },
                { true, true }
            });

        public static readonly Tetromino T =
            new Tetromino(new bool[,]
            {
                { true, true, true },
                { false, true, false }
            });

        public static readonly Tetromino J =
            new Tetromino(new bool[,]
            {
                { true, false, false },
                { true, true, true }
            });

        public static readonly Tetromino L =
            new Tetromino(new bool[,]
            {
                { false, false, true },
                { true, true, true }
            });

        public static readonly Tetromino S =
            new Tetromino(new bool[,]
            {
                { false, true, true },
                { true, true, false }
            });

        public static readonly Tetromino Z =
            new Tetromino(new bool[,]
            {
                { true, true, false },
                { false, true, true }
            });

        public static Tetromino GetRandomTetromino()
        {
            int randomIndex = Game.Random.Next(7);

            switch (randomIndex)
            {
                case 0:
                    return I;

                case 1:
                    return O;

                case 2:
                    return T;

                case 3:
                    return J;

                case 4:
                    return L;

                case 5:
                    return S;

                case 6:
                    return Z;

                default:
                    return null;
            }
        }
    }
}