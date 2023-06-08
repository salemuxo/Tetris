using RLNET;
using System;
using Tetris.Core;
using Tetris.UI;

namespace Tetris.Menus
{
    public class GameOverMenu : Menu
    {
        public TextBox NameBox { get; set; }
        public Button EnterButton { get; set; }

        private readonly int _score;
        private readonly double _time;

        private readonly int _gameMode;

        public GameOverMenu(int width, int height, int score, int gameMode)
        {
            Width = width;
            Height = height;
            _score = score;
            _gameMode = gameMode;

            NameBox = new TextBox(12, 6, 6, 1);
            EnterButton = new Button(13, 9, 4, "Save", Palette.Green, Palette.Yellow, 1);
            EnterButton.Click += EnterButton_Click;
        }

        public GameOverMenu(int width, int height, double time, int gameMode)
        {
            Width = width;
            Height = height;
            _time = time;
            _gameMode = gameMode;

            NameBox = new TextBox(12, 6, 6, 1);
            EnterButton = new Button(13, 9, 4, "Save", Palette.Green, Palette.Yellow, 1);
            EnterButton.Click += EnterButton_Click;
        }

        public override void Draw(RLConsole console)
        {
            switch (_gameMode)
            {
                case 0:
                    {
                        // check for new high score
                        if (_score > Program.Leaderboard.MarathonScores[0].Score)
                        {
                            console.Print(0, 8, "New high score", Palette.Green);
                        }

                        // write new score
                        string scoreLine = $"Score: {_score}";
                        console.Print(Utility.GetCenteredX(console.Width, scoreLine.Length),
                            2, scoreLine, Palette.Blue);

                        // draw leaderboard
                        UserInterface.DrawMarathonLeaderboard(console, 12);
                        break;
                    }
                case 1:
                    {
                        // check for new high score
                        //if (_time < Program.Leaderboard.SprintScores[0].Time)
                        //{
                        //    console.Print(0, 8, "New high score", Palette.Green);
                        //}

                        // write new time
                        string timeLine = $"Time: {Utility.TimeToString(_time)}";
                        console.Print(Utility.GetCenteredX(console.Width, timeLine.Length),
                            2, timeLine, Palette.Blue);

                        // draw leaderboard
                        UserInterface.DrawSprintLeaderboard(console, 12);
                        break;
                    }
            }

            console.Print(3, 4, "Please enter your name:", Palette.Text);
            NameBox.Draw(console);
            EnterButton.Draw(console);
        }

        // check for clicks on buttons
        public override void Clicked()
        {
            EnterButton.CheckClick(mouseX, mouseY);
        }

        // check for hover on buttons
        protected override void HandleHover()
        {
            EnterButton.HandleHover(mouseX, mouseY);
        }

        // save score
        public void SaveScore()
        {
            switch (_gameMode)
            {
                case 0:
                    {
                        Program.SaveMarathonScore(NameBox.Text, _score);
                        break;
                    }
                case 1:
                    {
                        Program.SaveSprintScore(NameBox.Text, _time);
                        break;
                    }
            }
        }

        // save score when button is clicked
        private void EnterButton_Click(object sender, EventArgs e)
        {
            SaveScore();
        }
    }
}