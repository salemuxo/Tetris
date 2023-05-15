using OpenTK.Graphics.OpenGL;
using RLNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tetris.Systems;

namespace Tetris.Core
{
    public abstract class Tetromino
    {
        public bool[,] Body { get; protected set; }
        public int Width => Body.GetLength(0);
        public int Height => Body.GetLength(1);

        // coords of top left corner
        public int X { get; set; }
        public int Y { get; set; }

        public int LowestY
        {
            get
            {
                int y = 0;
                while (true)
                {
                    if (CheckValidPos(X, y))
                    {
                        y++;
                    }
                    else
                    {
                        return y - 1;
                    }
                }
            }
        }
        public RLColor Color { get; set; }

        protected int _rotation = 0;
        protected bool _isGhost = false;

        // rotation offsets
        private readonly int[,] _offsets = new int[4, 2];

        public void Initialize()
        {
            X = 4;
            Y = 0;
            SetCells();
        }

        public void Move(Direction direction)
        {
            if (CanMove(direction))
            {
                switch (direction)
                {
                    case Direction.Left:
                        {
                            SetPos(X - 1, Y);
                            break;
                        }
                    case Direction.Down:
                        {
                            SetPos(X, Y + 1);
                            break;
                        }
                    case Direction.Right:
                        {
                            SetPos(X + 1, Y);
                            break;
                        }
                }
            }
            else if (direction == Direction.Down)
            {
                SetCells();
                Game.TetrominoController.NoMoveDown();
            }
        }

        public void RotateCW()
        {
            ResetCells();
            switch (_rotation)
            {
                case 0:
                    RotateCWAndMove(X + _offsets[0, 0], Y + _offsets[0, 1]);
                    break;

                case 1:
                    RotateCWAndMove(X + _offsets[1, 0], Y + _offsets[1, 1]);
                    break;

                case 2:
                    RotateCWAndMove(X + _offsets[2, 0], Y + _offsets[2, 1]);
                    break;

                case 3:
                    RotateCWAndMove(X + _offsets[3, 0], Y + _offsets[3, 1]);
                    break;
            }
        }

        public void RotateCCW()
        {
            ResetCells();
            switch (_rotation)
            {
                case 0:
                    RotateCCWAndMove(X - _offsets[3, 0], Y - _offsets[3, 1]);
                    break;

                case 1:
                    RotateCCWAndMove(X - _offsets[0, 0], Y - _offsets[0, 1]);
                    break;

                case 2:
                    RotateCCWAndMove(X - _offsets[1, 0], Y - _offsets[1, 1]);
                    break;

                case 3:
                    RotateCCWAndMove(X - _offsets[2, 0], Y - _offsets[2, 1]);
                    break;
            }
        }

        public void SetPos(int x, int y)
        {
            ResetCells();
            X = x;
            Y = y;
            SetCells();
        }

        public Tetromino Clone()
        {
            return this.MemberwiseClone() as Tetromino;
        }

