using System;
using System.Threading.Tasks;

namespace Tetris.Systems
{
    public static class TimeManager
    {
        public static double DeltaTime { get; private set; }
        public static double UpdateTime { get; set; }

        private static DateTime _previousGameTime;

        public static void Initialize()
        {
            UpdateTime = 400;
            _previousGameTime = DateTime.UtcNow;
        }

        public static void Update()
        {
            if (Game.IsPlaying)
            {
                TimeSpan deltaTime = DateTime.UtcNow - _previousGameTime;
                _previousGameTime += deltaTime;

                DeltaTime = deltaTime.TotalMilliseconds;
            }
        }
    }
}