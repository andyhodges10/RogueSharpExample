using System;
using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Behaviors
{
    public class StandardMoveAndAttack : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            if (!monster.TurnsAlerted.HasValue)
            {
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
                if (monsterFov.IsInFov(player.X, player.Y))
                {
                    
                    if (monster.GreetMessages != null)
                    {
                        Random random = new Random();
                        int i = random.Next(0, monster.GreetMessages.Length);
                        if (monster.IsABoss)
                        {
                            Game.MessageLog.Add($"{monster.GreetMessages[i]}", Swatch.DbBlood);
                        }
                        else
                        {
                            Game.MessageLog.Add($"{monster.GreetMessages[i]}");
                        }
                    }
                    else
                    {
                        Game.MessageLog.Add($"{monster.Name} is eager to fight {player.Name}");
                    }

                    monster.TurnsAlerted = 1;
                }
            }

            if (monster.TurnsAlerted.HasValue)
            {
                dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
                dungeonMap.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(dungeonMap);
                Path path = null;

                try {
                    path = pathFinder.ShortestPath(
                    dungeonMap.GetCell(monster.X, monster.Y),
                    dungeonMap.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException) {
                    if (monster.WaitMessages != null)
                    {
                        Random random = new Random();
                        int i = random.Next(0, monster.GreetMessages.Length);
                        Game.MessageLog.Add($"{monster.WaitMessages[i]}");
                    }
                    else
                    {
                        Game.MessageLog.Add($"{monster.Name} waits for a turn");
                    }
                }

                dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
                dungeonMap.SetIsWalkable(player.X, player.Y, false);

                if (path != null)
                {
                    try {
                        commandSystem.MoveMonster(monster, path.StepForward());
                    }
                    catch (NoMoreStepsException) {
                        if (monster.WaitMessages != null)
                        {
                            Random random = new Random();
                            int i = random.Next(0, monster.GreetMessages.Length);
                            Game.MessageLog.Add($"{monster.WaitMessages[i]}");
                        }
                        else
                        {
                            Game.MessageLog.Add($"{monster.Name} groans in frustration");
                        }
                    }
                }

                monster.TurnsAlerted++;
                if (monster.TurnsAlerted > 15)
                {
                    monster.TurnsAlerted = null;
                }

                if (monster.State != null && monster.State.Name != "None")
                {
                    monster.State.Perform();
                }
            }
            return true;
        }
    }
}
