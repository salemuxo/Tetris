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
            Board = new Board(_boardWidth, _boardHeight);
            Random = new Random();
            TimeHandler.Start();

            // create root console
            _rootConsole = new RLRootConsole(_fontFile, _screenWidth, _screenHeight,
                8, 8, 2f, _consoleTitle);

            // create subconsoles
            _boardConsole = new RLConsole(_boardWidth, _boardHeight);

            // set up event handlers
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;

            _rootConsole.Run();
        }

        // update with deltaTime
        public static void Update(TimeSpan deltaTime)
        {
            Board.Update();
        }

        // RLNET Update event handler
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            InputHandler.HandleInput(_rootConsole);
        }

        // RLNET Render event handler
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            _boardConsole.Clear();
            _boardConsole.Print(0, 0, TimeHandler.Tick.ToString(), RLColor.White);
            // blit subconsoles to root console
            RLConsole.Blit(_boardConsole, 0, 0, _boardWidth, _boardHeight,
                _rootConsole, 1, 1);
            _rootConsole.Draw();
        }
    }
}