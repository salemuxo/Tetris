using RLNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.Systems;

namespace Tetris.Core
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Cell[,] Cells { get; set; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Cells = CreateCells();
        }

        private double elapsedTime = 0;

        public void Update(double deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= TimeManager.UpdateTime)
            {
                //TetrominoController.Move(Direction.Down);
                elapsedTime = 0;
            }
        }

        public void Draw(RLConsole boardConsole)
        {
            foreach (Cell cell in Cells)
            {
                cell.Draw(boardConsole);
            }
        }

        public void CheckLine(int y)
        {
            bool isLine = true;
            for (int x = 0; x < Width; x++)
            {
                if (!Cells[x, y].IsTile)
                {
                    isLine = false;
                }
            }

            if (isLine)
            {
                //ClearLine(y);
            }
        }

        // return 2d array of blank cells
        private Cell[,] CreateCells()
        {
            Cell[,] cells = new Cell[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }

            return cells;
        }

        private void ClearLine(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                Cells[x, y].IsTile = false;
            }
            MoveAllDown(y);
        }

        private void MoveAllDown(int maxY)
        {
            Cell[,] oldCells = Cells;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < maxY - 1; y++)
                {
                    if (oldCells[x, y].IsTile)
                    {
                        Cells[x, y].IsTile = false;
                        Cells[x, y + 1].IsTile = true;
                    }
                }
            }
        }
    }
}