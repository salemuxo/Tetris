using System;
using System.Diagnostics;
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
            SetUpdateTime();
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

        public static void SetUpdateTime()
        {
            UpdateTime = Math.Pow(0.8 - ((StatManager.Level - 1) * 0.007), StatManager.Level - 1) * 1000;
            Debug.WriteLine(UpdateTime);
        }
    }
}