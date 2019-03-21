using RLNET;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Equipment;
using RogueSharpExample.Abilities; // debug
using RogueSharpExample.Items;

namespace RogueSharpExample.Systems
{
    public class InputSystemPlaying : IInputSystem
    {
        public bool GetInput(RLRootConsole rootConsole, CommandSystem commandSystem)
        {
            Player player = Game.Player;
            MessageLog messageLog = Game.MessageLog;
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();
            bool didPlayerAct = false;

            if (keyPress != null)
            {
                if (keyPress.Key == RLKey.Escape) // hp Fixme. Implement main menu
                {
                    rootConsole.Close();
                }
                else if (player.Status == "Dead")
                {
                    didPlayerAct = true;
                }
                else if (player.Status == "Stuck")
                {
                    messageLog.Add("You spend a turn trying to get out of your predicament", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.Up || keyPress.Key == RLKey.Keypad8)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.Up);
                }
                else if (keyPress.Key == RLKey.Down || keyPress.Key == RLKey.Keypad2)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.Down);
                }
                else if (keyPress.Key == RLKey.Left || keyPress.Key == RLKey.Keypad4)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.Left);
                }
                else if (keyPress.Key == RLKey.Right || keyPress.Key == RLKey.Keypad6)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.Right);
                }
                else if (keyPress.Key == RLKey.Keypad7)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.UpLeft);
                }
                else if (keyPress.Key == RLKey.Keypad9)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.UpRight);
                }
                else if (keyPress.Key == RLKey.Keypad1)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.DownLeft);
                }
                else if (keyPress.Key == RLKey.Keypad3)
                {
                    didPlayerAct = commandSystem.MovePlayer(Direction.DownRight);
                }
                else if (keyPress.Key == RLKey.Keypad5 || keyPress.Key == RLKey.KeypadPeriod
                    || keyPress.Key == RLKey.Period || keyPress.Key == RLKey.Comma)
                {
                    didPlayerAct = Game.AttemptMoveDownDungeonLevel();

                    if (!didPlayerAct)
                    {
                        didPlayerAct = Game.AttemptMoveUpDungeonLevel();
                    }

                    if (!didPlayerAct)
                    {
                        didPlayerAct = true;
                    }
                }
                else if (keyPress.Key == RLKey.I)
                {
                    Game.IsInventoryScreenShowing = true;
                    Game.TogglePopupScreen();
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.Insert) // hp Debug
                {
                    Game.MoveDownDungeonLevel();

                    messageLog.Add("You Cheater! Instantly went to next Dungeon", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.Home) // Debug
                {
                    player.LevelUp();

                    messageLog.Add("You Cheater! Instant level Up", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.PageUp) // Debug
                {
                    player.Body = BodyEquipment.DragonLord();
                    player.Feet = FeetEquipment.DragonLord();
                    if (player.Hand == HandEquipment.Vampiric()) {
                        player.Hand = HandEquipment.DragonLord();
                    }
                    else { 
                        player.Hand = HandEquipment.Vampiric();
                    }
                    player.Head = HeadEquipment.DragonLord();
                    player.Health = player.MaxHealth;
                    player.Mana = player.MaxMana;
                    player.IsPoisonedImmune = player.Body.GrantsPoisonImmunity;
                    messageLog.Add("You Cheater! Best equipment given and health fully restored", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.Delete) // Debug
                {
                    messageLog.Add("Debug: Poisoned for 4 turns and hunger set to low", Swatch.DbBlood);
                    player.Hunger = 50;
                    if (player.IsPoisonedImmune == false)
                    {
                        player.State = new AbnormalState(6, "Poisoned", "The Poison has stated to take its full effect", -2, -2, -3, 2, 3);
                    }
                    else
                    {
                        messageLog.Add("Thankfully you are completely immune to poison", Swatch.DbBlood);
                    }
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.End) // Debug
                {
                    player.Gold += 5000;
                    WeaponScroll WeaponScroll = new WeaponScroll();
                    ArmorScroll ArmorScroll = new ArmorScroll();
                    BookOfWhirlwind BookOfWhirlwind = new BookOfWhirlwind(4);
                    BookOfHealing BookOfHealing = new BookOfHealing(4);
                    BookOfSacrifice BookOfSacrifice = new BookOfSacrifice(4);
                    FoodRation FoodRation = new FoodRation(3);
                    RevealMapScroll RevealMapScroll = new RevealMapScroll();
                    SerpentWand SerpentWand = new SerpentWand(3);
                    PoisonFlask PoisonFlask = new PoisonFlask(3);
                    ExplosiveFlask ExplosiveFlask = new ExplosiveFlask(3);
                    VampiricWand VampiricWand = new VampiricWand(3);
                    player.Inventory.AddInventoryItem(WeaponScroll);
                    player.Inventory.AddInventoryItem(ArmorScroll);
                    player.Inventory.AddInventoryItem(BookOfWhirlwind);
                    player.Inventory.AddInventoryItem(BookOfHealing);
                    player.Inventory.AddInventoryItem(BookOfSacrifice);
                    player.Inventory.AddInventoryItem(FoodRation);
                    player.Inventory.AddInventoryItem(RevealMapScroll);
                    player.Inventory.AddInventoryItem(SerpentWand);
                    player.Inventory.AddInventoryItem(PoisonFlask);
                    player.Inventory.AddInventoryItem(ExplosiveFlask);
                    player.Inventory.AddInventoryItem(VampiricWand);
                    messageLog.Add("You Cheater! A bunch of gold and Rare Inventory items given", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.PageDown) // Debug
                {
                    player.Hunger = 1200;

                    messageLog.Add("You Cheater! Hunger set to full", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else
                {
                    didPlayerAct = commandSystem.HandleKey(keyPress.Key);
                }
            }

            return didPlayerAct;
        }
    }

    public class InputSystemPopupScreen : IInputSystem // hp complete me
    {
        public bool GetInput(RLRootConsole rootConsole, CommandSystem commandSystem)
        {
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();
            bool didPlayerAct = false;

            if (keyPress != null)
            {
                char commandChar = keyPress.Key.ToString().ToLower().ToCharArray()[0];
                bool exitMenu = false;

                if(keyPress.Key == RLKey.Keypad1 || keyPress.Key == RLKey.Keypad2 || keyPress.Key == RLKey.Keypad3
                    || keyPress.Key == RLKey.Keypad4 || keyPress.Key == RLKey.Keypad5 || keyPress.Key == RLKey.Keypad6
                    || keyPress.Key == RLKey.Keypad7 || keyPress.Key == RLKey.Keypad8 || keyPress.Key == RLKey.Keypad9
                    || keyPress.Key == RLKey.Up|| keyPress.Key == RLKey.Left || keyPress.Key == RLKey.Right
                    || keyPress.Key == RLKey.Down || keyPress.Key == RLKey.Escape)
                {
                    exitMenu = true;
                }

                if (exitMenu && Game.IsInventoryScreenShowing == true)
                {
                    Game.IsInventoryScreenShowing = false;
                    Game.TogglePopupScreen();
                }
                else if (exitMenu && Game.IsDialogScreenShowing == true)
                {
                    Game.IsDialogScreenShowing = false;
                    Game.TogglePopupScreen();
                }
                else if (exitMenu && Game.IsShopSelectionScreenShowing == true)
                {
                    Game.IsShopSelectionScreenShowing = false;
                    Game.TogglePopupScreen();
                }
                else if (exitMenu && Game.IsBuyScreenShowing == true)
                {
                    Game.IsBuyScreenShowing = false;
                    Game.TogglePopupScreen();
                }
                else if (exitMenu && Game.IsSellScreenShowing == true)
                {
                    Game.IsSellScreenShowing = false;
                    Game.TogglePopupScreen();
                }

                if ("abcdefghijklmnopqrstuvwxyz".Contains(commandChar.ToString()) && Game.IsInventoryScreenShowing == true)
                {
                    return commandSystem.UseItemInInventory(Game.Player.Inventory, commandChar);
                }

                if ("abcdefghijklmnopqrstuvwxyz".Contains(commandChar.ToString()) && Game.IsBuyScreenShowing == true)
                {
                    return commandSystem.BuyItemAtShop(Game.Shopkeeper.Inventory, commandChar);
                }

                if ("abcdefghijklmnopqrstuvwxyz".Contains(commandChar.ToString()) && Game.IsSellScreenShowing == true)
                {
                    return commandSystem.SellItemInInventory(Game.Player.Inventory, commandChar);
                }
            }

            return didPlayerAct;
        }
    }
}
