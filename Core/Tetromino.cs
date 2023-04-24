namespace Tetris.Core
{
    public class Tetromino
    {
        public bool[,] Body { get; private set; }
        public int Width => Body.GetLength(0);
        public int Height => Body.GetLength(1);

        public Tetromino(bool[,] body)
        {
            Body = body;
        }
    }
}