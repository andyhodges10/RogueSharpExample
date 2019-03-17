using RLNET;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Equipment;
using RogueSharpExample.Abilities; // debug

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
                if (keyPress.Key == RLKey.Up || keyPress.Key == RLKey.Keypad8)
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
                }
                else if (keyPress.Key == RLKey.Escape) // hp Fixme. Implement main menu
                {
                    rootConsole.Close();
                }
                else if (keyPress.Key == RLKey.Insert) // hp Debug
                {
                    Game.MoveDownDungeonLevel();
                    messageLog.Add("You Cheater! Instantly went to next Dungeon", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.Home) // hp Debug
                {
                    player.LevelUp();
                    messageLog.Add("Cheater: Instant levelUp", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.PageUp) // hp Debug
                {
                    player.Body = BodyEquipment.DragonLord();
                    player.Feet = FeetEquipment.Mithril();
                    player.Hand = HandEquipment.Excalibur();
                    player.Head = HeadEquipment.Mithril();
                    player.Health = player.MaxHealth;
                    player.Mana = player.MaxMana;
                    player.IsPoisonedImmune = player.Body.GrantsPoisonImmunity;
                    messageLog.Add("You Cheater! Best equipment given", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.Delete) // hp Debug
                {
                    if (player.IsPoisonedImmune == false)
                    {
                        player.State = new AbnormalState(4, "Poisoned", -1, -1, 3);
                        messageLog.Add("Debug: Poisoned for 4 turns", Swatch.DbBlood);
                    }
                    else
                    {
                        messageLog.Add("Debug: Player is immune to poison", Swatch.DbBlood);
                    }
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.End) // hp Debug
                {
                    player.Hunger = 50;
                    messageLog.Add("Debug: Player hunger set to low", Swatch.DbBlood);
                    didPlayerAct = true;
                }
                else if (keyPress.Key == RLKey.PageDown) // hp Debug
                {
                    player.Hunger = 1000;
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

    public class InputSystemPopupScreen : IInputSystem // hp refactor me
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
                else if (exitMenu && Game.IsShopScreenShowing == true)
                {
                    Game.IsShopScreenShowing = false;
                    Game.TogglePopupScreen();
                }
                else if (exitMenu && Game.IsDialogScreenShowing == true)
                {
                    Game.IsDialogScreenShowing = false;
                    Game.TogglePopupScreen();
                }

                if ("abcdefghijklmnopqrstuvwxyz".Contains(commandChar.ToString())) // hp fix me
                {
                    return commandSystem.UseItemInInventory(Game.Player.Inventory, commandChar);
                }
            }

            return didPlayerAct;
        }
    }
}
