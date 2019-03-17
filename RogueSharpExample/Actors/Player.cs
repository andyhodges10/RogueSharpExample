﻿using System;
using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Equipment;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Items;

namespace RogueSharpExample.Core
{
    public class Player : Actor
    {
        public IAbility QAbility { get; set; }
        public IAbility WAbility { get; set; }
        public IAbility EAbility { get; set; }
        public IAbility RAbility { get; set; }
        public IAbility XAbility { get; set; }
        public IAbility HPRegen { get; set; }
        public IAbility MPRegen { get; set; }
        public IAbility State   { get; set; }

        public IItem Item1 { get; set; }
        public IItem Item2 { get; set; }
        public IItem Item3 { get; set; }
        public IItem Item4 { get; set; }

        public Player()
        {
            AttackMessages = new string[] { "You take a thrash at the", "You fiercly lunge at the", "You tighten your grip and deliever an overhead swing at the"};
            GreetMessages = new string[] { "You begin your quest to defeat the DragonLord, Good luck", "You embark on your journey to slay the Dragonlord", "Good luck on your journey" };
            DeathMessages = new string[] { "You are dead, not a big surprise", "You fall over dead", "Crumble down to the floor and curse your failures", "You die, you have failed on your quest" };

            Attack = 2;
            AttackChance = 40;
            Awareness = 15;
            Color = Colors.Player;
            Defense = 1;
            DefenseChance = 15;
            Gold = 0;
            Health = 20;
            MaxHealth = 20;
            Mana = 10;
            MaxMana = 10;
            Name = "Novice";
            Speed = 10;
            Level = 1;
            MaxLevel = 15;
            Experience = 8;
            TotalExperience = 0;
            Hunger = 1000;
            MaxHunger = 1200;
            Symbol = '@';
            Status = "Healthy";
            Inventory = new Inventory(this);

            QAbility = new DoNothing();
            WAbility = new DoNothing();
            EAbility = new DoNothing();
            RAbility = new DoNothing();
            XAbility = new Look();

            HPRegen = new Heal(1, 10, 0, "Heal");
            MPRegen = new Meditate(1, 20, 0, "Mediate");
            State =   new DoNothing();

            Item1 = new HealingPotion();
            Item2 = new ManaPotion();
            Item3 = new ToughnessPotion();
            Item4 = new TeleportScroll();
        }

        public bool AddAbility(IAbility ability)
        {
            if (QAbility is DoNothing)
            {
                QAbility = ability;
            }
            else if (WAbility is DoNothing)
            {
                WAbility = ability;
            }
            else if (EAbility is DoNothing)
            {
                EAbility = ability;
            }
            else if (RAbility is DoNothing)
            {
                RAbility = ability;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool AddItem(Item item)
        {
            if (item is HealingPotion)
            {
                Item1.RemainingUses += 1;
            }
            else if (item is ManaPotion)
            {
                Item2.RemainingUses += 1;
            }
            else if (item is ToughnessPotion)  
            {
                Item3.RemainingUses += 1;
            }
            else if (item is TeleportScroll)
            {
                Item4.RemainingUses += 1;
            }
            else
            {
                Game.Player.Inventory.AddInventoryItem(item);
            }
            return true;
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1,  $"Title:  {Name}", Colors.Text);
            int healthWidth = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);

            int remainingHealthWidth = 16 - healthWidth;
            if (Status == "Poisoned") {
                statConsole.SetBackColor(1, 3, healthWidth, 1, RLColor.LightGreen);
            }
            else {
                statConsole.SetBackColor(1, 3, healthWidth, 1, RLColor.LightRed);
            }
            if (Status == "Poisoned") {
                statConsole.SetBackColor(1 + healthWidth, 3, remainingHealthWidth, 1, Colors.PoisonBacking);
            }
            else {
                statConsole.SetBackColor(1 + healthWidth, 3, remainingHealthWidth, 1, Colors.HPBacking);
            }
            if (Status == "Poisoned") {
                statConsole.Print(1, 3, $"HP:      {Health}/{MaxHealth}", RLColor.Green);
            }
            else {
                statConsole.Print(1, 3, $"HP:      {Health}/{MaxHealth}", RLColor.Red);
            }
            int manaWidth = Convert.ToInt32(((double)Mana / (double)MaxMana) * 16.0);
            int remainingManaWidth = 16 - manaWidth;
            statConsole.SetBackColor(1, 5, manaWidth, 1, RLColor.LightBlue); // Previously: Swatch.Primary
            statConsole.SetBackColor(1 + manaWidth, 5, remainingManaWidth, 1, Colors.MPBacking);
            statConsole.Print(1, 5,  $"MP:      {Mana}/{MaxMana}", RLColor.Blue);

            statConsole.Print(1, 7,  $"Attack:  {Attack} ({AttackChance}%)", RLColor.LightRed);
            statConsole.Print(1, 9,  $"Defense: {Defense} ({DefenseChance}%)", RLColor.LightBlue);
            statConsole.Print(1, 11, $"Level:   {Level} ", RLColor.LightGreen);
            statConsole.Print(1, 13, $"To next: {Experience}", RLColor.LightGreen);
            statConsole.Print(1, 15, $"Status:  {Status}", Colors.Gold);
            statConsole.Print(1, 17, $"Gold:    {Gold}", Colors.Gold);
        }
        
