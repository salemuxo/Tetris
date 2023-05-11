using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Core;

namespace Tetris.Systems
{
    public class GhostManager
    {
        private Tetromino GhostTetromino;

        public void Set()
        {
            GhostTetromino?.ResetCells();
            GhostTetromino = Game.TetrominoController.FallingTetromino.CreateGhost();
            Move();
        }

        public void Move()
        {
            var lowestY = Game.TetrominoController.FallingTetromino.GetLowestY();

            if (lowestY != -1)
            {
                GhostTetromino.SetPos(Game.TetrominoController.FallingTetromino.X,
                lowestY);
            }
        }
    }
}