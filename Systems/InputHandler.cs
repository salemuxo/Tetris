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
                switch (keyPress.Key)
                {
                    case RLKey.Up:
                        {
                            break;
                        }
                    case RLKey.Left:
                        {
                            TetrominoController.Move(Direction.Left);
                            break;
                        }
                    case RLKey.Down:
                        {
                            TetrominoController.Move(Direction.Down);
                            break;
                        }
                    case RLKey.Right:
                        {
                            TetrominoController.Move(Direction.Right);
                            break;
                        }
                    case RLKey.Space:
                        {
                            break;
                        }
                    case RLKey.Escape:
                        {
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