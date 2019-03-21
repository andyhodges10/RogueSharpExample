using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class BookOfReveal : Item
    {
        public BookOfReveal()
        {
            Name = "Book Of Revealing";
            Value = 50;

            Color = RLColor.Yellow;
            RemainingUses = 1;
            Symbol = '+';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;
            Game.MessageLog.Add($"You read the {Name} and learn a new ability", Colors.Healing);
            LearnReveal LearnReveal = new LearnReveal(20, 10, "Reveal map");

            return LearnReveal.Perform();
        }
    }
}
