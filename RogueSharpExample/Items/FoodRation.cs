using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class FoodRation : Item
    {
        public FoodRation()
        {
            Name = "Food Ration";
            Color = Swatch.DbBrightWood;
            RemainingUses = 2;
            Symbol = '%';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;
            
            if (player.Hunger + 200 > player.MaxHunger)
            {
                Game.MessageLog.Add("You ultimately decide to not eat the food ration as you are not hungry enough just yet");

                return true;
            }
            else
            {
                Game.MessageLog.Add($"You consume a {Name}", Colors.Healing);
                RemainingUses--;
                Sustain sustain = new Sustain(200, 0, 0, "Sustain");

                return sustain.Perform();
            }
        }
    }
}
