using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class TetrominoController
    {
        private static Tetromino FallingTetromino;
        private static Tetromino GhostTetromino;
        private static Queue<Tetromino> Queue;

        private static double elapsedTime = 0;

        public static void Update(double deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= TimeManager.UpdateTime)
            {
                Move(Direction.Down);
                elapsedTime = 0;
            }
        }

        // create queue from tetromino bag and get first tetromino from queue
        public static void Initialize()
        {
            Queue = new Queue<Tetromino>(GetTetrominoBag());
            GetNextTetromino();
        }

        // if game isnt paused, move falling tetromino in direction
        public static void Move(Direction direction)
        {
            if (Game.IsPlaying)
            {
                FallingTetromino.Move(direction);
                SetGhost();
            }
        }

        // if game isnt paused, rotate falling tetromino 90 CW
        public static void Rotate()
        {
            if (Game.IsPlaying)
            {
                FallingTetromino.Rotate();
                RotateGhost();
            }
        }

        public static void HardDrop()
        {
            FallingTetromino.SetPos(FallingTetromino.X, FallingTetromino.GetLowestY());
            NoMoveDown();
        }

        // if tetromino couldn't move down, check for full lines and get next tetromino
        public static void NoMoveDown()
        {
            Game.Board.CheckLines(FallingTetromino.Y, FallingTetromino.Y + FallingTetromino.Height);
            HoldManager.HasHeld = false;
            GetNextTetromino();
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
                    Debug.WriteLine("New bag...");
                    GetTetrominoBag().ForEach(x => Queue.Enqueue(x));
                }
            }
        }

        public static Tetromino HoldTetromino(Tetromino heldTetromino)
        {
            var holdTetromino = FallingTetromino;
            FallingTetromino.ResetCells();

            if (heldTetromino == null)
            {
                GetNextTetromino();
            }
            else
            {
                SetFallingTetromino(heldTetromino);
            }

            return holdTetromino;
        }

        // get next tetromino in queue and initialize
        private static void GetNextTetromino()
        {
            var nextTetromino = Queue.Dequeue();
            nextTetromino.Initialize();
            FallingTetromino = nextTetromino;
            GhostTetromino = nextTetromino.CreateGhost();
        }

        private static void SetFallingTetromino(Tetromino tetromino)
        {
            tetromino.Initialize();
            FallingTetromino = tetromino;
            GhostTetromino = tetromino.CreateGhost();
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

        // fisher-yates shuffle algorithm for list
        private static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + Game.Random.Next(n - i);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        private static void RotateGhost()
        {
            ResetGhost();
            GhostTetromino.Rotate();
            SetGhost();
        }

        private static void ResetGhost()
        {
            GhostTetromino.SetPos(FallingTetromino.X, FallingTetromino.Y);
        }

        private static void SetGhost()
        {
            GhostTetromino.SetPos(FallingTetromino.X, FallingTetromino.GetLowestY());
            FallingTetromino.SetCells();
        }
    }
}