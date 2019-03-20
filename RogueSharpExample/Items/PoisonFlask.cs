using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class PoisonFlask : Item
    {
        private readonly int _rarity;
        private readonly Fireball fireball;

        public PoisonFlask(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Low Quality Poison Flask";
                Value = 30;
                fireball = new Fireball(2, 70, 3, 0, 0, 80, 5, 3, "Poison Gas");
            }
            else if (_rarity == 2)
            {
                Name = "Poison Flask";
                Value = 50;
                fireball = new Fireball(3, 75, 3, 0, 0, 90, 6, 3, "Poison Gas");
            }
            else
            {
                Name = "High Quality Poison Flask";
                Value = 100;
                fireball = new Fireball(5, 85, 3, 0, 0, 95, 6, 4, "Poison Gas");
            }

            Color = RLColor.LightGreen;
            RemainingUses = 1;
        }

        protected override bool UseItem()
        {
            Game.MessageLog.Add($"You prepare to throw the {Name}");
            RemainingUses--;

            return fireball.Perform();
        }
    }
}
