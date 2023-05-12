﻿using RLNET;
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
                            break;
                        }
                    // move down
                    case RLKey.Down:
                        {
                            Game.TetrominoController.Move(Direction.Down);
                            break;
                        }
                    // move right
                    case RLKey.Right:
                        {
                            Game.TetrominoController.Move(Direction.Right);
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
        }

        private static void HandleMouseInput(RLRootConsole rootConsole)
        {
            RLMouse mouse = rootConsole.Mouse;
        }
    }
}