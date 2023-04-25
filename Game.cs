using RLNET;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Tetris.Core;
using Tetris.Systems;

namespace Tetris
{
    public static class Game
    {
        private const string _fontFile = "terminal8x8.png";
        private const string _consoleTitle = "Tetris";

        private const int _screenWidth = 12;
        private const int _screenHeight = 22;
        private static RLRootConsole _rootConsole;

        private const int _boardWidth = 10;
        private const int _boardHeight = 20;
        private static RLConsole _boardConsole;

        public static Board Board { get; private set; }
        public static Random Random { get; private set; }

        public static void Main(string[] args)
        {
            Random = new Random();
            Board = new Board(_boardWidth, _boardHeight);

            TimeManager.Initialize();
            TetrominoController.Initialize();

            // create root console
            _rootConsole = new RLRootConsole(_fontFile, _screenWidth, _screenHeight,
                8, 8, 4f, _consoleTitle);

            // create subconsoles
            _boardConsole = new RLConsole(_boardWidth, _boardHeight);

            // set up event handlers
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;

            _rootConsole.Run();
        }

        // RLNET Update event handler
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            TimeManager.Update();
            InputHandler.HandleInput(_rootConsole);
            Board.Update(TimeManager.DeltaTime);
        }

        // RLNET Render event handler
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            Board.Draw(_boardConsole);
            // blit subconsoles to root console
            RLConsole.Blit(_boardConsole, 0, 0, _boardWidth, _boardHeight,
                _rootConsole, 1, 1);
            _rootConsole.Draw();
        }
    }
}