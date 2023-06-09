using RLNET;
using System;
using Tetris.UI;
using Tetris.Core;

namespace Tetris.Menus
{
    public class MainMenu : Menu
    {
        public Button MarathonButton { get; set; }
        public Button SprintButton { get; set; }
        public Button UltraButton { get; set; }
        public Button EndButton { get; set; }

        // leaderboard tabs
        public Button MLButton { get; set; }
        public Button SLButton { get; set; }
        public Button ULButton { get; set; }

        private int _leaderboardTab = 0;

        public MainMenu(int width, int height)
        {
            Width = width;
            Height = height;

            MarathonButton = new Button(11, 6, 8, "MARATHON", Palette.Green, Palette.Blue, 1);
            MarathonButton.Click += MarathonButton_Click;

            SprintButton = new Button(9, 9, 6, "SPRINT", Palette.Cyan, Palette.Blue, 1);
            SprintButton.Click += SprintButton_Click;

            UltraButton = new Button(17, 9, 5, "ULTRA", Palette.Orange, Palette.Red, 1);
            UltraButton.Click += UltraButton_Click;

            EndButton = new Button(28, 1, 4, "X", Palette.Red, false);
            EndButton.Click += EndButton_Click;

            MLButton = new Button(7, 14, 1, "M", Palette.Green, false)
            {
                BackgroundColor = Palette.Blue
            };
            MLButton.Click += MLButton_Click;

            SLButton = new Button(9, 14, 1, "S", Palette.Cyan, false);
            SLButton.Click += SLButton_Click;

            ULButton = new Button(11, 14, 1, "U", Palette.Orange, false);
            ULButton.Click += ULButton_Click;
        }

        public override void Draw(RLConsole console)
        {
            // draw title
            UserInterface.DrawLogo(console, 12, 2);
            console.Print(11, 3, "by Salem", Palette.Text);

            // draw buttons
            MarathonButton.Draw(console);
            SprintButton.Draw(console);
            UltraButton.Draw(console);
            EndButton.Draw(console);

            MLButton.Draw(console);
            SLButton.Draw(console);
            ULButton.Draw(console);

            switch (_leaderboardTab)
            {
                case 0:
                    {
                        UserInterface.DrawMarathonLeaderboard(console, 16);
                        break;
                    }
                case 1:
                    {
                        UserInterface.DrawSprintLeaderboard(console, 16);
                        break;
                    }
                case 2:
                    {
                        UserInterface.DrawUltraLeaderboard(console, 16);
                        break;
                    }
            }
        }

        // check if buttons were clicked
        public override void Clicked()
        {
            MarathonButton.CheckClick(mouseX, mouseY);
            SprintButton.CheckClick(mouseX, mouseY);
            UltraButton.CheckClick(mouseX, mouseY);
            EndButton.CheckClick(mouseX, mouseY);

            MLButton.CheckClick(mouseX, mouseY);
            SLButton.CheckClick(mouseX, mouseY);
            ULButton.CheckClick(mouseX, mouseY);
        }

        // check if buttons are hovered
        protected override void HandleHover()
        {
            MarathonButton.HandleHover(mouseX, mouseY);
            SprintButton.HandleHover(mouseX, mouseY);
            UltraButton.HandleHover(mouseX, mouseY);
            EndButton.HandleHover(mouseX, mouseY);

            MLButton.HandleHover(mouseX, mouseY);
            SLButton.HandleHover(mouseX, mouseY);
            ULButton.HandleHover(mouseX, mouseY);
        }

        // start game
        private void MarathonButton_Click(object sender, System.EventArgs e)
        {
            Program.StartGame(GameMode.Marathon);
        }

        private void SprintButton_Click(object sender, EventArgs e)
        {
            Program.StartGame(GameMode.Sprint);
        }

        private void UltraButton_Click(object sender, EventArgs e)
        {
            Program.StartGame(GameMode.Ultra);
        }

        // close game
        private void EndButton_Click(object sender, System.EventArgs e)
        {
            Program.Close();
        }

        // switch tabs
        private void MLButton_Click(object sender, EventArgs e)
        {
            _leaderboardTab = 0;
            MLButton.BackgroundColor = Palette.Blue;
            SLButton.BackgroundColor = RLColor.Black;
            ULButton.BackgroundColor = RLColor.Black;
            Program.ClearMenu();
        }

        private void SLButton_Click(object sender, EventArgs e)
        {
            _leaderboardTab = 1;
            MLButton.BackgroundColor = RLColor.Black;
            SLButton.BackgroundColor = Palette.Blue;
            ULButton.BackgroundColor = RLColor.Black;
            Program.ClearMenu();
        }

        private void ULButton_Click(object sender, EventArgs e)
        {
            _leaderboardTab = 2;
            MLButton.BackgroundColor = RLColor.Black;
            SLButton.BackgroundColor = RLColor.Black;
            ULButton.BackgroundColor = Palette.Blue;
            Program.ClearMenu();
        }
    }
}