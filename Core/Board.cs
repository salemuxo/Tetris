﻿using RLNET;
using System.Collections.Generic;

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

        // draw board and border
        public void Draw(RLConsole boardConsole, RLConsole borderConsole)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y].Draw(boardConsole);
                }
            }

            // create border
            for (int x = 0; x < Width + 2; x++)
            {
                borderConsole.Set(x, 0, Palette.Text, RLColor.Black, 176);
                borderConsole.Set(x, Height + 1, Palette.Text, RLColor.Black, 176);
            }
            for (int y = 0; y < Height + 2; y++)
            {
                borderConsole.Set(0, y, Palette.Text, RLColor.Black, 176);
                borderConsole.Set(Width + 1, y, Palette.Text, RLColor.Black, 176);
            }
        }

        // check for cleared lines, clear, add score
        public void CheckLines(int minY, int maxY)
        {
            int lines = 0;
            for (int y = minY; y < maxY; y++)
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
                    ClearLine(y);
                    lines++;
                }
            }

            Game.StatManager.ClearedLines(lines);
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

        // called when line is filled, removes tiles from line and moves above tiles down
        private void ClearLine(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                Cells[x, y].ClearCell();
            }
            MoveAllDown(y);
        }

        // move all tiles above maxY down 1
        private void MoveAllDown(int maxY)
        {
            List<Cell> tiles = new List<Cell>();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // if tile, add clone to list and remove tile
                    if (Cells[x, y].IsTile)
                    {
                        tiles.Add(Cells[x, y].Clone());
                        Cells[x, y].ClearCell();
                    }
                }
            }

            foreach (var tile in tiles)
            {
                tile.Y++;
                Cells[tile.X, tile.Y] = tile;
            }
        }
    }
}