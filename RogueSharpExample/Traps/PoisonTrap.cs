using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;
using RogueSharpExample.Abilities;

namespace RogueSharpExample.Traps
{
    public class PoisonTrap : Trap
    {
        public PoisonTrap()
        {
            Name = "PoisonTrap";
            Color = RLColor.Green;
            Symbol = '^';
        }

        protected override bool TrapTriggered()
        {
            int _damage = Dice.Roll("2D3");
            Game.MessageLog.Add($"You stepped on a {Name} and take {_damage} damage", Swatch.DbBlood);

            Game.Player.Health -= _damage;
            if (Game.Player.Health <= 0)
            {
                Game.Player.Status = "Dead";
            }
            else
            {
                Game.Player.State = new AbnormalState(6, "Poisoned", "The Poison has stated to take its full effect", -2, -2, -3, 2, 3);
            }

            return true;
        }
    }
}
