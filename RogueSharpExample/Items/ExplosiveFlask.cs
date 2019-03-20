using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class ExplosiveFlask : Item
    {
        private readonly int _rarity;
        private readonly Fireball fireball;

        public ExplosiveFlask(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Low Quality Explosive Flask";
                Value = 30;
                fireball = new Fireball(5, 80, 4, 0, 0, 0, 0, 0, "Explosive");
            }
            else if (_rarity == 2)
            {
                Name = "Explosive Flask";
                Value = 50;
                fireball = new Fireball(8, 85, 4, 0, 0, 0, 0, 0, "Explosive");
            }
            else
            {
                Name = "High Quality Explosive Flask";
                Value = 100;
                fireball = new Fireball(12, 95, 4, 0, 0, 0, 0, 0, "Explosive");
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
