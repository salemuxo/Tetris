namespace Tetris.Core
{
    public class HighScore
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public HighScore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override string ToString()
        {
            return Name + ": " + Score;
        }
    }
}