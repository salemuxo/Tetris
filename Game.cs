using RLNET;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Tetris.Core;
using Tetris.Systems;

namespace Tetris
{
    public class Game
    {
        // systems
        public static Board Board { get; private set; }
        public static GhostManager GhostManager { get; private set; }
        public static TetrominoController TetrominoController { get; private set; }
        public static HoldManager HoldManager { get; private set; }
        public static MessageLog MessageLog { get; private set; }
        public static StatManager StatManager { get; private set; }
        public static TimeManager TimeManager { get; private set; }
        public static bool IsPlaying { get; set; }

        public Game(int boardWidth, int boardHeight)
        {
            // initialize systems
            Board = new Board(boardWidth, boardHeight);
            GhostManager = new GhostManager();
            StatManager = new StatManager();
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