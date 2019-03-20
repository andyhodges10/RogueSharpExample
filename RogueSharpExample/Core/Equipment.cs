using RLNET;
using RogueSharp;
using RogueSharpExample.Equipment;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Equipment : IEquipment, ITreasure, IDrawable
    {
        public Equipment()
        {
            Symbol = ']';
            Color = RLColor.Yellow;
        }

        public int Attack { get; set; }
        public int AttackChance { get; set; }
        public int Awareness { get; set; }
        public int Defense { get; set; }
        public int DefenseChance { get; set; }
        public int MaxHealth { get; set; }
        public int MaxMana { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public int Speed { get; set; }
        public bool GrantsPoisonImmunity { get; set; }
        public bool IsEnhanced { get; set; } // Currently unused
        public int Value { get; set; }

        protected bool Equals(Equipment other)
        {
            return Attack == other.Attack && AttackChance == other.AttackChance && Awareness == other.Awareness && Defense == other.Defense && DefenseChance == other.DefenseChance && MaxHealth == other.MaxHealth && MaxMana == other.MaxMana && string.Equals(Name, other.Name) && string.Equals(Name2, other.Name2) && Speed == other.Speed;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Equipment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Attack;
                hashCode = (hashCode * 397) ^ AttackChance;
                hashCode = (hashCode * 397) ^ Awareness;
                hashCode = (hashCode * 397) ^ Defense;
                hashCode = (hashCode * 397) ^ DefenseChance;
                hashCode = (hashCode * 397) ^ MaxHealth;
                hashCode = (hashCode * 397) ^ MaxMana;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name2 != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Speed;
                hashCode = (hashCode * 397) ^ Value;
                return hashCode;
            }
        }

        public static bool operator ==(Equipment left, Equipment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Equipment left, Equipment right)
        {
            return !Equals(left, right);
        }

        public bool PickUp(IActor actor)
        {
            if (this is HeadEquipment)
            {
                HeadEquipment newHeadEquipment = this as HeadEquipment;
                if (newHeadEquipment.Value > actor.Head.Value)
                {
                    actor.Head = this as HeadEquipment;
                    actor.Health += newHeadEquipment.MaxHealth;
                    actor.Mana += newHeadEquipment.MaxMana;
                    Game.MessageLog.Add($"You picked up a {Name} helmet");
                }
                else
                {
                    Game.MessageLog.Add($"The {Name} helmet is weaker then {actor.Head.Name} helmet, selling");
                    actor.Gold += newHeadEquipment.Value;
                }
                return true;
            }

            if (this is BodyEquipment)
            {
                BodyEquipment newBodyEquipment = this as BodyEquipment;
                if (newBodyEquipment.Value > actor.Body.Value)
                {
                    actor.Body = this as BodyEquipment;
                    actor.IsPoisonedImmune = newBodyEquipment.GrantsPoisonImmunity;
                    actor.Health += newBodyEquipment.MaxHealth;
                    actor.Mana += newBodyEquipment.MaxMana;
                    Game.MessageLog.Add($"You picked up {Name} body armor");
                }
                else
                {
                    Game.MessageLog.Add($"The {Name} body armor is weaker then {actor.Body.Name} body armor, selling");
                    actor.Gold += newBodyEquipment.Value;
                }
                return true;
            }

            if (this is HandEquipment)
            {
                HandEquipment newHandEquipment = this as HandEquipment;
                if (newHandEquipment.Value > actor.Hand.Value)
                {
                    actor.Hand = this as HandEquipment;
                    actor.Health += newHandEquipment.MaxHealth;
                    actor.Mana += newHandEquipment.MaxMana;
                    Game.MessageLog.Add($"You picked up a {Name}");
                }
                else
                {
                    Game.MessageLog.Add($"The {Name} is weaker then {actor.Hand.Name}, selling");
                    actor.Gold += newHandEquipment.Value;
                }
                return true;
            }

            if (this is FeetEquipment)
            {
                FeetEquipment newFeetEquipment = this as FeetEquipment;
                if (newFeetEquipment.Value >= actor.Feet.Value)
                {
                    actor.Feet = this as FeetEquipment;
                    actor.Health += newFeetEquipment.MaxHealth;
                    actor.Mana += newFeetEquipment.MaxMana;
                    Game.MessageLog.Add($"You picked up {Name} boots");
                }
                else
                {
                    Game.MessageLog.Add($"The {Name} boots is weaker then {actor.Feet.Name} boots, selling");
                    actor.Gold += newFeetEquipment.Value;
                }
                return true;
            }

            return false;
        }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, RLColor.Blend(Color, RLColor.Gray, 0.5f), Colors.Background, Symbol);
            }
        }
    }
}
