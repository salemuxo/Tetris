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
            Cells = new Cell[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y] = new Cell(x, y);
                }
            }
        }

        private double elapsedTime = 0;

        public void Update(double deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= TimeManager.UpdateTime)
            {
                TetrominoController.Move(Direction.Down);
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
    }
}