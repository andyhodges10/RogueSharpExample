using System;
using RLNET;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Core;

namespace RogueSharpExample.Screens
{
    public class DialogScreen : Screen
    {
        public string Dialog;

        public DialogScreen(int width, int height) : base("", width, height)
        {

        }

        public void Draw(RLConsole rootConsole)
        {
            console.Clear();

            console.Print(3, 2, Dialog, RLColor.White);

            Draw((rootConsole.Width / 2) - (console.Width / 2), (rootConsole.Height / 2) - (console.Height / 2), rootConsole);
        }
    }
}
