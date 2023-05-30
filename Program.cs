using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Tetris.Core;
using Tetris.Systems;

namespace Tetris
{
    public static class Program
    {
        private const string _fontFile = "terminal8x8.png";
        private const string _consoleTitle = "Tetris";

        // consoles
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

        private const int _statWidth = 9;
        private const int _statHeight = 11;
        private static RLConsole _statConsole;

        private const int _queueWidth = 4;
        private const int _queueHeight = 11;
        private static RLConsole _queueConsole;

        private const int _logWidth = 20;
        private const int _logHeight = 4;
        private static RLConsole _logConsole;

        private static RLConsole _menuConsole;

        private static int _lastScore;

        public static GameState GameState = GameState.MainMenu;
        public static Game Game { get; private set; }
        public static List<HighScore> HighScores { get; private set; }
        public static TextBox NameBox { get; private set; }
        public static MainMenu MainMenu { get; private set; }

        public static void Main(string[] args)
        {
            // try to load high scores, otherwise create blank list
            try
            {
                // load json data
                string jsonString = File.ReadAllText(@"..\..\Data\HighScores.json");
                HighScores = JsonSerializer.Deserialize<List<HighScore>>(jsonString);

                // sort list
                var sortedList = HighScores.OrderByDescending(x => x.Score).ToList();
                HighScores = sortedList;
            }
            catch
            {
                HighScores = new List<HighScore>();
                Debug.WriteLine("No high scores to load");
            }

            // create root console
            _rootConsole = new RLRootConsole(_fontFile, _screenWidth, _screenHeight,
                8, 8, 3.5f, _consoleTitle);

            // create subconsoles
            _boardConsole = new RLConsole(_boardWidth, _boardHeight);
            _borderConsole = new RLConsole(_boardWidth + 2, _boardHeight + 2);
            _holdConsole = new RLConsole(_holdWidth, _holdHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _queueConsole = new RLConsole(_queueWidth, _queueHeight);
            _logConsole = new RLConsole(_logWidth, _logHeight);
            _menuConsole = new RLConsole(_screenWidth, _screenHeight);

            MainMenu = new MainMenu(_screenWidth, _screenHeight);

            // set up event handlers
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;

            _rootConsole.Run();
        }

        // RLNET Update event handler
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            InputHandler.HandleInput(_rootConsole);
            switch (GameState)
            {
                case GameState.MainMenu:
                    {
                        break;
                    }
                case GameState.Playing:
                    {
                        Game.Update();
                        break;
                    }
                case GameState.SavingScore:
                    {
                        break;
                    }
            }
        }

        // RLNET Render event handler
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // draw to subconsoles
            switch (GameState)
            {
                case GameState.MainMenu:
                    {
                        MainMenu.Draw(_menuConsole);
                        break;
                    }
                case GameState.Playing:
                    {
                        Game.Board.Draw(_boardConsole, _borderConsole);
                        Game.TetrominoController.DrawQueue(_queueConsole);
                        Game.StatManager.Draw(_statConsole);
                        Game.HoldManager.Draw(_holdConsole);
                        Game.MessageLog.Draw(_logConsole);
                        break;
                    }
                case GameState.SavingScore:
                    {
                        _rootConsole.Clear();
                        _rootConsole.Print(3, 1, "Please enter your name:", Palette.Text);
                        NameBox.Draw(_rootConsole);
                        //_rootConsole.Print(3, 3, NameBox.Text, Palette.Text);
                        break;
                    }
            }

            // blit subconsoles to root console
            if (GameState == GameState.Playing)
            {
                RLConsole.Blit(_borderConsole, 0, 0,
                    _boardWidth + 2, _boardHeight + 2,
                    _rootConsole, 9, 2);
                RLConsole.Blit(_boardConsole, 0, 0, _boardWidth, _boardHeight,
                    _rootConsole, 10, 3);
                RLConsole.Blit(_queueConsole, 0, 0, _queueWidth, _queueHeight,
                    _rootConsole, 22, 2);
                RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
                    _rootConsole, 0, 8);
                RLConsole.Blit(_holdConsole, 0, 0, _holdWidth, _holdHeight,
                    _rootConsole, 2, 2);
                RLConsole.Blit(_logConsole, 0, 0, _logWidth, _logHeight,
                    _rootConsole, 5, 25);
            }
            else if (GameState == GameState.MainMenu)
            {
                RLConsole.Blit(_menuConsole, 0, 0, _screenWidth, _screenHeight,
                    _rootConsole, 0, 0);
            }
            _rootConsole.Draw();
        }

        public static void Close()
        {
            string jsonString = JsonSerializer.Serialize(HighScores);
            File.WriteAllText(@"..\..\Data\HighScores.json", jsonString);
            _rootConsole.Close();
        }

        public static void StartGame()
        {
            _rootConsole.Clear();
            Game = new Game(_boardWidth, _boardHeight);
            GameState = GameState.Playing;
            Game.Start();
        }

        public static void EndGame(int score)
        {
            ClearAllConsoles();
            NameBox = new TextBox(12, 3, 6, 1);
            _lastScore = score;
            GameState = GameState.SavingScore;
        }

        public static void SaveScore()
        {
            HighScores.Add(new HighScore(NameBox.Text, _lastScore));
            GameState = GameState.MainMenu;
        }

        private static void ClearAllConsoles()
        {
            _borderConsole.Clear();
            _boardConsole.Clear();
            _queueConsole.Clear();
            _statConsole.Clear();
            _holdConsole.Clear();
            _logConsole.Clear();
            _rootConsole.Clear();
        }
    }
}