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
        public List<HighScore> MarathonScores { get; private set; }
        public List<HighScore> SprintScores { get; private set; }
        public List<HighScore> UltraScores { get; private set; }

        public Leaderboard()
        {
            LoadAllScores();
        }

        public void LoadAllScores()
        {
            LoadMarathonScores();
            LoadSprintScores();
            LoadUltraScores();
        }

        public void SaveAllScores()
        {
            SaveMarathonScores();
            SaveSprintScores();
            SaveUltraScores();
        }

        // MARATHON
        private void LoadMarathonScores()
        {
            try
            {
                // load json data
                string jsonString = File.ReadAllText(@"..\..\Data\MarathonScores.json");
                MarathonScores = JsonSerializer.Deserialize<List<HighScore>>(jsonString);
            }
            catch
            {
                MarathonScores = new List<HighScore>();
                Debug.WriteLine("No high scores to load");
            }
        }

        public void SaveMarathonScores()
        {
            MarathonScores = SortByScore(MarathonScores);
            string jsonString = JsonSerializer.Serialize(MarathonScores);
            File.WriteAllText(@"..\..\Data\MarathonScores.json", jsonString);
        }

        // SPRINT
        private void LoadSprintScores()
        {
            try
            {
                // load json data
                string jsonString = File.ReadAllText(@"..\..\Data\SprintScores.json");
                SprintScores = JsonSerializer.Deserialize<List<HighScore>>(jsonString);
            }
            catch
            {
                SprintScores = new List<HighScore>();
                Debug.WriteLine("No high scores to load");
            }
        }

        public void SaveSprintScores()
        {
            SprintScores = SortByTime(SprintScores);
            string jsonString = JsonSerializer.Serialize(SprintScores);
            File.WriteAllText(@"..\..\Data\SprintScores.json", jsonString);
        }

        // ULTRA
        private void LoadUltraScores()
        {
            try
            {
                // load json data
                string jsonString = File.ReadAllText(@"..\..\Data\UltraScores.json");
                UltraScores = JsonSerializer.Deserialize<List<HighScore>>(jsonString);
            }
            catch
            {
                UltraScores = new List<HighScore>();
                Debug.WriteLine("No high scores to load");
            }
        }

        public void SaveUltraScores()
        {
            UltraScores = SortByTime(UltraScores);
            string jsonString = JsonSerializer.Serialize(UltraScores);
            File.WriteAllText(@"..\..\Data\UltraScores.json", jsonString);
        }

        // SORT
        private List<HighScore> SortByScore(List<HighScore> listToSort)
        {
            var sortedList = listToSort.OrderByDescending(x => x.Score).ToList();
            return sortedList;
        }

        private List<HighScore> SortByTime(List<HighScore> listToSort)
        {
            var sortedList = listToSort.OrderBy(x => x.Time).ToList();
            return sortedList;
        }
    }
}