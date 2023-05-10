using System.Collections.Generic;

namespace Tetris.Core
{
    public class Grid<T>
    {
        public Grid(int width, int height, int offset)
        {
            Array = new T[width, height];
            Offset = offset;
        }

        private readonly T[,] Array;
        private readonly int Offset;

        public T this[int x, int y]
        {
            get
            {
                return Array[x, y + Offset];
            }
            set
            {
                Array[x, y + Offset] = value;
            }
        }
    }
}