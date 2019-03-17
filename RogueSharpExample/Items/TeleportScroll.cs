using RLNET;
using RogueSharp;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class TeleportScroll : Item
    {
        public TeleportScroll()
        {
            Name = "Teleport Scroll";
            Color = RLColor.Gray;
            RemainingUses = 0;
            Symbol = '?';
        }

        protected override bool UseItem()
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;

            Game.MessageLog.Add($"{player.Name} uses a {Name} and reappears in another place");

            Point point = map.GetRandomLocation();

            map.SetActorPosition(player, point.X, point.Y);

            RemainingUses--;

            return true;
        }
    }
}