        // set all cells occupied by tetromino to tile
        public virtual void SetCells()
        {
            if (!_isGhost)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (Body[x, y])
                        {
                            Game.Board.Cells[X + x, Y + y].SetTile(Color);
                        }
                    }
                }
            }
            // ghost set
            else
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (Body[x, y])
                        {
                            Game.Board.Cells[X + x, Y + y].SetGhost(Color);
                        }
                    }
                }
            }
        }

        // remove tile from cells
        public virtual void ResetCells()
        {
            if (!_isGhost)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (Body[x, y])
                        {
                            try
                            {
                                Game.Board.Cells[X + x, Y + y].RemoveTile();
                            }
                            catch
                            {
                                Debug.WriteLine("Can't reset");
                            }
                        }
                    }
                }
            }
            // ghost reset
            else
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (Body[x, y])
                        {
                            try
                            {
                                Game.Board.Cells[X + x, Y + y].RemoveGhost();
                            }
                            catch
                            {
                                Debug.WriteLine("Can't reset");
                            }
                        }
                    }
                }
            }
        }

        // create ghost piece from this
        public Tetromino CreateGhost()
        {
            var ghostTetromino = Clone();
            ghostTetromino._isGhost = true;
            ghostTetromino.SetPos(X, LowestY);
            return ghostTetromino;
        }

        protected void SetRotationOffsets(
            int aX, int aY, int bX, int bY, int cX, int cY, int dX, int dY)
        {
            _offsets[0, 0] = aX;
            _offsets[0, 1] = aY;
            _offsets[1, 0] = bX;
            _offsets[1, 1] = bY;
            _offsets[2, 0] = cX;
            _offsets[2, 1] = cY;
            _offsets[3, 0] = dX;
            _offsets[3, 1] = dY;
        }

        // rotate matrix 90 degrees clockwise
        private bool[,] RotateMatrixCW(bool[,] matrix)
        {
            bool[,] result = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            int maxX = matrix.GetUpperBound(0);
            int maxY = matrix.GetUpperBound(1);

            for (int row = 0; row <= maxX; row++)
            {
                for (int col = 0; col <= (maxY / 2); col++)
                {
                    result[row, col] = matrix[row, maxY - col];
                    result[row, maxY - col] = matrix[row, col];
                }
            }

            result = TransposeMatrix(result);

            return result;
        }

        // rotate matrix 90 degrees counter clockwise
        private bool[,] RotateMatrixCCW(bool[,] matrix)
        {
            matrix = TransposeMatrix(matrix);

            bool[,] result = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            int maxX = matrix.GetUpperBound(0);
            int maxY = matrix.GetUpperBound(1);

            for (int row = 0; row <= maxX; row++)
            {
                for (int col = 0; col <= (maxY / 2); col++)
                {
                    result[row, col] = matrix[row, maxY - col];
                    result[row, maxY - col] = matrix[row, col];
                }
            }

            return result;
        }

        private void SetNewBody(bool[,] newBody)
        {
            ResetCells();
            Body = newBody;
        }

        // rotate tetromino and move to keep correct center point
        protected void RotateCWAndMove(int x, int y)
        {
            var rotatedBody = RotateMatrixCW(Body);
            var kickInfo = CheckKicksCW(x, y, rotatedBody);

            if (kickInfo.canRotate)
            {
                SetNewBody(rotatedBody);
                SetPos(kickInfo.x, kickInfo.y);

                if (_rotation == 3)
                {
                    _rotation = 0;
                }
                else
                {
                    _rotation++;
                }
            }
        }

        // rotate tetromino and move to keep correct center point
        protected void RotateCCWAndMove(int x, int y)
        {
            var rotatedBody = RotateMatrixCCW(Body);
            var kickInfo = CheckKicksCCW(x, y, rotatedBody);

            if (kickInfo.canRotate)
            {
                SetNewBody(rotatedBody);
                SetPos(kickInfo.x, kickInfo.y);

                if (_rotation == 0)
                {
                    _rotation = 3;
                }
                else
                {
                    _rotation--;
                }
            }
        }

        // check if movement is valid (no tile or boundary in way)
        private bool CanMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        if (X > 0 && CheckValidPos(X - 1, Y))
                        {
                            return true;
                        }
                        return false;
                    }
                case Direction.Down:
                    {
                        if (Y + Height < Game.Board.Height && CheckValidPos(X, Y + 1))
                        {
                            return true;
                        }
                        return false;
                    }
                case Direction.Right:
                    {
                        if (X + Width < Game.Board.Width && CheckValidPos(X + 1, Y))
                        {
                            return true;
                        }
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        // check if body position is valid (no tiles in way)
        public bool CheckValidPos(int newX, int newY)
        {
            ResetCells();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    try
                    {
                        if (Body[x, y] && Game.Board.Cells[newX + x, newY + y].IsTile)
                        {
                            SetCells();
                            return false;
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("Invalid position");
                        SetCells();
                        return false;
                    }
                }
            }
            SetCells();
            return true;
        }

        // check if new body and position is valid
        private bool CheckValidPos(int newX, int newY, bool[,] newBody)
        {
            ResetCells();
            int w = newBody.GetLength(0);
            int h = newBody.GetLength(1);
            for (int x = 0; x < w; x++)
            {
                try
                {
                    for (int y = 0; y < h; y++)
                    {
                        if (newBody[x, y] && Game.Board.Cells[newX + x, newY + y].IsTile)
                        {
                            SetCells();
                            return false;
                        }
                    }
                }
                catch
                {
                    Debug.WriteLine("Invalid position");
                    SetCells();
                    return false;
                }
            }
            SetCells();
            return true;
        }

        // transpose matrix (swap x and y)
        private bool[,] TransposeMatrix(bool[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            bool[,] result = new bool[h, w];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    result[y, x] = matrix[x, y];
                }
            }

            return result;
        }

        // CW check for wall kick, return whether rotation is possible and coords after kick
        private (bool canRotate, int x, int y) CheckKicksCW(int x, int y, bool[,] newBody)
        {
            bool canRotate = true;
            int newX = x;
            int newY = y;

            switch (_rotation)
            {
                case 0:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x - 1, y, newBody))
                        {
                            newX = x - 1;
                        }
                        else if (CheckValidPos(x - 1, y - 1, newBody))
                        {
                            newX = x - 1;
                            newY = y - 1;
                        }
                        else if (CheckValidPos(x, y + 2, newBody))
                        {
                            newY = y + 2;
                        }
                        else if (CheckValidPos(x - 1, y + 2, newBody))
                        {
                            newX = x - 1;
                            newY = y + 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
                case 1:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x + 1, y, newBody))
                        {
                            newX = x + 1;
                        }
                        else if (CheckValidPos(x + 1, y + 1, newBody))
                        {
                            newX = x + 1;
                            newY = y + 1;
                        }
                        else if (CheckValidPos(x, y - 2, newBody))
                        {
                            newY = y - 2;
                        }
                        else if (CheckValidPos(x + 1, y - 2, newBody))
                        {
                            newX = x + 1;
                            newY = y - 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x + 1, y, newBody))
                        {
                            newX = x + 1;
                        }
                        else if (CheckValidPos(x + 1, y - 1, newBody))
                        {
                            newX = x + 1;
                            newY = y - 1;
                        }
                        else if (CheckValidPos(x, y + 2, newBody))
                        {
                            newY = y + 2;
                        }
                        else if (CheckValidPos(x + 1, y + 2, newBody))
                        {
                            newX = x + 1;
                            newY = y + 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
                case 3:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x - 1, y, newBody))
                        {
                            newX = x - 1;
                        }
                        else if (CheckValidPos(x - 1, y + 1, newBody))
                        {
                            newX = x - 1;
                            newY = y + 1;
                        }
                        else if (CheckValidPos(x, y - 2, newBody))
                        {
                            newY = y - 2;
                        }
                        else if (CheckValidPos(x - 1, y - 2, newBody))
                        {
                            newX = x - 1;
                            newY = y - 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
            }

            return (canRotate, newX, newY);
        }

        // CW check for wall kick, return whether rotation is possible and coords after kick
        private (bool canRotate, int x, int y) CheckKicksCCW(int x, int y, bool[,] newBody)
        {
            bool canRotate = true;
            int newX = x;
            int newY = y;

            switch (_rotation)
            {
                case 0:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x + 1, y, newBody))
                        {
                            newX = x + 1;
                        }
                        else if (CheckValidPos(x + 1, y - 1, newBody))
                        {
                            newX = x + 1;
                            newY = y - 1;
                        }
                        else if (CheckValidPos(x, y + 2, newBody))
                        {
                            newY = y + 2;
                        }
                        else if (CheckValidPos(x + 1, y + 2, newBody))
                        {
                            newX = x + 1;
                            newY = y + 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
                case 1:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x + 1, y, newBody))
                        {
                            newX = x + 1;
                        }
                        else if (CheckValidPos(x + 1, y + 1, newBody))
                        {
                            newX = x + 1;
                            newY = y + 1;
                        }
                        else if (CheckValidPos(x, y - 2, newBody))
                        {
                            newY = y - 2;
                        }
                        else if (CheckValidPos(x + 1, y - 2, newBody))
                        {
                            newX = x + 1;
                            newY = y - 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x - 1, y, newBody))
                        {
                            newX = x - 1;
                        }
                        else if (CheckValidPos(x - 1, y - 1, newBody))
                        {
                            newX = x - 1;
                            newY = y - 1;
                        }
                        else if (CheckValidPos(x, y + 2, newBody))
                        {
                            newY = y + 2;
                        }
                        else if (CheckValidPos(x - 1, y + 2, newBody))
                        {
                            newX = x - 1;
                            newY = y + 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
                case 3:
                    {
                        if (CheckValidPos(x, y, newBody))
                        {
                        }
                        else if (CheckValidPos(x - 1, y, newBody))
                        {
                            newX = x - 1;
                        }
                        else if (CheckValidPos(x - 1, y + 1, newBody))
                        {
                            newX = x - 1;
                            newY = y + 1;
                        }
                        else if (CheckValidPos(x, y - 2, newBody))
                        {
                            newY = y - 2;
                        }
                        else if (CheckValidPos(x - 1, y - 2, newBody))
                        {
                            newX = x - 1;
                            newY = y - 2;
                        }
                        else
                        {
                            canRotate = false;
                        }
                        break;
                    }
            }

            return (canRotate, newX, newY);
        }
    }
}