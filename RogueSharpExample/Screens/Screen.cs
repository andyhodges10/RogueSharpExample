using RLNET;

namespace RogueSharpExample.Screens
{
    public class Screen
    {
        protected RLConsole console;
        string title;

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Screen(string title, int width, int height)
        {
            Height = height;
            Width = width;
            console = new RLConsole(width, height);
            this.title = title;
        }

        public void Draw(int x, int y, RLConsole rootConsole)
        {
            for (int _x = 0; _x < console.Width; _x++)
            {
                for (int _y = 0; _y < console.Height; _y++)
                {
                    if (_y == 0 || _y == console.Height - 1)
                    {
                        console.Print(_x, _y, $"{(char)205}", RLColor.White);
                    }
                    if (_x == 0 || _x == console.Width - 1)
                    {
                        console.Print(_x, _y, $"{(char)186}", RLColor.White);
                    }
                }

            }

            console.Print((console.Width / 2) - (title.Length / 2), 0, title, RLColor.White);

            console.Print(0, 0, $"{(char)201}", RLColor.White);
            console.Print(console.Width - 1, 0, $"{(char)187}", RLColor.White);
            console.Print(0, console.Height - 1, $"{(char)200}", RLColor.White);
            console.Print(console.Width - 1, console.Height - 1, $"{(char)188}", RLColor.White);

            RLConsole.Blit(console, 0, 0, console.Width, console.Height, rootConsole, x, y);
        }
    }
}
