using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Equipment;
using System.Collections.Generic;

namespace RogueSharpExample.Abilities
{
    public class Look : Ability, ITargetable
    {
        protected override bool PerformAbility()
        {
            Game.MessageLog.Add("Select the target you want to investigate");
            return Game.TargetingSystem.SelectArea(this, 0);
        }

        public void SelectTarget(Point target)
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            List<TreasurePile> checkTreasurePile = map.GetItemsAt(target.X, target.Y);
            List<Plant> checkPlant = map.GetPlantsAt(target.X, target.Y);
            Monster currentMonster = map.GetMonsterAt(target.X, target.Y);
            if (map.GetCell(target.X, target.Y).IsWalkable)
            {
                if (checkTreasurePile != null)
                {
                    for (int i = 0; i < checkTreasurePile.Count; i++)
                    {
                        if (checkTreasurePile[i].Treasure is Trap)
                        {
                            Game.MessageLog.Add($"That is a {checkTreasurePile[i].Treasure.Name}");
                            foreach (ICell cell in Game.DungeonMap.GetCellsInSquare(target.X, target.Y, 1))
                            {
                                if (Game.DungeonMap.CheckForPlayer(cell.X, cell.Y))
                                {
                                    Game.DungeonMap.RemoveTrap(target.X, target.Y);
                                    Game.MessageLog.Add($"You disarm the {checkTreasurePile[i].Treasure.Name}");
                                }
                            }
                        }
                        else if (checkTreasurePile[i].Treasure is HeadEquipment ||
                           checkTreasurePile[i].Treasure is HandEquipment ||
                           checkTreasurePile[i].Treasure is FeetEquipment ||
                           checkTreasurePile[i].Treasure is BodyEquipment)
                        {
                            Game.MessageLog.Add($"That is a {checkTreasurePile[i].Treasure.Name} {checkTreasurePile[i].Treasure.Description}");
                        }
                        else
                        {
                            Game.MessageLog.Add($"That is a {checkTreasurePile[i].Treasure.Name}");
                        }
                    }
                }

                for (int i = 0; i < Game.DungeonMap.Doors.Count; i++)
                {
                    if (Game.DungeonMap.Doors[i].X == target.X && Game.DungeonMap.Doors[i].Y == target.Y)
                    {
                        // We know the door is open because the tile has to be walkable
                        Game.MessageLog.Add("That is an open door");
                    }
                }

                if (Game.DungeonMap.StairsUp != null)
                {
                    if (Game.DungeonMap.StairsUp.X == target.X && Game.DungeonMap.StairsUp.Y == target.Y)
                    {
                        // We know the door is open because the tile has to be walkable
                        Game.MessageLog.Add("That is some stairs leading up.");
                    }
                }

                if (Game.DungeonMap.StairsDown != null)
                {
                    if (Game.DungeonMap.StairsDown.X == target.X && Game.DungeonMap.StairsDown.Y == target.Y)
                    {
                        // We know the door is open because the tile has to be walkable
                        Game.MessageLog.Add("That is some stairs leading down.");
                    }
                }
            }
            else
            {
                if (currentMonster != null)
                {
                    Game.MessageLog.Add($"That is a {currentMonster.Name}");
                    //Game.MessageLog.Add($"That is a {currentMonster.Description}"); // implement me
                }
                if (checkTreasurePile != null)
                {
                    Game.MessageLog.Add("That is an item sitting on a Tree. A programmer blushes in an alternate dimension");
                }
                if (checkPlant != null)
                {
                    for (int i = 0; i < checkPlant.Count; i++)
                    {
                        if (checkPlant[i] is Tree)
                        {
                            Game.MessageLog.Add($"That is a {checkPlant[i].Name}");
                        }
                    }
                }
                for (int i = 0; i < Game.DungeonMap.Doors.Count; i++)
                {
                    if (Game.DungeonMap.Doors[i].X == target.X && Game.DungeonMap.Doors[i].Y == target.Y)
                    {
                        // We know the door is open because the tile has to be walkable
                        Game.MessageLog.Add("That is a closed door");
                    }
                }
            }
        }
    }
}
