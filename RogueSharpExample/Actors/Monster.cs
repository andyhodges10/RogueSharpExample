using System;
using RLNET;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Monsters;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Core
{
    public class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }
        public IAbility State { get; set; }
        public bool IsInvisible { get; set; } // hp implement me
        public bool IsMimicInHiding { get; set; }
        public int PoisonLength { get; set; }
        public int PoisonChance { get; set; }
        
        public void DrawStats(RLConsole statConsole, int position)
        {
            int yPosition = 20 + (position * 2);
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;
            if (Status == "Poisoned") {
                statConsole.SetBackColor(3, yPosition, width, 1, RLColor.LightGreen);
            }
            else {
                statConsole.SetBackColor(3, yPosition, width, 1, RLColor.LightRed);
            }
            if (Status == "Poisoned") {
                statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Colors.PoisonBacking);
            }
            else {
                statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Colors.HPBacking);
            }
            if (Status == "Poisoned") {
                statConsole.Print(2, yPosition, $": {Name}", RLColor.Green);
            }
            else {
                statConsole.Print(2, yPosition, $": {Name}", RLColor.Red);
                //statConsole.Print(13, yPosition, $"HP:{Health}", RLColor.Red); // debug
            }
        }

        public static Monster CloneSludge(Monster anotherMonster)
        {
            return new Sludge
            {
                Attack = anotherMonster.Attack,
                AttackChance = anotherMonster.AttackChance,
                Awareness = anotherMonster.Awareness,
                Color = anotherMonster.Color,
                Defense = anotherMonster.Defense,
                DefenseChance = anotherMonster.DefenseChance,
                Gold = anotherMonster.Gold,
                Health = anotherMonster.Health,
                MaxHealth = anotherMonster.MaxHealth,
                Name = anotherMonster.Name,
                Speed = anotherMonster.Speed,
                Symbol = anotherMonster.Symbol
            };
        }

        public static Monster CloneSlime(Monster anotherMonster)
        {
            return new Slime
            {
                Attack = anotherMonster.Attack,
                AttackChance = anotherMonster.AttackChance,
                Awareness = anotherMonster.Awareness,
                Color = anotherMonster.Color,
                Defense = anotherMonster.Defense,
                DefenseChance = anotherMonster.DefenseChance,
                Gold = anotherMonster.Gold,
                Health = anotherMonster.Health,
                MaxHealth = anotherMonster.MaxHealth,
                Name = anotherMonster.Name,
                Speed = anotherMonster.Speed,
                Symbol = anotherMonster.Symbol
            };
        }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }

        public void Tick()
        {
            State?.Tick();
        }
    }
}
