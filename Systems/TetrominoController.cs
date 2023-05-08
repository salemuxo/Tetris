using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class TetrominoController
    {
        private static Tetromino Tetromino;
        private static Queue<Tetromino> Queue;

        // create queue from tetromino bag and get first tetromino from queue
        public static void Initialize()
        {
            Queue = new Queue<Tetromino>(GetTetrominoBag());
            Tetromino = GetNextTetromino();
        }

        // if game isnt paused, move falling tetromino in direction
        public static void Move(Direction direction)
        {
            if (Game.IsPlaying)
            {
                Tetromino.Move(direction);
            }
        }

        // if game isnt paused, rotate falling tetromino 90 CW
        public static void Rotate()
        {
            if (Game.IsPlaying)
            {
                Tetromino.Rotate();
            }
        }

        // if tetromino couldn't move down, check for full lines and get next tetromino
        public static void NoMoveDown()
        {
            Game.Board.CheckLines(Tetromino.Y, Tetromino.Y + Tetromino.Height);

            CycleTetromino();
        }

        // draw queue to queue console
        public static void DrawQueue(RLConsole queueConsole)
        {
            var queueArray = Queue.ToArray();
            queueConsole.Print(0, 0, "NEXT", RLColor.White);
            for (int i = 0; i <= 3; i++)
            {
                try
                {
                    queueConsole.Print(1, i + 1,
                    queueArray[i].ToString(), queueArray[i].Color);
                }
                catch
                {
                    Debug.WriteLine("New bag..");
                    GetTetrominoBag().ForEach(x => Queue.Enqueue(x));
                }
            }
        }

        // if there are tetrominos in queue, get next one, otherwise get new bag
        private static void CycleTetromino()
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

        // get next tetromino in queue and initialize
        private static Tetromino GetNextTetromino()
        {
            var nextTetromino = Queue.Dequeue();
            nextTetromino.Initialize();
            return nextTetromino;
        }

        // get bag of all tetrominos in random order
        private static List<Tetromino> GetTetrominoBag()
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

        // fisher-yates shuffle list
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