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

        private const int _screenWidth = 30;
        private const int _screenHeight = 30;
        private static RLRootConsole _rootConsole;

        private const int _boardWidth = 10;
        private const int _boardHeight = 20;
        private static RLConsole _boardConsole;
        private static RLConsole _borderConsole;

        private const int _holdWidth = 4;
        private const int _holdHeight = 5;
        private static RLConsole _holdConsole;

        private const int _statWidth = 5;
        private const int _statHeight = 9;
        private static RLConsole _statConsole;

        private const int _queueWidth = 4;
        private const int _queueHeight = 4;
        private static RLConsole _queueConsole;

        public static Board Board { get; private set; }
        public static Random Random { get; private set; }
        public static bool IsPlaying { get; set; }

        public static void Main(string[] args)
        {
            Random = new Random();
            Board = new Board(_boardWidth, _boardHeight);
            IsPlaying = true;

            // initialize systems
            TimeManager.Initialize();
            TetrominoController.Initialize();
            StatManager.Initialize(_statWidth, _statHeight);

            // create root console
            _rootConsole = new RLRootConsole(_fontFile, _screenWidth, _screenHeight,
                8, 8, 3.5f, _consoleTitle);

            // create subconsoles
            _boardConsole = new RLConsole(_boardWidth, _boardHeight);
            _borderConsole = new RLConsole(_boardWidth + 2, _boardHeight + 2);
            _holdConsole = new RLConsole(_holdWidth, _holdHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _queueConsole = new RLConsole(_queueWidth, _queueHeight);

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
            TetrominoController.Update(TimeManager.DeltaTime);
        }

        // RLNET Render event handler
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // draw to subconsoles
            Board.Draw(_boardConsole, _borderConsole);
            TetrominoController.DrawQueue(_queueConsole);
            StatManager.Draw(_statConsole);
            HoldManager.Draw(_holdConsole);

            // blit subconsoles to root console
            RLConsole.Blit(_borderConsole, 0, 0,
                _boardWidth + 2, _boardHeight + 2,
                _rootConsole, 9, 4);
            RLConsole.Blit(_boardConsole, 0, 0, _boardWidth, _boardHeight,
                _rootConsole, 10, 5);
            RLConsole.Blit(_queueConsole, 0, 0, _queueWidth, _queueHeight,
                _rootConsole, 23, 4);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
                _rootConsole, 2, 10);
            RLConsole.Blit(_holdConsole, 0, 0, _holdWidth, _holdHeight,
                _rootConsole, 2, 4);
            _rootConsole.Draw();
        }
    }
}