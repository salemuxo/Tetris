using Tetris.Core;

namespace Tetris.Systems
{
    public class GhostManager
    {
        private Tetromino _ghost;

        // remove old ghost and set new ghost based on falling tetromino
        public void Set()
        {
            _ghost?.ResetCells();
            _ghost = Game.TetrominoController.FallingTetromino.CreateGhost();
            Move();
        }

        // move ghost to be under falling tetromino
        public void Move()
        {
            var lowestY = Game.TetrominoController.FallingTetromino.LowestY;

            if (lowestY != -1)
            {
                _ghost.SetPos(Game.TetrominoController.FallingTetromino.X,
                lowestY);
            }
        }
    }
}