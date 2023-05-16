using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public class TetrominoController
    {
        public Tetromino FallingTetromino { get; private set; }

        private readonly Queue<Tetromino> _queue;
        private double _elapsedTime = 0;

        public void Update(double deltaTime)
        {
            _elapsedTime += deltaTime;
            if (_elapsedTime >= Game.TimeManager.UpdateTime)
            {
                Move(Direction.Down);
                _elapsedTime = 0;

                if (Game.TimeManager.IsSoftDropping)
                {
                    Game.StatManager.Score++;
                }
            }
        }

        // create queue from tetromino bag and get first tetromino from queue
        public TetrominoController()
        {
            _queue = new Queue<Tetromino>(GetTetrominoBag());
        }

        public void Start()
        {
            GetNextTetromino();
        }

        // if game isnt paused, move falling tetromino in direction
        public void Move(Direction direction)
        {
            if (Game.IsPlaying)
            {
                FallingTetromino.Move(direction);
                if (direction != Direction.Down)
                {
                    Game.GhostManager.Move();
                }
            }
        }

        // if game isnt paused, rotate falling tetromino 90 CW
        public void RotateCW()
        {
            if (Game.IsPlaying)
            {
                FallingTetromino.RotateCW();
                Game.GhostManager.Set();
            }
        }

        // if game isnt paused, rotate falling tetromino 90 CCW
        public void RotateCCW()
        {
            if (Game.IsPlaying)
            {
                FallingTetromino.RotateCCW();
                Game.GhostManager.Set();
            }
        }

        // instantly drop tetromino to lowest point
        public void HardDrop()
        {
            int droppedTiles = FallingTetromino.LowestY - FallingTetromino.Y;
            FallingTetromino.SetPos(FallingTetromino.X, FallingTetromino.LowestY);
            NoMoveDown();

            Game.StatManager.HardDrop(droppedTiles);
        }

        // if tetromino couldn't move down, check for full lines and get next tetromino
        public void NoMoveDown()
        {
            Game.Board.CheckLines(FallingTetromino.Y, FallingTetromino.Y + FallingTetromino.Height);
            Game.HoldManager.HasHeld = false;
            GetNextTetromino();
        }

        // draw queue to queue console
        public void DrawQueue(RLConsole queueConsole)
        {
            var queueArray = _queue.ToArray();
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
                    GetTetrominoBag().ForEach(x => _queue.Enqueue(x));
                }
            }
        }

        // if hold is empty, put falling in hold and get next, otherwise swap falling and held
        public Tetromino HoldTetromino(Tetromino heldTetromino)
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

        public void ResetTimer()
        {
            _elapsedTime = Game.TimeManager.UpdateTime;
        }

        // get next tetromino in queue and initialize
        private void GetNextTetromino()
        {
            var nextTetromino = _queue.Dequeue();
            nextTetromino.Initialize();

            //if (!nextTetromino.CheckValidPos(4, 0))
            //{
            //}

            FallingTetromino = nextTetromino;
            Game.GhostManager.Set();
        }

        // set falling tetromino to specific tetromino
        private void SetFallingTetromino(Tetromino tetromino)
        {
            tetromino.Initialize();
            FallingTetromino = tetromino;
            Game.GhostManager.Set();
        }

        // get bag of all tetrominos in random order
        private List<Tetromino> GetTetrominoBag()
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
        private void Shuffle<T>(List<T> list)
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