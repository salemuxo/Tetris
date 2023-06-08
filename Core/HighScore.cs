namespace Tetris.Core
{
    public class HighScore
    {
        public string Name { get; set; }
        public int? Score { get; set; }
        public double? Time { get; set; }

        public HighScore(string name, int? score, double? time)
        {
            Name = name;
            Score = score;
            Time = time;
        }
    }
}