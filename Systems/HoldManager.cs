using RLNET;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public class HoldManager
    {
        public bool HasHeld { get; set; } = false;
        private Tetromino heldTetromino;

        public void Draw(RLConsole console)
        {
            console.Print(0, 0, "HOLD", RLColor.White);
            if (heldTetromino != null)
            {
                console.Print(0, 1, heldTetromino.ToString(), heldTetromino.Color);
            }
        }

        public void HoldPiece()
        {
            if (!HasHeld)
            {
                HasHeld = true;
                heldTetromino = Game.TetrominoController.HoldTetromino(heldTetromino);
            }
        }
    }
}