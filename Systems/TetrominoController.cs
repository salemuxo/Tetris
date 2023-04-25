using System.Collections.Generic;
using System.Security.Cryptography;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class TetrominoController
    {
        private static Tetromino Tetromino;
        private static Queue<Tetromino> Queue;

        public static void Initialize()
        {
            Queue = new Queue<Tetromino>(Tetrominos.GetTetrominos());
            Tetromino = Queue.Dequeue();
        }

        public static void Move(Direction direction)
        {
            Tetromino.Move(direction);
        }

        public static void CycleTetromino()
        {
            if (Queue.Count > 0)
            {
                Tetromino = Queue.Dequeue();
            }
            else
            {
                Initialize();
            }
        }
    }
}