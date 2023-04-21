using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public class Tetromino
    {
        public List<Tile> Tiles { get; set; }

        public Tetromino()
        {
            int shape = Game.Random.Next(7);
            Tiles = new List<Tile>();
        }

        public void MoveDown()
        {
            foreach (var tile in Tiles)
            {
            }
        }
    }
}