using RLNET;
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

        public void Draw(RLConsole boardConsole, RLConsole borderConsole)
        {
            foreach (Cell cell in Cells)
            {
                cell.Draw(boardConsole);
            }
            //borderConsole.SetBackColor(0, 0, Width + 2, Height + 2, RLColor.White);
            for (int x = 0; x < Width + 2; x++)
            {
                borderConsole.Set(x, 0, RLColor.White, RLColor.Black, 176);
                borderConsole.Set(x, Height + 1, RLColor.White, RLColor.Black, 176);
            }
            for (int y = 0; y < Height + 2; y++)
            {
                borderConsole.Set(0, y, RLColor.White, RLColor.Black, 176);
                borderConsole.Set(Width + 1, y, RLColor.White, RLColor.Black, 176);
            }
        }

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

            StatManager.ClearedLines(lines);
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

            for (int x = 0; x < Width - 1; x++)
            {
                cells[x, Height - 1].SetTile(Palette.Red);
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
            List<Cell> tiles = new List<Cell>();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Cells[x, y].IsTile)
                    {
                        tiles.Add(Cells[x, y].Clone());
                        Debug.WriteLine($"Cell at ({x}, {y})");
                        Cells[x, y].RemoveTile();
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