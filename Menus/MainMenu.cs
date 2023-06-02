using RLNET;
using System;
using Tetris.UI;
using Tetris.Core;

namespace Tetris.Menus
{
    public class MainMenu : Menu
    {
        public Button StartButton { get; set; }
        public Button EndButton { get; set; }

        public MainMenu(int width, int height)
        {
            Width = width;
            Height = height;

            StartButton = new Button(12, 6, 6, "START!", Palette.Green, Palette.Blue, 1);
            StartButton.Click += StartButton_Click;

            EndButton = new Button(28, 1, 4, "X", Palette.Red, false);
            EndButton.Click += EndButton_Click;
        }

        public override void Draw(RLConsole console)
        {
            // draw title
            DrawLogo(console, 12, 2);
            console.Print(11, 3, "by Salem", Palette.Text);

            // draw buttons
            StartButton.Draw(console);
            EndButton.Draw(console);

            // draw leaderboard
            int leaderboardHeight = Math.Min(Program.Leaderboard.HighScores.Count, 5);

            console.Print(10, 12, "Top Scores", Palette.Purple);
            for (int i = 0; i < leaderboardHeight; i++)
            {
                string highScoreString = Program.Leaderboard.HighScores[i].ToString();
                console.Print(Utility.GetCenteredX(Width, highScoreString.Length),
                    14 + i, highScoreString, Palette.Text);
            }

            UserInterface.DrawDoubleBorder(console, 7, 12, 16, leaderboardHeight + 2, Palette.Blue);
        }

        // check if buttons were clicked
        public override void Clicked()
        {
            StartButton.CheckClick(mouseX, mouseY);
            EndButton.CheckClick(mouseX, mouseY);
        }

        // check if buttons are hovered
        protected override void HandleHover()
        {
            StartButton.HandleHover(mouseX, mouseY);
            EndButton.HandleHover(mouseX, mouseY);
        }

        // draw rainbow logo
        private void DrawLogo(RLConsole console, int x, int y)
        {
            console.Set(x, y, Palette.Red, null, 'T');
            console.Set(x + 1, y, Palette.Orange, null, 'E');
            console.Set(x + 2, y, Palette.Yellow, null, 'T');
            console.Set(x + 3, y, Palette.Green, null, 'R');
            console.Set(x + 4, y, Palette.Blue, null, 'I');
            console.Set(x + 5, y, Palette.Purple, null, 'S');
        }

        // start game
        private void StartButton_Click(object sender, System.EventArgs e)
        {
            Program.StartGame();
        }

        // close game
        private void EndButton_Click(object sender, System.EventArgs e)
        {
            Program.Close();
        }
    }
}