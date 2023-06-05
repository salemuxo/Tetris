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

        private int _score;

        public GameOverMenu(int width, int height, int score)
        {
            Width = width;
            Height = height;
            _score = score;
            NameBox = new TextBox(12, 6, 6, 1);
            EnterButton = new Button(13, 9, 4, "Save", Palette.Green, Palette.Yellow, 1);
            EnterButton.Click += EnterButton_Click;
        }

        public override void Draw(RLConsole console)
        {
            if (_score > Program.Leaderboard.HighScores[0].Score)
            {
                console.Print(0, 1, "New high score!", Palette.Green);
            }

            string scoreLine = $"Score: {_score}";
            console.Print(Utility.GetCenteredX(console.Width, scoreLine.Length),
                2, scoreLine, Palette.Blue);

            console.Print(3, 4, "Please enter your name:", Palette.Text);
            NameBox.Draw(console);
            EnterButton.Draw(console);

            UserInterface.DrawLeaderboard(console);
        }

        public override void Clicked()
        {
            EnterButton.CheckClick(mouseX, mouseY);
        }

        protected override void HandleHover()
        {
            EnterButton.HandleHover(mouseX, mouseY);
        }

        public void SaveScore()
        {
            Program.SaveScore(NameBox.Text, _score);
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            SaveScore();
        }
    }
}