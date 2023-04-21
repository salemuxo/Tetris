using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Systems
{
    public static class TimeHandler
    {
        public static int Tick { get; private set; }

        public static async void Start()
        {
            Tick = 0;

            DateTime previousGameTime = DateTime.UtcNow;

            while (true)
            {
                TimeSpan gameTime = DateTime.UtcNow - previousGameTime;
                previousGameTime += gameTime;

                Game.Update(gameTime);

                if (Tick < 16)
                {
                    Tick++;
                }
                else
                {
                    Tick = 0;
                }

                await Task.Delay(70);
            }
        }
    }
}