using RLNET;
using System.Collections.Generic;
using Tetris.Core;

namespace Tetris.Systems
{
    public class MessageLog
    {
        private readonly List<Message> _messages;
        private readonly List<Message> _toRemove;

        public MessageLog()
        {
            _messages = new List<Message>();
            _toRemove = new List<Message>();
        }

        public void Update(double deltaTime)
        {
            foreach (var message in _messages)
            {
                message.Update(deltaTime);
            }
            foreach (var message in _toRemove)
            {
                _messages.Remove(message);
            }
        }

        public void Draw(RLConsole console)
        {
            console.Clear();
            for (int i = 0; i < _messages.Count; i++)
            {
                console.Print(0, i, _messages[i].Line, _messages[i].Color);
            }
        }

        public void Add(string message)
        {
            _messages.Add(new Message(message));
        }

        public void Add(string message, RLColor color)
        {
            _messages.Add(new Message(message, color));
        }

        public void Add(string message, double displayTime)
        {
            _messages.Add(new Message(message, displayTime));
        }

        public void Remove(Message message)
        {
            _toRemove.Add(message);
        }
    }
}