using RLNET;
using Tetris.Core;

namespace Tetris.Systems
{
    public class HoldManager
    {
        public bool CanHold { get; set; } = true;
        private Tetromino _heldTetromino;

        public void Draw(RLConsole console)
        {
            console.Print(0, 0, "HOLD", Palette.Text);
            if (_heldTetromino != null)
            {
                console.Print(0, 1, _heldTetromino.ToString(), _heldTetromino.Color);
            }
        }

        // if hasnt held this turn, set held tetromino to current falling and get new
        public void HoldPiece()
        {
            if (CanHold)
            {
                CanHold = false;
                _heldTetromino = Game.TetrominoController.HoldTetromino(_heldTetromino);
            }
        }
    }
}