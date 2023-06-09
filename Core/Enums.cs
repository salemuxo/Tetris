namespace Tetris.Core
{
    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        SavingScore
    }

    public enum GameMode
    {
        Marathon,
        Sprint,
        Ultra
    }
}