        public void DrawInventory(RLConsole inventoryConsole)
        {
            inventoryConsole.Print(1, 1, "Equipment", Colors.InventoryHeading);
            inventoryConsole.Print(1, 3, $"Head: {Head.Name}", Head == HeadEquipment.None() ? Swatch.DbOldStone : Swatch.DbLight);
            inventoryConsole.Print(1, 5, $"Body: {Body.Name}", Body == BodyEquipment.None() ? Swatch.DbOldStone : Swatch.DbLight);
            inventoryConsole.Print(1, 7, $"Hand: {Hand.Name}", Hand == HandEquipment.None() ? Swatch.DbOldStone : Swatch.DbLight);
            inventoryConsole.Print(1, 9, $"Feet: {Feet.Name}", Feet == FeetEquipment.None() ? Swatch.DbOldStone : Swatch.DbLight);

            inventoryConsole.Print(28, 1, "Abilities", Colors.InventoryHeading);
            DrawAbility(QAbility, inventoryConsole, 0);
            DrawAbility(WAbility, inventoryConsole, 1);
            DrawAbility(EAbility, inventoryConsole, 2);
            DrawAbility(RAbility, inventoryConsole, 3);

            inventoryConsole.Print(55, 1, "Items", Colors.InventoryHeading);
            DrawItem(Item1, inventoryConsole, 0);
            DrawItem(Item2, inventoryConsole, 1);
            DrawItem(Item3, inventoryConsole, 2);
            DrawItem(Item4, inventoryConsole, 3);
        }
        
        private void DrawAbility(IAbility ability, RLConsole inventoryConsole, int position)
        {
            char letter = 'Q';
            if (position == 0)
            {
                letter = 'Q';
            }
            else if (position == 1)
            {
                letter = 'W';
            }
            else if (position == 2)
            {
                letter = 'E';
            }
            else if (position == 3)
            {
                letter = 'R';
            }

            RLColor highlightTextColor = Swatch.DbOldStone;
            if (!(ability is DoNothing))
            {
                if (ability.TurnsUntilRefreshed == 0)
                {
                    highlightTextColor = Swatch.DbLight;
                }
                else
                {
                    highlightTextColor = Swatch.DbSkin;
                }
            }

            int xPosition = 28;
            int xHighlightPosition = 28 + 4;
            int yPosition = 3 + (position * 2);
            inventoryConsole.Print(xPosition, yPosition, $"{letter} - {ability.Name}", highlightTextColor);

            if (ability.TurnsToRefresh > 0)
            {
                int width = Convert.ToInt32(((double)ability.TurnsUntilRefreshed / (double)ability.TurnsToRefresh) * 16.0);
                int remainingWidth = 20 - width;
                inventoryConsole.SetBackColor(xHighlightPosition, yPosition, width, 1, Swatch.DbOldBlood);
                inventoryConsole.SetBackColor(xHighlightPosition + width, yPosition, remainingWidth, 1, RLColor.Black);
            }
        }
        
