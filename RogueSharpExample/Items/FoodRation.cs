using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class FoodRation : Item
    {
        private readonly int _amountToRegain;

        public FoodRation(int rarity)
        {
            if (rarity == 1)
            {
                Name = "Low Quality Food Ration";
                Value = 10;
                _amountToRegain = 250;
            }
            else if (rarity == 2)
            {
                Name = "Food Ration";
                Value = 15;
                _amountToRegain = 300;
            }
            else
            {
                Name = "High Quality Food Ration";
                Value = 30;
                _amountToRegain = 400;
            }
            Color = Swatch.DbBrightWood;
            RemainingUses = 1;
            Symbol = '%';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;
            
            if (player.Hunger + _amountToRegain > player.MaxHunger)
            {
                Game.MessageLog.Add("You ultimately decide to not eat the food ration as you are not hungry enough just yet");

                return true;
            }
            else
            {
                Game.MessageLog.Add($"You consume a {Name}, your hunger level is now {player.Hunger + _amountToRegain}", Colors.Healing); // debug
                //Game.MessageLog.Add($"You consume a {Name}", Colors.Healing);
                RemainingUses--;
                RegainHunger regainHunger = new RegainHunger(_amountToRegain, 0);

                return regainHunger.Perform();
            }
        }
    }
}
