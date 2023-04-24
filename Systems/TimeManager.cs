using System;
using System.Threading.Tasks;

namespace Tetris.Systems
{
    public static class TimeManager
    {
        public static double DeltaTime { get; private set; }
        public static double UpdateTime { get; private set; }

        private static DateTime _previousGameTime;

        public static void Initialize()
        {
            UpdateTime = 500;
            _previousGameTime = DateTime.UtcNow;
        }

        public static void Update()
        {
            TimeSpan deltaTime = DateTime.UtcNow - _previousGameTime;
            _previousGameTime += deltaTime;

            DeltaTime = deltaTime.TotalMilliseconds;
        }
    }
}