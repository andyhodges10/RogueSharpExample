using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Behaviors
{
    public class MimicDisguised : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            bool didReveal = false;

            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;
            MessageLog messageLog = Game.MessageLog;

            foreach (ICell cell in dungeonMap.GetCellsInCircle(monster.X, monster.Y, 1))
            {
                if (dungeonMap.CheckForPlayer(cell.X, cell.Y) && didReveal == false)
                {
                    monster.Name = "Mimic";
                    monster.Symbol = 'M';
                    monster.IsMimicInHiding = false;
                    messageLog.Add("That's no armor, that a Mimic", Swatch.DbBlood);
                    messageLog.Add("The mimic spat poison at you", Swatch.DbBlood); // hp debug

                    if (Dice.Roll("1D10") < 7)
                    {
                        if (player.IsPoisonedImmune == false)
                        {
                            messageLog.Add("You were hit! The poison starts to enter your system", Swatch.DbBlood);
                            player.State = new AbnormalState(3, "Poisoned", -1, -1, 3);
                        }
                        else if (player.Status == "Hardened")
                        {
                            messageLog.Add("You were hit! However, the poison has no effect on you in your hardened state");
                        }
                        else
                        {
                            messageLog.Add("You were hit! However, you are completely immune to the poison", Swatch.DbBlood);
                        }
                    }
                    else
                    {
                        messageLog.Add("You swifty dodge the poison");
                    }

                    didReveal = true;
                }
            }

            return didReveal;
        }
    }
}
