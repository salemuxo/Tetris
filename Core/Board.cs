using RLNET;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris.Core
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> StaticTiles { get; set; }
        public List<Tetromino> FallingTetrominos { get; set; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            StaticTiles = new List<Tile>();
            FallingTetrominos = new List<Tetromino>();
        }

        public void Update()
        {
            foreach (var tetromino in FallingTetrominos)
            {
                tetromino.MoveDown();
            }
        }

        public void Draw(RLConsole boardConsole)
        {
        }
    }
}