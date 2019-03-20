using System;
using System.Text;
using RLNET;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Equipment;

namespace RogueSharpExample.Systems
{
    public class CommandSystem
    {
        public bool IsPlayerTurn { get; set; }
        
        public bool MovePlayer(Direction direction)
        {
            Player player = Game.Player;
            int x = player.X;
            int y = player.Y;

            switch (direction) {
                case Direction.Up: {
                    y = player.Y - 1;
                    break;
                }
                case Direction.Down: {
                    y = player.Y + 1;
                    break;
                }
                case Direction.Left: {
                    x = player.X - 1;
                    break;
                }
                case Direction.Right: {
                    x = player.X + 1;
                    break;
                }
                case Direction.UpLeft: {
                    x = player.X - 1;
                    y = player.Y - 1;
                    break;
                }
                case Direction.UpRight: {
                    x = player.X + 1;
                    y = player.Y - 1;
                    break;
                }
                case Direction.DownLeft: {
                    x = player.X - 1;
                    y = player.Y + 1;
                    break;
                }
                case Direction.DownRight: {
                    x = player.X + 1;
                    y = player.Y + 1;
                    break;
                }
                default: {
                    return false;
                }
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }

            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);
            if (monster != null)
            {
                Attack(player, monster);
                Ability atkAbility = player.Hand.AtkAbility;
                if(atkAbility != null)
                {
                    atkAbility.Victim = monster;
                    atkAbility.Perform();
                    atkAbility.Tick();
                }
                return true;
            }

            Actor shopkeeper = Game.DungeonMap.GetShopkeeperAt(x, y);
            if (shopkeeper != null) // debug
            {
                Game.MessageLog.Add("What are you selling? (Debug)");
                Game.IsSellScreenShowing = true; // debug
                //Game.IsShopSelectionScreenShowing = true;
                Game.TogglePopupScreen();
                return true;
            }

            Actor explorer = Game.DungeonMap.GetExplorerAt(x, y);
            if (explorer != null)
            {
                if (explorer.GreetMessages != null)
                {
                    Random random = new Random();
                    int i = random.Next(0, explorer.GreetMessages.Length);
                    Game.DialogScreen.Dialog = explorer.GreetMessages[i];
                }
                else
                {
                    Game.DialogScreen.Dialog = "Good luck on your quest";
                }

                Game.IsDialogScreenShowing = true;
                Game.TogglePopupScreen();
                return true;
            }

            return false;
        }

        public void ActivateMonsters()
        {
            IScheduleable scheduleable = Game.SchedulingSystem.Get();
            if (scheduleable is Player)
            {
                IsPlayerTurn = true;
                Game.SchedulingSystem.Add(Game.Player);
            }
            else
            {
                if (scheduleable is Monster monster)
                {
                    monster.PerformAction(this);
                    Game.SchedulingSystem.Add(monster);
                }

                ActivateMonsters();
             }
        }

        public void MoveMonster(Monster monster, ICell cell)
        {
            if (!Game.DungeonMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y)
                {
                    Attack(monster, Game.Player);
                }
            }

            if (monster.Status == "Poisoned")
            {
                monster.Health -= monster.PoisonDamage;
            }

            if (monster.Health <= 0)
            {
                monster.Health = 1;
            }

