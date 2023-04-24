using Tetris.Core;

namespace Tetris.Systems
{
    public static class TetrominoController
    {
        public static Tetromino FallingTetromino { get; set; }

        // coords of top left corner
        public static int X { get; set; }
        public static int Y { get; set; }

        public static void Initialize()
        {
            X = 0;
            Y = 0;
            FallingTetromino = Tetrominos.GetRandomTetromino();
        }

        public static void Update()
        {
            for (int x = 0; x < FallingTetromino.Width; x++)
            {
                for (int y = 0; y < FallingTetromino.Height; y++)
                {
                    Game.Board.Cells[X + x, Y + y].IsTile = FallingTetromino.Body[x, y];
                }
            }
        }

        public static void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    {
                        Y--;
                        break;
                    }
                case Direction.Left:
                    {
                        X--;
                        break;
                    }
                case Direction.Down:
                    {
                        Y++;
                        break;
                    }
                case Direction.Right:
                    {
                        X++;
                        break;
                    }
            }
        }
    }
}