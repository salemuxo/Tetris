using Tetris.Core;
using Tetris.Systems;

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

        private static int _gameMode;

        public Game(int boardWidth, int boardHeight, int gameMode)
        {
            // initialize systems
            Board = new Board(boardWidth, boardHeight);
            GhostManager = new GhostManager();

            switch (gameMode)
            {
                case 0:
                    {
                        StatManager = new MarathonStatManager();
                        break;
                    }
            }

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

        public void Update()
        {
            TimeManager.Update();
            TetrominoController.Update(TimeManager.DeltaTime);
            MessageLog.Update(TimeManager.DeltaTime);
        }

        public void GameOver()
        {
            IsPlaying = false;
            Program.EndGame(StatManager.Score);
        }
    }
}