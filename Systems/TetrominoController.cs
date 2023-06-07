using RLNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public class TetrominoController
    {
        // config variables
        private const double _dasDelay = 167;
        private const double _dasRate = 33;

        // other variables
        public Tetromino FallingTetromino { get; private set; }
        public bool IsSoftDropping;

        private readonly Queue<Tetromino> _queue;
        private double _dropTime = 0;
        private double _graceTime = 0;
        private bool _isGraceTurn = false;

        // direction of horizontal movement
        public Direction? DasDirection
        {
            get
            {
                return _dasDirection;
            }
            set
            {
                _dasTime = 0;
                isDasMoving = false;
                _dasDirection = value;
            }
        }
        private Direction? _dasDirection;
        private double _dasTime = 0;
        private bool isDasMoving = false;

        public void Update(double deltaTime)
        {
            if (!_isGraceTurn)
            {
                // move down automatically
                _dropTime += deltaTime;
                if (_dropTime >= Game.TimeManager.UpdateTime)
                {
                    Move(Direction.Down);
                    _dropTime = 0;

                    if (IsSoftDropping)
                    {
                        Game.StatManager.Score++;
                    }
                }

                // DAS (horizontal movement)
                if (_dasDirection != null)
                {
                    _dasTime += deltaTime;
                    if (!isDasMoving)
                    {
                        if (_dasTime >= _dasDelay)
                        {
                            isDasMoving = true;
                            _dasTime = 0;
                        }
                    }
                    else
                    {
                        if (_dasTime >= _dasRate)
                        {
                            Move((Direction)_dasDirection);
                            _dasTime = 0;
                        }
                    }
                }
            }
            else
            {
                // grace turn
                _graceTime += deltaTime;
                if (_graceTime >= 500)
                {
                    _isGraceTurn = false;
                    _graceTime = 0;
                    PlaceTetromino();
                }
            }
        }

        // create queue from tetromino bag and get first tetromino from queue
        public TetrominoController()
        {
            _queue = new Queue<Tetromino>(GetTetrominoBag());
        }

        // set first tetromino -- starts game
        public void Start()
        {
            GetNextTetromino();
        }

        // if game isnt paused, move falling tetromino in direction
        public void Move(Direction direction)
        {
            if (Game.IsPlaying)
            {
                _isGraceTurn = false;
                FallingTetromino.Move(direction);
                if (direction != Direction.Down)
                {
                    Game.GhostManager.Move();
                    _isGraceTurn = false;
                }
            }
        }

        // if game isnt paused, rotate falling tetromino 90 CW
        public void RotateCW()
        {
            if (Game.IsPlaying)
            {
                _isGraceTurn = false;
                FallingTetromino.RotateCW();
                Game.GhostManager.Set();
            }
        }

        // if game isnt paused, rotate falling tetromino 90 CCW
        public void RotateCCW()
        {
            if (Game.IsPlaying)
            {
                _isGraceTurn = false;
                FallingTetromino.RotateCCW();
                Game.GhostManager.Set();
            }
        }

        // instantly drop tetromino to lowest point
        public void HardDrop()
        {
            int droppedTiles = FallingTetromino.LowestY - FallingTetromino.Y;
            FallingTetromino.SetPos(FallingTetromino.X, FallingTetromino.LowestY);
            PlaceTetromino();

            Game.StatManager.HardDrop(droppedTiles);
        }

        // set grace turn (half second before tetromino is placed)
        public void NoMoveDown()
        {
            _isGraceTurn = true;
            //PlaceTetromino();
        }

        // check for full lines and get next tetromino
        public void PlaceTetromino()
        {
            Game.Board.CheckLines(FallingTetromino.Y, FallingTetromino.Y + FallingTetromino.Height);
            Game.HoldManager.CanHold = true;
            GetNextTetromino();
        }

        // draw queue to queue console
        public void DrawQueue(RLConsole queueConsole)
        {
            var queueArray = _queue.ToArray();
            queueConsole.Print(0, 0, "NEXT", Palette.Text);
            for (int i = 0; i < 3; i++)
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
                SetFallingTetromino(heldTetromino.Clone());
            }

            return holdTetromino;
        }

        // get next tetromino in queue and initialize
        private void GetNextTetromino()
        {
            var nextTetromino = _queue.Dequeue();
            SetFallingTetromino(nextTetromino);
        }

        // set falling tetromino to specific tetromino
        private void SetFallingTetromino(Tetromino tetromino)
        {
            if (!tetromino.CheckValidPos(tetromino.StartingX, 0, false))
            {
                Game.MessageLog.Add("GAME OVER!", 10000);
                Program.Game.GameOver();
            }

            tetromino.Initialize();
            FallingTetromino = tetromino;
            Game.GhostManager.Set();
            _dropTime = 0;
            _dasDirection = null;
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
                int r = i + Program.Random.Next(n - i);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }
    }
}