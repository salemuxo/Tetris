using RLNET;
using System.Diagnostics;
using Tetris.Core;

namespace Tetris.Systems
{
    public static class InputHandler
    {
        public static void HandleInput(RLRootConsole rootConsole)
        {
            HandleKeyInput(rootConsole);
            HandleMouseInput(rootConsole);
        }

        private static void HandleKeyInput(RLRootConsole rootConsole)
        {
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();
            if (keyPress != null)
            {
                //Debug.WriteLine(keyPress.Key);
                switch (keyPress.Key)
                {
                    // rotate clockwise
                    case RLKey.Up:
                    case RLKey.X:
                        {
                            TetrominoController.Rotate();
                            break;
                        }
                    // move left
                    case RLKey.Left:
                        {
                            TetrominoController.Move(Direction.Left);
                            break;
                        }
                    // move down
                    case RLKey.Down:
                        {
                            TetrominoController.Move(Direction.Down);
                            break;
                        }
                    // move right
                    case RLKey.Right:
                        {
                            TetrominoController.Move(Direction.Right);
                            break;
                        }
                    // hard drop
                    case RLKey.Space:
                        {
                            TetrominoController.HardDrop();
                            break;
                        }
                    // hold
                    case RLKey.LShift:
                    case RLKey.RShift:
                    case RLKey.C:
                        {
                            HoldManager.HoldPiece();
                            break;
                        }
                    // pause
                    case RLKey.Escape:
                    case RLKey.F1:
                        {
                            Game.IsPlaying = !Game.IsPlaying;
                            break;
                        }
                }
            }
        }

        private static void HandleMouseInput(RLRootConsole rootConsole)
        {
            RLMouse mouse = rootConsole.Mouse;
        }
    }
}