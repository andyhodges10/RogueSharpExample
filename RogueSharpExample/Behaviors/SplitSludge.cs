﻿using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Monsters;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Behaviors
{
    public class SplitSludge : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap map = Game.DungeonMap;

            if (monster.Health >= monster.MaxHealth)
            {
                return false;
            }

            int halfHealth = monster.MaxHealth / 2;
            if (halfHealth <= 0)
            {
                return false;
            }

            ICell cell = FindClosestUnoccupiedCell(map, monster.X, monster.Y);

            if (cell == null)
            {
                return false;
            }

            if (Monster.CloneSludge(monster) is Sludge newSludge)
            {
                newSludge.TurnsAlerted = 1;
                newSludge.X = cell.X;
                newSludge.Y = cell.Y;
                newSludge.MaxHealth = halfHealth;
                newSludge.Health = halfHealth;
                map.AddMonster(newSludge);
                Game.MessageLog.Add($"{monster.Name} splits itself in two");
            }
            else
            {
                return false;
            }

            monster.MaxHealth = halfHealth;
            monster.Health = halfHealth;

            return true;
        }

        private ICell FindClosestUnoccupiedCell(DungeonMap dungeonMap, int x, int y)
        {
            for (int i = 1; i < 5; i++)
            {
                foreach (ICell cell in dungeonMap.GetBorderCellsInCircle(x, y, i))
                {
                    if (cell.IsWalkable)
                    {
                        return cell;
                    }
                }
            }

            return null;
        }
    }
}
