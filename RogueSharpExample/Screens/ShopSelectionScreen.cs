using System;
using RLNET;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Core;

namespace RogueSharpExample.Screens
{
    public class ShopSelectionScreen : Screen
    {
        public ShopSelectionScreen(int width, int height) : base("", width, height)
        {

        }

        public void Draw(RLConsole rootConsole)
        {
            console.Clear();

            console.Print(3, 2, "Do you want to buy or sell?", RLColor.White);
            console.Print(3, 4, "B- Buy", RLColor.White);
            console.Print(3, 5, "S- Sell", RLColor.White);

            Draw((rootConsole.Width / 2) - (console.Width / 2), (rootConsole.Height / 2) - (console.Height / 2), rootConsole);
        }
    }
}
