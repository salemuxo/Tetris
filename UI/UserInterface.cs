using RLNET;
using System;
using Tetris.Core;

namespace Tetris.UI
{
    public static class UserInterface
    {
        // draw double line border
        public static void DrawDoubleBorder(RLConsole console, int x, int y,
            int width, int height, RLColor color)
        {
            // top and bottom
            for (int n = 0; n < width; n++)
            {
                console.Set(x + n, y - 1, color, null, 205);
                console.Set(x + n, y + height, color, null, 205);
            }
            // left and right
            for (int n = 0; n < height; n++)
            {
                console.Set(x - 1, y + n, color, null, 186);
                console.Set(x + width, y + n, color, null, 186);
            }

            // corners
            console.Set(x - 1, y - 1, color, null, 201);
            console.Set(x - 1, y + height, color, null, 200);
            console.Set(x + width, y - 1, color, null, 187);
            console.Set(x + width, y + height, color, null, 188);
        }

        // draw single line border
        public static void DrawSingleBorder(RLConsole console, int x, int y,
            int width, int height, RLColor color)
        {
            // top and bottom
            for (int n = 0; n < width; n++)
            {
                console.Set(x + n, y - 1, color, null, 196);
                console.Set(x + n, y + height, color, null, 196);
            }
            // left and right
            for (int n = 0; n < height; n++)
            {
                console.Set(x - 1, y + n, color, null, 179);
                console.Set(x + width, y + n, color, null, 179);
            }

            // corners
            console.Set(x - 1, y - 1, color, null, 218);
            console.Set(x - 1, y + height, color, null, 192);
            console.Set(x + width, y - 1, color, null, 191);
            console.Set(x + width, y + height, color, null, 217);
        }

        // draw marathon leaderboard to console with top left corner at y
        public static void DrawMarathonLeaderboard(RLConsole console, int y)
        {
            int leaderboardHeight = Math.Min(Program.Leaderboard.MarathonScores.Count, 5);

            console.Print(7, y, "Marathon Scores", Palette.Green);
            for (int i = 0; i < leaderboardHeight; i++)
            {
                console.Print(9, y + 2 + i,
                    Program.Leaderboard.MarathonScores[i].Name, Palette.Text);
                console.Print(16, y + 2 + i,
                    Program.Leaderboard.MarathonScores[i].Score.ToString(), Palette.Text);
            }

            DrawDoubleBorder(console, 7, y, 16, leaderboardHeight + 2, Palette.Blue);
        }

        // draw sprint leaderboard to console with top left corner at y
        public static void DrawSprintLeaderboard(RLConsole console, int y)
        {
            int leaderboardHeight = Math.Min(Program.Leaderboard.SprintScores.Count, 5);

            console.Print(9, y, "Sprint Times", Palette.Cyan);
            for (int i = 0; i < leaderboardHeight; i++)
            {
                console.Print(9, y + 2 + i,
                    Program.Leaderboard.SprintScores[i].Name, Palette.Text);
                console.Print(16, y + 2 + i,
                    Utility.TimeToString((double)Program.Leaderboard.SprintScores[i].Time), Palette.Text);
            }

            DrawDoubleBorder(console, 7, y, 16, leaderboardHeight + 2, Palette.Blue);
        }

        // draw ultra leaderboard to console with top left corner at y
        public static void DrawUltraLeaderboard(RLConsole console, int y)
        {
            int leaderboardHeight = Math.Min(Program.Leaderboard.UltraScores.Count, 5);

            console.Print(9, y, "Ultra Scores", Palette.Orange);
            for (int i = 0; i < leaderboardHeight; i++)
            {
                console.Print(9, y + 2 + i,
                    Program.Leaderboard.UltraScores[i].Name, Palette.Text);
                console.Print(16, y + 2 + i,
                    Program.Leaderboard.UltraScores[i].Score.ToString(), Palette.Text);
            }

            DrawDoubleBorder(console, 7, y, 16, leaderboardHeight + 2, Palette.Blue);
        }

        // draw rainbow logo
        public static void DrawLogo(RLConsole console, int x, int y)
        {
            console.Set(x, y, Palette.Red, null, 'T');
            console.Set(x + 1, y, Palette.Orange, null, 'E');
            console.Set(x + 2, y, Palette.Yellow, null, 'T');
            console.Set(x + 3, y, Palette.Green, null, 'R');
            console.Set(x + 4, y, Palette.Blue, null, 'I');
            console.Set(x + 5, y, Palette.Purple, null, 'S');
        }
    }
}