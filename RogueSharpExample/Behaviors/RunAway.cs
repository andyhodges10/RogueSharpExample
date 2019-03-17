using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Behaviors
{
    public class RunAway : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;

            dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
            dungeonMap.SetIsWalkable(player.X, player.Y, true);

            GoalMap goalMap = new GoalMap(dungeonMap);
            goalMap.AddGoal(player.X, player.Y, 0);

            Path path = null;
            try
            {
                path = goalMap.FindPathAvoidingGoals(monster.X, monster.Y);
            }
            catch (PathNotFoundException)
            {
                Game.MessageLog.Add($"{monster.Name} cowers in fear");
            }

            dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
            dungeonMap.SetIsWalkable(player.X, player.Y, false);

            if (path != null)
            {
                try
                {
                    commandSystem.MoveMonster(monster, path.StepForward());
                }
                catch (NoMoreStepsException)
                {
                    Game.MessageLog.Add($"{monster.Name} cowers in fear");
                }
            }

            return true;
        }
    }
}
