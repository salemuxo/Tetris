using RLNET;
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
            RLKey? keyRelease = rootConsole.Keyboard.GetKeyRelease();

            switch (Program.GameState)
            {
                case GameState.MainMenu:
                    {
                        if (keyPress != null)
                        {
                            if (keyPress.Key == RLKey.Escape)
                            {
                                Program.Close();
                            }
                        }
                        break;
                    }
                case GameState.Playing:
                    {
                        // released key
                        switch (keyRelease)
                        {
                            // stop soft drop
                            case RLKey.Down:
                                {
                                    Game.TetrominoController.IsSoftDropping = false;
                                    break;
                                }
                            case RLKey.Left:
                            case RLKey.Right:
                                {
                                    Game.TetrominoController.DasDirection = null;
                                    break;
                                }
                        }

                        if (keyPress != null && !keyPress.Repeating)
                        {
                            //Debug.WriteLine(keyPress.Key);
                            switch (keyPress.Key)
                            {
                                // rotate counter clockwise
                                case RLKey.LControl:
                                case RLKey.Z:
                                    {
                                        Game.TetrominoController.RotateCCW();
                                        break;
                                    }
                                // rotate clockwise
                                case RLKey.Up:
                                case RLKey.X:
                                    {
                                        Game.TetrominoController.RotateCW();
                                        break;
                                    }
                                // move left
                                case RLKey.Left:
                                    {
                                        Game.TetrominoController.Move(Direction.Left);
                                        Game.TetrominoController.DasDirection = Direction.Left;
                                        break;
                                    }
                                // move down
                                case RLKey.Down:
                                    {
                                        Game.TetrominoController.IsSoftDropping = true;
                                        //Game.TetrominoController.Move(Direction.Down);
                                        break;
                                    }
                                // move right
                                case RLKey.Right:
                                    {
                                        Game.TetrominoController.Move(Direction.Right);
                                        Game.TetrominoController.DasDirection = Direction.Right;
                                        break;
                                    }
                                // hard drop
                                case RLKey.Space:
                                    {
                                        Game.TetrominoController.HardDrop();
                                        break;
                                    }
                                // hold
                                case RLKey.LShift:
                                case RLKey.RShift:
                                case RLKey.C:
                                    {
                                        Game.HoldManager.HoldPiece();
                                        break;
                                    }
                                // pause
                                case RLKey.Escape:
                                case RLKey.F1:
                                    {
                                        Game.IsPlaying = !Game.IsPlaying;
                                        break;
                                    }
                                case RLKey.R:
                                    {
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case GameState.SavingScore:
                    {
                        if (keyPress != null)
                        {
                            switch (keyPress.Key)
                            {
                                case RLKey.BackSpace:
                                    {
                                        Program.GameOverMenu.NameBox.Remove();
                                        break;
                                    }
                                case RLKey.Enter:
                                    {
                                        Program.GameOverMenu.SaveScore();
                                        break;
                                    }
                                default:
                                    {
                                        Program.GameOverMenu.NameBox.Add(keyPress.Char);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
            }
        }

        private static void HandleMouseInput(RLRootConsole rootConsole)
        {
            RLMouse mouse = rootConsole.Mouse;
            switch (Program.GameState)
            {
                case GameState.MainMenu:
                    {
                        Program.MainMenu.SetMousePos(mouse.X, mouse.Y);
                        if (mouse.GetLeftClick())
                        {
                            Program.MainMenu.Clicked();
                        }
                        break;
                    }
            }
        }
    }
}