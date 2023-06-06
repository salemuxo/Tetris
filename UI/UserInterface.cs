using RLNET;
using System;
using Tetris.Core;

namespace Tetris.UI
{
    public static class UserInterface
    {
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

        // draw leaderboard
        public static void DrawLeaderboard(RLConsole console, int y)
        {
            int leaderboardHeight = Math.Min(Program.Leaderboard.HighScores.Count, 5);

            console.Print(10, y, "Top Scores", Palette.Purple);
            for (int i = 0; i < leaderboardHeight; i++)
            {
                console.Print(9, y + 2 + i,
                    Program.Leaderboard.HighScores[i].Name, Palette.Text);
                console.Print(16, y + 2 + i,
                    Program.Leaderboard.HighScores[i].Score.ToString(), Palette.Text);
            }

            DrawDoubleBorder(console, 7, y, 16, leaderboardHeight + 2, Palette.Blue);
        }
    }
}