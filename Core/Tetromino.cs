﻿using OpenTK.Graphics.OpenGL;
using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tetris.Systems;

namespace Tetris.Core
{
    public abstract class Tetromino
    {
        public bool[,] Body { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        // coords of top left corner
        public int X { get; set; }
        public int Y { get; set; }

        protected int rotation = 0;
        protected List<bool[,]> bodies;

        public void Initialize()
        {
            Width = Body.GetLength(0);
            Height = Body.GetLength(1);
            SetSize();
            X = 1;
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
                TetrominoController.CycleTetromino();
            }
        }

        public abstract void Rotate();

        private void SetPos(int x, int y)
        {
            ResetCells();
            X = x;
            Y = y;
            SetCells();
        }

        // check if movement is valid (no tile or boundary in way)
        private bool CanMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        if (X > 0 && !IsTileOnSide(direction))
                        {
                            return true;
                        }
                        return false;
                    }
                case Direction.Down:
                    {
                        if (Y + Height < Game.Board.Height && !IsTileOnSide(direction))
                        {
                            return true;
                        }
                        return false;
                    }
                case Direction.Right:
                    {
                        if (X + Width < Game.Board.Width && !IsTileOnSide(direction))
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

        // check for cell containing tile on given side of tetromino
        private bool IsTileOnSide(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (Game.Board.Cells[X - 1, Y + y].IsTile
                                && Game.Board.Cells[X, Y + y].IsTile)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                case Direction.Down:
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            if (Game.Board.Cells[X + x, Y + Height].IsTile
                                && Game.Board.Cells[X + x, Y + Height - 1].IsTile)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                case Direction.Right:
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (Game.Board.Cells[X + Width, Y + y].IsTile
                                && Game.Board.Cells[X + Width - 1, Y + y].IsTile)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        protected void SetCells()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Body[x, y])
                    {
                        Game.Board.Cells[X + x, Y + y].IsTile = true;
                    }
                }
            }
        }

        // remove tile from cells
        private void ResetCells()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Game.Board.Cells[X + x, Y + y].IsTile = false;
                }
            }
        }

        private void SetSize()
        {
            int height = Height;
            int width = Width;

            for (int y = 0; y < height; y++)
            {
                if (GetRow(Body, y).All(cell => cell == false))
                {
                    Debug.WriteLine($"Row {y} is blank");
                    Height--;
                }
            }
            for (int x = 0; x < width; x++)
            {
                if (GetColumn(Body, x).All(cell => cell == false))
                {
                    Debug.WriteLine($"Column {x} is blank");
                    Width--;
                }
            }
            Debug.WriteLine($"{Width}, {Height}");
        }

        private bool[] GetRow(bool[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        private bool[] GetColumn(bool[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}