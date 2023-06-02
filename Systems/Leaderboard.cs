using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using Tetris.Core;

namespace Tetris.Systems
{
    public class Leaderboard
    {
        public List<HighScore> HighScores { get; private set; }

        public Leaderboard()
        {
            LoadScores();
        }

        // try to load high scores, otherwise create blank list
        private void LoadScores()
        {
            try
            {
                // load json data
                string jsonString = File.ReadAllText(@"..\..\Data\HighScores.json");
                HighScores = JsonSerializer.Deserialize<List<HighScore>>(jsonString);
            }
            catch
            {
                HighScores = new List<HighScore>();
                Debug.WriteLine("No high scores to load");
            }
        }

        public void SaveScores()
        {
            SortScores();
            string jsonString = JsonSerializer.Serialize(HighScores);
            File.WriteAllText(@"..\..\Data\HighScores.json", jsonString);
        }

        private void SortScores()
        {
            var sortedList = HighScores.OrderByDescending(x => x.Score).ToList();
            HighScores = sortedList;
        }
    }
}