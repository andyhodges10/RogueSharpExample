using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Behaviors
{
    public class MonsterPoison : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            bool didPoison = false;

            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;
            MessageLog messageLog = Game.MessageLog;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            monsterFov.ComputeFov(monster.X, monster.Y, 2, true);

            if (monsterFov.IsInFov(player.X, player.Y) && didPoison == false)
            {
                messageLog.Add($"The {monster.Name} spat poison at you", Swatch.DbBlood);

                if (Dice.Roll("1D100") <= monster.PoisonChance)
                {
                    if (player.IsPoisonedImmune == false)
                    {
                        messageLog.Add("You were hit! The poison starts to enter your system", Swatch.DbBlood);
                        player.State = new AbnormalState(monster.PoisonLength, "Poisoned", "The Poison has stated to take its full effect", -2, -2, -3, 2, monster.PoisonDamage);
                    }
                    else if (player.Status == "Hardened")
                    {
                        messageLog.Add("You were hit! However, the poison has no effect on you in your hardened state");
                    }
                    else
                    {
                        messageLog.Add("You were hit! However, you are completely immune to the poison");
                    }
                }
                else
                {
                    messageLog.Add("You swifty dodge the poison");
                }

                didPoison = true;
            }

            return didPoison;
        }
    }
}
