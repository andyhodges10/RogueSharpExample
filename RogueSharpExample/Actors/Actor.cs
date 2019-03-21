using RLNET;
using RogueSharp;
using RogueSharpExample.Equipment;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Actor : IActor, IDrawable, IScheduleable
    {
        public Actor()
        {
            Head = HeadEquipment.None();
            Body = BodyEquipment.None();
            Hand = HandEquipment.None();
            Feet = FeetEquipment.None();
        }

        public HeadEquipment Head { get; set; }
        public BodyEquipment Body { get; set; }
        public HandEquipment Hand { get; set; }
        public FeetEquipment Feet { get; set; }

        public string[] AttackMessages { get; set; }
        public string[] GreetMessages { get; set; }
        public string[] DeathMessages { get; set; }
        public string[] WaitMessages { get; set; }

        public bool IsABoss { get; set; }
        public bool IsEndBoss { get; set; } // hp implement me

        private int _attack;
        private int _bonusAttack;
        private int _adjustedAttack;
        private int _attackChance;
        private int _bonusAttackChance;
        private int _adjustedAttackChance;
        private int _awareness;
        private int _bonusAwareness;
        private int _adjustedAwareness;
        private int _defense;
        private int _bonusDefense;
        private int _adjustedDefense;
        private int _defenseChance;
        private int _bonusDefenseChance;
        private int _adjustedDefenseChance;
        private int _gold;
        private int _maxGold;
        private int _health;
        private int _maxHealth;
        private int _bonusMaxHealth;
        private int _mana;
        private int _maxMana;
        private int _bonusMaxMana;
        private string _name;
        private string _description;
        private int _speed;
        private int _bonusSpeed;
        private int _adjustedSpeed;
        private int _level;
        private int _maxLevel;
        private int _experience;
        private int _totalExperience;
        private string _status;
        private int _hunger;
        private int _maxHunger;
        private int _poisonDamage;
        private bool _isPoisonedImmune;
        private Inventory _inventory;

        public int Attack {
            get {
                return _attack + Head.Attack + Body.Attack + Hand.Attack + Feet.Attack + _bonusAttack + _adjustedAttack;
            }
            set {
                _attack = value;
            }
        }

        public int BonusAttack {
            get {
                return _bonusAttack;
            }
            set {
                _bonusAttack = value;
            }
        }

        public int AdjustedAttack {
            get {
                return _adjustedAttack;
            }
            set {
                _adjustedAttack = value;
            }
        }
 
        public int AttackChance {
            get {
                return _attackChance + Head.AttackChance + Body.AttackChance + Hand.AttackChance + Feet.AttackChance + _bonusAttackChance + _adjustedAttackChance;
            }
            set {
                _attackChance = value;
            }
        }

        public int BonusAttackChance {
            get {
                return _bonusAttackChance;
            }
            set {
                _bonusAttackChance = value;
            }
        }

        public int AdjustedAttackChance {
            get {
                return _adjustedAttackChance;
            }
            set {
                _adjustedAttackChance = value;
            }
        }

        public int Awareness {
            get {
                return _awareness + Head.Awareness + Body.Awareness + Hand.Awareness + Feet.Awareness + _bonusAwareness + _adjustedAwareness;
            }
            set {
                _awareness = value;
            }
        }

        public int BonusAwareness {
            get {
                return _bonusAwareness;
            }
            set {
                _bonusAwareness = value;
            }
        }

        public int AdjustedAwareness {
            get {
                return _adjustedAwareness;
            }
            set {
                _adjustedAwareness = value;
            }
        }

        public int Defense {
            get {
                return _defense + Head.Defense + Body.Defense + Hand.Defense + Feet.Defense + _bonusDefense + _adjustedDefense;
            }
            set {
                _defense = value;
            }
        }

        public int BonusDefense {
            get {
                return _bonusDefense;
            }
            set {
                _bonusDefense = value;
            }
        }

        public int AdjustedDefense {
            get {
                return _adjustedDefense;
            }
            set {
                _adjustedDefense = value;
            }
        }

        public int DefenseChance {
            get {
                return _defenseChance + Head.DefenseChance + Body.DefenseChance + Hand.DefenseChance + Feet.DefenseChance + _bonusDefenseChance + _adjustedDefenseChance;
            }
            set {
                _defenseChance = value;
            }
        }

        public int BonusDefenseChance {
            get {
                return _bonusDefenseChance;
            }
            set {
                _bonusDefenseChance = value;
            }
        }

        public int AdjustedDefenseChance {
            get {
                return _adjustedDefenseChance;
            }
            set {
                _adjustedDefenseChance = value;
            }
        }
 
        public int Gold {
            get {
                return _gold;
            }
            set {
                 _gold = value;
            }
        }

        public int MaxGold {
            get {
                return _maxGold;
            }
            set {
                _maxGold = value;
            }
        }
 
        public int Health {
            get {
                return _health;
            }
            set {
                _health = value;
            }
        }
 
        public int MaxHealth {
            get {
                return _maxHealth + Head.MaxHealth + Body.MaxHealth + Hand.MaxHealth + Feet.MaxHealth + BonusMaxHealth;
            }
            set {
                _maxHealth = value;
            }
        }

        public int BonusMaxHealth {
            get {
                return _bonusMaxHealth;
            }
            set {
                _bonusMaxHealth = value;
            }
        }

        public int Mana {
            get {
                return _mana;
            }
            set {
                _mana = value;
            }
        }

        public int MaxMana {
            get {
                return _maxMana + Head.MaxMana + Body.MaxMana + Hand.MaxMana + Feet.MaxMana + _bonusMaxMana;
            }
            set {
                _maxMana = value;
            }
        }

        public int BonusMaxMana {
            get {
                return _bonusMaxMana;
            }
            set {
                _bonusMaxMana = value;
            }
        }

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            } 
        }

        public string Description {
            get {
                return _description;
            }
            set {
                _description = value;
            } 
        }
 
        public int Speed {
            get {
                return _speed + Head.Speed + Body.Speed + Hand.Speed + Feet.Speed + _bonusSpeed + _adjustedSpeed;
            }
            set {
                _speed = value;
            }
        }

        public int BonusSpeed {
            get {
                return _bonusSpeed;
            }
            set {
                _bonusSpeed = value;
            }
        }

        public int AdjustedSpeed
        {
            get
            {
                return _adjustedSpeed;
            }
            set
            {
                _adjustedSpeed = value;
            }
        }

        public int Level {
            get {
                return _level;
            }
            set {
                _level = value;
            }
        }

        public int MaxLevel
        {
            get
            {
                return _maxLevel;
            }
            set
            {
                _maxLevel = value;
            }
        }

        public int Experience {
            get {
                return _experience;
            }
            set {
                _experience = value;
            }
        }

        public int TotalExperience {
            get {
                return _totalExperience;
            }
            set {
                _totalExperience = value;
            }
        }

        public string Status {
            get {
                return _status;
            }
            set {
                _status = value;
            }
        }

        public int Hunger {
            get {
                return _hunger;
            }
            set {
                _hunger = value;
            }
        }

        public int MaxHunger
        {
            get {
                return _maxHunger;
            }
            set {
                _maxHunger = value;
            }
        }

        public int PoisonDamage {
            get {
                return _poisonDamage;
            }
            set {
                _poisonDamage = value;
            }
        }

        public bool IsPoisonedImmune {
            get {
                return _isPoisonedImmune;
            }
            set {
                _isPoisonedImmune = value;
            }
        }

        public Inventory Inventory {
            get {
                return _inventory;
            }
            set {
                _inventory = value;
            }
        }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole mapConsole, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            // Taken from DungeonMap.cs
            if (map.IsInFov(X, Y))
            {
                mapConsole.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else if (Game.MapLevel == 1)
            {
                mapConsole.Set(X, Y, Colors.LowLevelFloor, Colors.Background, ',');
            }
            else if (Game.MapLevel == 2)
            {
                mapConsole.Set(X, Y, Colors.LowLevelFloor, Colors.Background, '.');
            }
            else if (Game.MapLevel < 5)
            {
                mapConsole.Set(X, Y, Colors.Floor, Colors.Background, '.');
            }
            else if (Game.MapLevel < 7)
            {
                mapConsole.Set(X, Y, Colors.IceFloor, Colors.Background, '.');
            }
            else if (Game.MapLevel < 9)
            {
                mapConsole.Set(X, Y, Colors.CaveFloor, Colors.Background, '.');
            }
            else
            {
                mapConsole.Set(X, Y, Colors.HellFloor, Colors.Background, '.');
            }
        }

        public int Time
        {
            get {
                return Speed;
            }
        }
    }
}