        private void DrawItem(IItem item, RLConsole inventoryConsole, int position)
        {
            int xPosition = 55;
            int yPosition = 3 + (position * 2);
            string place = (position + 1).ToString();
            inventoryConsole.Print(xPosition, yPosition, $"{place} - {item.Name}: {item.RemainingUses}", item.RemainingUses < 1 ? Swatch.DbOldStone : Swatch.DbLight);
        }

        public void LevelUp()
        {
            Player player = Game.Player;
            player.Level += 1;
            Game.MessageLog.Add($"You leveled up to level {player.Level}. You feel invigorated and stronger than before", Colors.Gold);
            ExperienceTable();
            StatGain();
        }

        public void ExperienceTable() // hp
        {
            Player player = Game.Player;
            if (player.Level == 2) {
                player.Experience = 15;
            }
            else if (player.Level == 3) {
                player.Experience = 25;
            }
            else if (player.Level == 4) {
                player.Experience = 40;
            }
            else if (player.Level == 5) {
                player.Experience = 60;
            }
            else if (player.Level == 6) {
                player.Experience = 85;
            }
            else if (player.Level == 7) {
                player.Experience = 110;
            }
            else if (player.Level == 8) {
                player.Experience = 140;
            }
            else if (player.Level == 9) {
                player.Experience = 185;
            }
            else if (player.Level == 10) {
                player.Experience = 240;
            }
            else if (player.Level == 11) {
                player.Experience = 310;
            }
            else if (player.Level == 12) {
                player.Experience = 390;
            }
            else if (player.Level == 13) {
                player.Experience = 480;
            }
            else if (player.Level == 14) {
                player.Experience = 580;
            }
            else {
                player.Experience = 700;
            }
        }

