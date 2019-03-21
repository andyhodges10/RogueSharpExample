using System;
using RLNET;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Core;

namespace RogueSharpExample.Screens
{
    public class DialogScreen : Screen
    {
        public string Dialog;
        public string[] DialogList;

        public DialogScreen(int width, int height) : base("", width, height)
        {
        }

        public void Draw(RLConsole rootConsole)
        {
            console.Clear();
            DialogList = Dialog.Split('\n');
            console.Print(3, 2, DialogList[0], RLColor.White);
            if (DialogList.Length > 1)
            {
                console.Print(3, 3, DialogList[1], RLColor.White);
            }
            if (DialogList.Length > 2)
            {
                console.Print(3, 4, DialogList[2], RLColor.White);
            }
            if (DialogList.Length > 3)
            {
                console.Print(3, 5, DialogList[3], RLColor.White);
            }
            if (DialogList.Length > 4)
            {
                console.Print(3, 6, DialogList[4], RLColor.White);
            }

            Draw((rootConsole.Width / 2) - (console.Width / 2), (rootConsole.Height / 2) - (console.Height / 2), rootConsole);
        }
    }
}
