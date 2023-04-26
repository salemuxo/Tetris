using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class TetrominoController
    {
        private static Tetromino Tetromino;
        private static Queue<Tetromino> Queue;

        public static void Initialize()
        {
            Queue = new Queue<Tetromino>(GetTetrominos());
            Tetromino = GetNextTetromino();
        }

        public static void Move(Direction direction)
        {
            Tetromino.Move(direction);
        }

        public static void CycleTetromino()
        {
            if (Queue.Count > 0)
            {
                Debug.WriteLine("Cycling...");
                Tetromino = GetNextTetromino();
            }
            else
            {
                Debug.WriteLine("New bag...");
                Initialize();
            }
        }

        private static Tetromino GetNextTetromino()
        {
            var nextTetromino = Queue.Dequeue();
            nextTetromino.Initialize();
            return nextTetromino;
        }

        private static List<Tetromino> GetTetrominos()
        {
            List<Tetromino> tetrominos = new List<Tetromino>
            {
                new I(), new O(), new T(),
                new J(), new L(),
                new S(), new Z()
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