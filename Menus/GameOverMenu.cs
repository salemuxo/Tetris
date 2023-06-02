using RLNET;
using System;
using Tetris.Core;

namespace Tetris.Menus
{
    public class GameOverMenu : Menu
    {
        public TextBox NameBox { get; set; }

        private int _score;

        public GameOverMenu(int width, int height, int score)
        {
            Width = width;
            Height = height;
            _score = score;
            NameBox = new TextBox(12, 6, 6, 1);
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
        }

        public override void Clicked()
        {
            throw new NotImplementedException();
        }

        protected override void HandleHover()
        {
            throw new NotImplementedException();
        }

        public void SaveScore()
        {
            Program.SaveScore(NameBox.Text, _score);
        }
    }
}