using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public static class Tetrominos
    {
        public static Tetromino GetI()
        {
            return new Tetromino(new bool[,]
            {
                { true, true, true, true }
            }, 1, 0);
        }

        public static Tetromino GetO()
        {
            return new Tetromino(new bool[,]
            {
                { true, true },
                { true, true }
            }, 1, 0);
        }

        public static Tetromino GetT()
        {
            return new Tetromino(new bool[,]
            {
                { true, true, true },
                { false, true, false }
            }, 1, 0);
        }

        public static Tetromino GetJ()
        {
            return new Tetromino(new bool[,]
            {
                { true, false, false },
                { true, true, true }
            }, 1, 0);
        }

        public static Tetromino GetL()
        {
            return new Tetromino(new bool[,]
            {
                { false, false, true },
                { true, true, true }
            }, 1, 0);
        }

        public static Tetromino GetS()
        {
            return new Tetromino(new bool[,]
            {
                { false, true, true },
                { true, true, false }
            }, 1, 0);
        }

        public static Tetromino GetZ()
        {
            return new Tetromino(new bool[,]
            {
                { true, true, false },
                { false, true, true }
            }, 1, 0);
        }

        public static List<Tetromino> GetTetrominos()
        {
            List<Tetromino> tetrominos = new List<Tetromino>
            {
                GetI(), GetO(), GetT(), GetJ(), GetL(), GetS(), GetZ()
            };
            Shuffle<Tetromino>(tetrominos);
            return tetrominos;
        }

        private static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + Game.Random.Next(n - i);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }
    }
}