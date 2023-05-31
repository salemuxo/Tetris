using RLNET;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris.UI;

namespace Tetris.Core
{
    public class MainMenu
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Button StartButton { get; set; }
        public Button EndButton { get; set; }

        public MainMenu(int width, int height)
        {
            Width = width;
            Height = height;

            StartButton = new Button(12, 6, 6, "START!", Palette.Green, Palette.Blue);
            StartButton.Click += StartButton_Click;

            EndButton = new Button(28, 1, 4, "X", Palette.Red, false);
            EndButton.Click += EndButton_Click;
        }

        public void Draw(RLConsole console)
        {
            DrawLogo(console, 12, 2);
            console.Print(11, 3, "by Salem", Palette.Text);

            StartButton.Draw(console);
            EndButton.Draw(console);

            // draw leaderboard
            console.Print(10, 10, "Top Scores", Palette.Purple);
            for (int i = 0; i < Program.HighScores.Count; i++)
            {
                string highScoreString = Program.HighScores[i].ToString();
                console.Print(Utility.GetCenteredX(Width, highScoreString.Length),
                    12 + i, highScoreString, Palette.Text);
            }
        }

        public void Clicked(int x, int y)
        {
            //Debug.WriteLine($"{x}, {y}");
            StartButton.CheckClick(x, y);
            EndButton.CheckClick(x, y);
        }

        private void DrawLogo(RLConsole console, int x, int y)
        {
            console.Set(x, y, Palette.Red, null, 'T');
            console.Set(x + 1, y, Palette.Orange, null, 'E');
            console.Set(x + 2, y, Palette.Yellow, null, 'T');
            console.Set(x + 3, y, Palette.Green, null, 'R');
            console.Set(x + 4, y, Palette.Blue, null, 'I');
            console.Set(x + 5, y, Palette.Purple, null, 'S');
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            Program.StartGame();
        }

        private void EndButton_Click(object sender, System.EventArgs e)
        {
            Program.Close();
        }
    }
}