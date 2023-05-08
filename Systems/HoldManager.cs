using RLNET;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class HoldManager
    {
        public static bool HasHeld { get; set; } = false;
        private static Tetromino heldTetromino;

        public static void Draw(RLConsole console)
        {
            console.Print(0, 0, "HOLD", RLColor.White);
            if (heldTetromino != null)
            {
                console.Print(0, 1, heldTetromino.ToString(), heldTetromino.Color);
            }
        }

        public static void HoldPiece()
        {
            if (!HasHeld)
            {
                HasHeld = true;
                heldTetromino = TetrominoController.HoldTetromino(heldTetromino);
            }
        }
    }
}