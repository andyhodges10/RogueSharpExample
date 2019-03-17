using System;
using RLNET;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Core;

namespace RogueSharpExample.Screens
{
    public class ShopScreen : Screen
    {
        readonly int _itemsPerPage = 26;
        int _currentPage = 0;
        double _totalPages = 1;

        public ShopScreen(int width, int height) : base("Shop", width, height)
        {
        }

        public void Draw(RLConsole rootConsole, Inventory inventory)
        {
            console.Clear();
            /*
            ITreasure[] items = inventory.Item.ToArray();
            _totalPages = Math.Ceiling(items.Length / (double)_itemsPerPage);

            string pageString = $"{_currentPage + 1}/{_totalPages}";

            if (inventory.Item.Count > 0)
            {
                for (int i = 0; i < items.Length % 26; i++)
                {
                    Item item = items[i + (_itemsPerPage * _currentPage)] as Item;
                    console.Print(2, 2 + i, $"{(char)(97 + i)} - {item.ToString()}", RLColor.White);
                }
                console.Print((console.Width / 2) - (pageString.Length / 2), console.Height - 2, pageString, RLColor.White);

                if (_currentPage > 0)
                {
                    console.Print(console.Width - 1, 1, "^", RLColor.White);
                }
                if (_currentPage + 1 < _totalPages)
                {
                    console.Print(console.Width - 2, console.Height - 1, "V", RLColor.White);
                }
            }
            */

            Draw((rootConsole.Width / 2) - (console.Width / 2), (rootConsole.Height / 2) - (console.Height / 2), rootConsole);
        }

        public void GoToNextPage()
        {
            if (_currentPage < _totalPages - 1)
            {
                _currentPage++;
            }
        }

        public void GoToPreviousPage()
        {
            if (_currentPage > 0)
            {
                _currentPage--;
            }
        }
    }
}
