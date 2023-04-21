using RLNET;
using System.Diagnostics;

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
                            break;
                        }
                    case RLKey.Down:
                        {
                            break;
                        }
                    case RLKey.Right:
                        {
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
                Debug.WriteLine(keyPress.Key);
            }
        }

        private static void HandleMouseInput(RLRootConsole rootConsole)
        {
            RLMouse mouse = rootConsole.Mouse;
        }
    }
}