        public void StatGain()
        {
            Player player = Game.Player;

            if (player.Level % 2 == 0) {
                player.BonusMaxHealth += 5;
                player.Health += 5; 
                player.BonusMaxMana += 3;
                player.Mana += 3;
            }
            else {
                player.BonusMaxHealth += 6;
                player.Health += 6;
                player.BonusMaxMana += 4;
                player.Mana += 4;
            }

            if (player.Level == 2)
            {
                player.Name = "Wanderer";
                player.BonusAttack += 1;
                player.QAbility = new Whirlwind(1, 2, 1, 0, 0, 0, "Whirlwind"); // Old: RevealMap(12)
                Game.MessageLog.Add($"Congratulations, you are now a {player.Name}", Colors.Gold);
                Game.MessageLog.Add("You also learned Whirlwind", Colors.Gold);
            }
            else if (player.Level == 3)
            {
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.HPRegen = new Heal(1, 8, 0, "Heal");
                player.BonusAttackChance += 5;
                Game.MessageLog.Add("Your health regen is now faster", Colors.Gold);
            }
            else if (player.Level == 4)
            {
                player.Name = "Explorer";
                player.BonusDefense += 1;
                player.WAbility = new MagicMissile(4, 70, 4, 3, "Magic Missile");
                
                Game.MessageLog.Add($"Congratulations, you are now a {player.Name}", Colors.Gold);
                Game.MessageLog.Add("You also learned Magic Missile", Colors.Gold);
            }
            else if (player.Level == 5)
            {
                player.BonusMaxMana += 3;
                player.Mana += 3;
                player.QAbility = new Whirlwind(2, 3, 4, 0, 0, 0, "Whirlwind 2");
                Game.MessageLog.Add($"You also improved your Whirlwind", Colors.Gold);
            }
            else if (player.Level == 6)
            {
                player.Name = "Apprentice";
                player.BonusAttack += 1;
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.EAbility = new LightningBolt(8, 60, 10, 5, "Lightning Bolt");
                Game.MessageLog.Add($"Congratulations, you are now a {player.Name} ", Colors.Gold);
                Game.MessageLog.Add($"You also learned Lightning Bolt", Colors.Gold);
            }
            else if (player.Level == 7)
            {
                player.BonusMaxMana += 3;
                player.Mana += 3;
                MPRegen = new Meditate(1, 12, 0, "Mediate"); 
                player.WAbility = new MagicMissile(6, 75, 2, 5, "Magic Missile 2");
                Game.MessageLog.Add($"You also improved your Magic Missile", Colors.Gold);
                Game.MessageLog.Add("Your mana regen is now faster", Colors.Gold);
            }
            else if (player.Level == 8)
            {
                player.Name = "Adventurer";
                player.BonusDefense += 1;
                player.BonusDefenseChance += 5;
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.RAbility = new Fireball(10, 70, 2, 15, 8, "Fireball");
                Game.MessageLog.Add($"Congratulations, you are now a {player.Name}", Colors.Gold);
                Game.MessageLog.Add($"You also learned Fireball", Colors.Gold);
            }
            else if (player.Level == 9)
            {
                player.BonusAttack += 1;
                player.BonusAttackChance += 5;
                player.BonusMaxMana += 3;
                player.Mana += 3;
                player.HPRegen = new Heal(1, 6, 0, "Heal");
                player.EAbility = new LightningBolt(10, 65, 8, 6, "Lightning Bolt 2");
                Game.MessageLog.Add($"You also improved your Lightning Bolt", Colors.Gold);
                Game.MessageLog.Add("Your health regen is now faster", Colors.Gold);
            }
            else if (player.Level == 10)
            {
                player.Name = "Champion";
                player.BonusDefense += 1;
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.QAbility = new Whirlwind(2, 2, 5, 40, 5, 2, "Whirlwind 3");
                Game.MessageLog.Add($"Congratulations, you are now a {player.Name}", Colors.Gold);
                Game.MessageLog.Add($"You also improved your Whirlwind", Colors.Gold);
            }
            else if (player.Level == 11)
            {
                player.BonusMaxMana += 3;
                player.Mana += 3;
                player.RAbility = new Fireball(12, 75, 2, 12, 10, "Fireball 2");
                Game.MessageLog.Add($"You also improved your Fireball", Colors.Gold);
            }
            else if (player.Level == 12)
            {
                player.BonusAttack += 1;
                player.Name = "Savior";
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.BonusMaxMana += 2;
                player.Mana += 2;
                player.WAbility = new MagicMissile(8, 80, 1, 6, "Magic Missile 3");
                Game.MessageLog.Add($"Congratulations, you are now a {player.Name}", Colors.Gold);
                Game.MessageLog.Add($"You also improved your Magic Missile", Colors.Gold);
            }
            else if (player.Level == 13)
            {
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.BonusMaxMana += 3;
                player.Mana += 3;
                MPRegen = new Meditate(1, 8, 0, "Mediate");
                player.EAbility = new LightningBolt(12, 70, 6, 7, "Lightning Bolt 3");
                Game.MessageLog.Add($"You also improved your Lightning Bolt", Colors.Gold);
            }
            else if (player.Level == 14)
            {
                player.Name = "Demigod";
                player.BonusDefense += 1;
                player.BonusMaxHealth += 2;
                player.Health += 2;
                player.BonusMaxMana += 3;
                player.Mana += 3;
                player.RAbility = new Fireball(14, 80, 2, 10, 12, "Fireball 3");
                Game.MessageLog.Add($"You also improved your Fireball", Colors.Gold);
                Game.MessageLog.Add("Your mana regen is now faster", Colors.Gold);
            }
            else if (player.Level == 15)
            {
                player.BonusMaxMana +=1;
                player.Mana += 1;
            }
        }

        public void Tick()
        {
            QAbility?.Tick();
            WAbility?.Tick();
            EAbility?.Tick();
            RAbility?.Tick();

            HPRegen?.Tick();
            MPRegen?.Tick();
            State?.Tick();
            --Game.Player.Hunger;
        }
    }
}