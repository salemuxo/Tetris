using Tetris.Core;
using Tetris.Systems;
using Tetris.Modes;

namespace Tetris
{
    public class Game
    {
        // systems
        public static Board Board { get; protected set; }
        public static GhostManager GhostManager { get; protected set; }
        public static TetrominoController TetrominoController { get; protected set; }
        public static HoldManager HoldManager { get; protected set; }
        public static MessageLog MessageLog { get; protected set; }
        public static StatManager StatManager { get; protected set; }
        public static TimeManager TimeManager { get; protected set; }
        public static bool IsPlaying { get; set; }

        private static GameMode _gameMode;

        public Game(int boardWidth, int boardHeight, GameMode gameMode)
        {
            // use stat manager corresponding to game mode
            switch (gameMode)
            {
                case GameMode.Marathon:
                    {
                        StatManager = new MarathonStatManager();
                        break;
                    }
                case GameMode.Sprint:
                    {
                        StatManager = new SprintStatManager();
                        break;
                    }
                case GameMode.Ultra:
                    {
                        StatManager = new UltraStatManager();
                        break;
                    }
            }
            _gameMode = gameMode;

            // initialize systems
            Board = new Board(boardWidth, boardHeight);
            GhostManager = new GhostManager();
            TimeManager = new TimeManager();
            HoldManager = new HoldManager();
            MessageLog = new MessageLog();
            TetrominoController = new TetrominoController();
        }

        public void Start()
        {
            IsPlaying = true;
            TetrominoController.Start();
        }

        // update time
        public void Update()
        {
            TimeManager.Update();

            TetrominoController.Update(TimeManager.DeltaTime);
            MessageLog.Update(TimeManager.DeltaTime);

            if (_gameMode != GameMode.Marathon)
            {
                StatManager.Update(TimeManager.DeltaTime);
            }
        }

        // end game, prompt game over screen
        public void GameOver()
        {
            //Debug.WriteLine(_gameMode);
            IsPlaying = false;
            if (_gameMode == GameMode.Sprint)
            {
                Program.EndGame(GameMode.Sprint, null, StatManager.Time);
            }
            else
            {
                Program.EndGame(_gameMode, StatManager.Score, null);
            }
        }
    }
}