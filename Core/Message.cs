using RLNET;

namespace Tetris.Core
{
    public class Message
    {
        public string Line { get; set; }
        public RLColor Color { get; set; }
        private readonly double _displayTime;
        private double _timeActive;

        public Message(string message, RLColor color, double displayTime)
        {
            Line = message;
            Color = color;
            _displayTime = displayTime;
            _timeActive = 0;
        }

        public void Update(double deltaTime)
        {
            _timeActive += deltaTime;
            if (_timeActive >= _displayTime)
            {
                Game.MessageLog.Remove(this);
            }
        }

        public int GetCenteredPos(int logWidth)
        {
            return (logWidth - Line.Length) / 2;
        }
    }
}