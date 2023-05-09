using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class GhostManager
    {
        private static Tetromino GhostTetromino;

        public static void Set()
        {
            GhostTetromino?.ResetCells();
            GhostTetromino = TetrominoController.FallingTetromino.CreateGhost();
            Move();
        }

        public static void Move()
        {
            GhostTetromino.SetPos(TetrominoController.FallingTetromino.X,
                TetrominoController.FallingTetromino.GetLowestY());
        }
    }
}