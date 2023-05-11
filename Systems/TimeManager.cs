using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tetris.Systems
{
    public class TimeManager
    {
        public double DeltaTime { get; private set; }
        public double UpdateTime { get; set; }

        private DateTime _previousGameTime;

        public TimeManager()
        {
            SetUpdateTime();
            _previousGameTime = DateTime.UtcNow;
        }

        public void Update()
        {
            if (Game.IsPlaying)
            {
                TimeSpan deltaTime = DateTime.UtcNow - _previousGameTime;
                _previousGameTime += deltaTime;

                DeltaTime = deltaTime.TotalMilliseconds;
            }
        }

        public void SetUpdateTime()
        {
            UpdateTime = Math.Pow(0.8 - ((Game.StatManager.Level - 1) * 0.007), Game.StatManager.Level - 1) * 1000;
            Debug.WriteLine(UpdateTime);
        }
    }
}