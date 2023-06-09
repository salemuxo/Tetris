using System;
using System.Diagnostics;

namespace Tetris.Systems
{
    public class TimeManager
    {
        // time between current frame and last frame in ms
        public double DeltaTime { get; private set; }

        // time between auto movement
        public double UpdateTime
        {
            get
            {
                if (Game.TetrominoController.IsSoftDropping)
                {
                    return 50;
                }
                else
                {
                    return _updateTime;
                }
            }
            set
            {
                _updateTime = value;
            }
        }

        private double _updateTime;
        private DateTime _previousGameTime;

        public TimeManager()
        {
            SetUpdateTime();
            _previousGameTime = DateTime.UtcNow;
        }

        // calculate DeltaTime
        public void Update()
        {
            if (Game.IsPlaying)
            {
                TimeSpan deltaTime = DateTime.UtcNow - _previousGameTime;
                _previousGameTime += deltaTime;

                DeltaTime = deltaTime.TotalMilliseconds;
            }
        }

        // set update time based on level
        public void SetUpdateTime()
        {
            _updateTime = Math.Pow(0.8 - ((Game.StatManager.Level - 1) * 0.007), Game.StatManager.Level - 1) * 1000;
            Debug.WriteLine(_updateTime);
        }
    }
}