            monster.Tick();
        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);
            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            /*Game.MessageLog.Add(attackMessage.ToString()); // hp debug
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                Game.MessageLog.Add(defenseMessage.ToString()); // hp debug
            }*/

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }

        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;

            if (attacker.AttackMessages != null)
            {
                Random random = new Random();
                int i = random.Next(0, attacker.AttackMessages.Length);
                if (attacker is Player)
                {
                    Game.MessageLog.Add($"{attacker.AttackMessages[i]} {defender.Name}");
                }
                else
                {
                    Game.MessageLog.Add($"{attacker.AttackMessages[i]}");
                }
            }
            else
            {
                Game.MessageLog.Add($"{attacker.Name} attacks {defender.Name}");
                // attackMessage.AppendFormat("{0} attacks {1}", attacker.Name, defender.Name); // debug - previously: ":  and rolls: " at end
            }

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach (TermResult termResult in attackResult.Results)
            {
                // attackMessage.Append(termResult.Value + ", "); // debug
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }

            return hits;
        }

        private static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                // attackMessage.AppendFormat("scoring {0} hits.", hits); // debug
                // defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.Name); // debug

                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                // defenseMessage.AppendFormat("resulting in {0} blocks.", blocks); // debug
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                if (defender is Player)
                {
                    Game.MessageLog.Add($"  You were hit for {damage} damage");
                }
                else
                {
                    Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");
                }

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                if (Game.IsGameOver == false)
                {
                    defender.Status = "Dead";
                    if (defender.DeathMessages != null)
                    {
                        Random random = new Random();
                        int i = random.Next(0, defender.DeathMessages.Length);
                        Game.MessageLog.Add($"{defender.DeathMessages[i]}", Swatch.DbBlood);
                    }
                    else
                    {
                        Game.MessageLog.Add("Game Over", Swatch.DbBlood);
                    }
                }
                Game.IsGameOver = true;
            }
            else if (defender is Monster)
            {
                if (defender.Head != null && defender.Head != HeadEquipment.None())
                {
                    Game.DungeonMap.AddTreasure(defender.X, defender.Y, defender.Head);
                }
                if (defender.Body != null && defender.Body != BodyEquipment.None())
                {
                    Game.DungeonMap.AddTreasure(defender.X, defender.Y, defender.Body);
                }
                if (defender.Hand != null && defender.Hand != HandEquipment.None())
                {
                    Game.DungeonMap.AddTreasure(defender.X, defender.Y, defender.Hand);
                }
                if (defender.Feet != null && defender.Feet != FeetEquipment.None())
                {
                    Game.DungeonMap.AddTreasure(defender.X, defender.Y, defender.Feet);
                }

                Game.DungeonMap.AddGold(defender.X, defender.Y, defender.Gold);
                Game.DungeonMap.RemoveMonster((Monster)defender);

                if (defender.DeathMessages != null)
                {
                    Random random = new Random();
                    int i = random.Next(0, defender.DeathMessages.Length);
                    if (defender.IsEndBoss)
                    {
                        Game.MessageLog.Add($"  {defender.DeathMessages[i]}", Swatch.DbBlood);
                        Game.MessageLog.Add("Congrats, you have completed your quest");
                        // hp dome add game win logic
                    }
                    else if (defender.IsABoss)
                    {
                        Game.MessageLog.Add($"  {defender.DeathMessages[i]}", Swatch.DbBlood);
                    }
                    else
                    {
                        Game.MessageLog.Add($"  {defender.DeathMessages[i]}");
                    }
                }
                else
                {
                    Game.MessageLog.Add($"  {defender.Name} died and dropped {defender.Gold} gold");
                }
            }
        }

        public bool HandleKey(RLKey key) 
        {
            Player player = Game.Player;

            if (key == RLKey.Q)
            {
                return player.QAbility.Perform();
            }
            if (key == RLKey.W)
            {
                return player.WAbility.Perform();
            }
            if (key == RLKey.E)
            {
                return player.EAbility.Perform();
            }
            if (key == RLKey.R)
            {
                return player.RAbility.Perform();
            }
            if (key == RLKey.X || key == RLKey.L || key == RLKey.Slash)
            {
                return player.XAbility.Perform();
            }

            bool didUseItem = false;
            if (key == RLKey.Number1 && player.Item1.RemainingUses > 0)
            {
                didUseItem = player.Item1.Use();
            }
            else if (key == RLKey.Number2 && player.Item2.RemainingUses > 0)
            {
                didUseItem = player.Item2.Use();
            }
            else if (key == RLKey.Number3 && player.Item3.RemainingUses > 0)
            {
                didUseItem = player.Item3.Use();
            }
            else if (key == RLKey.Number4 && player.Item4.RemainingUses > 0)
            {
                didUseItem = player.Item4.Use();
            }
            
            return didUseItem;
        }

        public void EndPlayerTurn()
        {
            Player player = Game.Player;
            if(player.State != null && player.State.Name != "None")
            {
                player.State.Perform();
            }

            if (player.Status != "Poisoned")
            {
                player.Color = Swatch.DbLight;
            }

            if (player.HPRegen.Name != "None" && player.Status == "Healthy" || player.Status == "Hardened")
            {
                player.HPRegen.Perform();
            }
            else if (player.Status == "Poisoned")
            {
                player.Color = RLColor.LightGreen;
                player.Health -= player.PoisonDamage;
            }
            else if (player.Status == "Starving")
            {
                player.Health -= 1;
            }

            if (player.MPRegen.Name != "None" && player.Status == "Healthy" || player.Status == "Hardened")
            {
                player.MPRegen.Perform();
            }
            if (player.Health <= 0)
            {
                if (player.Status == "Starving")
                {
                    Game.IsGameOver = true;
                }
                else
                {
                    player.Health = 1;
                }
            }

            if (Game.IsGameOver == true)
            {
                Game.GameOver();
            }

            IsPlayerTurn = false;
            player.Tick();
        }

        public bool UseItemInInventory(Inventory inventory, char slot)
        {
            bool itemWasUsed = inventory.UseItemInSlot(slot);
            if (itemWasUsed)
            {
                Game.IsInventoryScreenShowing = false;
                Game.TogglePopupScreen();
            }
            return itemWasUsed;
        }

        public bool SellItemInInventory(Inventory inventory, char slot) // hp dome
        {
            bool itemWasSold = inventory.SellItemInSlot(slot);
            if (itemWasSold)
            {
                Game.IsSellScreenShowing = false;
                Game.TogglePopupScreen();
            }
            return itemWasSold;
        }
    }
}
