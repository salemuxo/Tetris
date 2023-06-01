using RLNET;
using Tetris.Core;

namespace Tetris.Systems
{
    public class HoldManager
    {
        public bool HasHeld { get; set; } = false;
        private Tetromino _heldTetromino;

        public void Draw(RLConsole console)
        {
            console.Print(0, 0, "HOLD", Palette.Text);
            if (_heldTetromino != null)
            {
                console.Print(0, 1, _heldTetromino.ToString(), _heldTetromino.Color);
            }
        }

        public void HoldPiece()
        {
            if (!HasHeld)
            {
                HasHeld = true;
                _heldTetromino = Game.TetrominoController.HoldTetromino(_heldTetromino);
            }
        }
    }
}