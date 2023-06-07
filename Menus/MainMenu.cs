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

            UserInterface.DrawLeaderboard(console, 16);
        }

        // check if buttons were clicked
        public override void Clicked()
        {
            MarathonButton.CheckClick(mouseX, mouseY);
            SprintButton.CheckClick(mouseX, mouseY);
            UltraButton.CheckClick(mouseX, mouseY);
            EndButton.CheckClick(mouseX, mouseY);
        }

        // check if buttons are hovered
        protected override void HandleHover()
        {
            MarathonButton.HandleHover(mouseX, mouseY);
            SprintButton.HandleHover(mouseX, mouseY);
            UltraButton.HandleHover(mouseX, mouseY);
            EndButton.HandleHover(mouseX, mouseY);
        }

        // start game
        private void MarathonButton_Click(object sender, System.EventArgs e)
        {
            Program.StartGame(0);
        }

        private void SprintButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UltraButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // close game
        private void EndButton_Click(object sender, System.EventArgs e)
        {
            Program.Close();
        }
    }
}