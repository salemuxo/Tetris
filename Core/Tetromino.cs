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
        public RLColor Color { get; set; }
        protected int rotation = 1;

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
                TetrominoController.NoMoveDown();
            }
        }

        public abstract void Rotate();

        public void SetPos(int x, int y)
        {
            ResetCells();
            X = x;
            Y = y;
            SetCells();
        }

        // set all cells occupied by tetromino to tile
        protected void SetCells()
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

        // remove tile from cells
        protected void ResetCells()
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
                        }
                    }
                }
            }
        }

        // rotate matrix 90 degrees clockwise
        protected bool[,] RotateMatrix(bool[,] matrix)
        {
            //matrix = TransposeMatrix(matrix);

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

        protected void SetNewBody(bool[,] newBody)
        {
            ResetCells();
            Body = newBody;
        }

        // rotate tetromino and move to keep correct center point
        protected void RotateAndMove(int x, int y)
        {
            var rotatedBody = RotateMatrix(Body);
            if (CheckValidPos(x, y, rotatedBody))
            {
                SetNewBody(rotatedBody);
                SetPos(x, y);
                if (rotation == 3)
                {
                    rotation = 0;
                }
                else
                {
                    rotation++;
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
        private bool CheckValidPos(int newX, int newY)
        {
            ResetCells();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Body[x, y] && Game.Board.Cells[newX + x, newY + y].IsTile)
                    {
                        SetCells();
                        return false;
                    }
                }
            }
            SetCells();
            return true;
        }

        private bool CheckValidPos(int newX, int newY, bool[,] newBody)
        {
            ResetCells();
            int w = newBody.GetLength(0);
            int h = newBody.GetLength(1);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    try
                    {
                        if (newBody[x, y] && Game.Board.Cells[newX + x, newY + y].IsTile)
                        {
                            SetCells();
                            return false;
                        }
                    }
                    catch
                    {
                        SetCells();
                        return false;
                    }
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
    }
}