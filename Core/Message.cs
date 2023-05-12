using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public class Message
    {
        public string Line { get; set; }
        public RLColor Color { get; set; }
        private double _timeActive;
        private readonly double _displayTime;

        public Message(string message)
        {
            Line = message;
            Color = RLColor.White;
            _timeActive = 0;
            _displayTime = 1000;
        }

        public Message(string message, RLColor color)
        {
            Line = message;
            Color = color;
            _timeActive = 0;
            _displayTime = 1000;
        }

        public Message(string message, double displayTime)
        {
            Line = message;
            Color = RLColor.White;
            _timeActive = 0;
            _displayTime = displayTime;
        }

        public void Update(double deltaTime)
        {
            _timeActive += deltaTime;
            if (_timeActive >= _displayTime)
            {
                Game.MessageLog.Remove(this);
            }
        }
    }
}