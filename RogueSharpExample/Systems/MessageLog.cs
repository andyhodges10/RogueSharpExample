using System.Collections.Generic;
using RLNET;

namespace RogueSharpExample.Systems
{
    public class MessageLog
    {
        private static readonly int _maxLines = 9;

        private readonly Queue<string> _lines;
        private Queue<RLColor> _color;

        public MessageLog()
        {
            _lines = new Queue<string>();
            _color = new Queue<RLColor>();
        }

        public void Add(string message)
        {
            RLColor color = RLColor.White;
            _lines.Enqueue(message);
            _color.Enqueue(color);

            // When exceeding the maximum number of lines remove the oldest one.
            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
                _color.Dequeue();
            }
        }

        public void Add(string message, RLColor color)
        {
            _lines.Enqueue(message);
            _color.Enqueue(color);

            // When exceeding the maximum number of lines remove the oldest one.
            if (_lines.Count > _maxLines && _color.Count > _maxLines)
            {
                _lines.Dequeue();
                _color.Dequeue();
            }
        }

        // Draw each line of the MessageLog queue to the console
        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] lines = _lines.ToArray();
            RLColor[] color = _color.ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 1, lines[i], color[i]);
            }
        }
    }
}
