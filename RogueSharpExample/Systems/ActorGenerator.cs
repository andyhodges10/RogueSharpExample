using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Monsters;

namespace RogueSharpExample.Systems
{
    public static class ActorGenerator
    {
        private static Player _player = null;

        public static Monster CreateMonster(int level, Point location)
        {
            Pool<Monster> monsterPool = new Pool<Monster>();

            if (level <= 2)
            {
                monsterPool.Add(Rat.Create(level), 20);
                monsterPool.Add(Lichen.Create(level), 30);
                monsterPool.Add(Jackal.Create(level), 25);
                monsterPool.Add(Kobold.Create(level), 25);
            }
            else if (level <= 4)
            {
                monsterPool.Add(Lichen.Create(level), 15);
                monsterPool.Add(Rat.Create(level), 16);
                monsterPool.Add(Jackal.Create(level), 25);
                monsterPool.Add(Kobold.Create(level), 25);
                monsterPool.Add(Goblin.Create(level), 15);
                monsterPool.Add(Ooze.Create(level), 3);
                monsterPool.Add(Wolf.Create(level), 1);
            }
            else if (level <= 6)
            {
                monsterPool.Add(Jackal.Create(level), 5);
                monsterPool.Add(Wolf.Create(level), 10);
                monsterPool.Add(Kobold.Create(level), 15); 
                monsterPool.Add(Goblin.Create(level), 30);
                monsterPool.Add(Ooze.Create(level), 25);
                monsterPool.Add(Gnoll.Create(level), 5);
                monsterPool.Add(Viper.Create(level), 10);
            }
            else if (level <= 8)
            {
                monsterPool.Add(Ooze.Create(level), 10);
                monsterPool.Add(Goblin.Create(level), 10);
                monsterPool.Add(Slime.Create(level), 15);
                monsterPool.Add(Viper.Create(level), 10);
                monsterPool.Add(Wolf.Create(level), 20);
                monsterPool.Add(Gnoll.Create(level), 25);
                monsterPool.Add(LizardMan.Create(level), 10);
            }
            else if (level <= 10)
            {
                monsterPool.Add(Viper.Create(level), 10);
                monsterPool.Add(Gnoll.Create(level), 10);
                monsterPool.Add(Wolf.Create(level), 20);
                monsterPool.Add(LizardMan.Create(level), 30);
                monsterPool.Add(Orc.Create(level), 20);
                monsterPool.Add(Dragon.Create(level), 10);
            }
            else
            {
                monsterPool.Add(LizardMan.Create(level), 40);
                monsterPool.Add(Orc.Create(level), 30);
                monsterPool.Add(Dragon.Create(level), 30);
            }

            Monster monster = monsterPool.Get();
            monster.X = location.X;
            monster.Y = location.Y;

            return monster;
        }

        public static Player CreatePlayer()
        {
            if (_player == null)
            {
                _player = new Player();
            }

            return _player;
        }

        public static Monster CreateBoss(int level, Point location) // hp
        {
            Pool<Monster> bossPool = new Pool<Monster>();

            if (level <= 4)
            {
                bossPool.Add(KoboldChief.Create(level), 100);
            }
            else if (level <= 6)
            {
                bossPool.Add(OrcLeader.Create(level), 100);
            }
            else if (level <= 8)
            {
                bossPool.Add(OrcLeader.Create(level), 100);
            }
            else
            {
                bossPool.Add(DragonLord.Create(level), 100);
            }

            Monster boss = bossPool.Get();
            boss.X = location.X;
            boss.Y = location.Y;

            return boss;
        }

        public static Monster CreateMimic(int level, Point location)
        {
            Pool<Monster> mimicPool = new Pool<Monster>();

            if (level <= 7)
            {
                mimicPool.Add(Mimic.Create(level), 100);
            }
            else
            {
                mimicPool.Add(Mimic.Create(level), 100);
            }

            Monster mimic = mimicPool.Get();
            mimic.X = location.X;
            mimic.Y = location.Y;

            return mimic;
        }

        public static Actor CreateNPC(int level, Point location)
        {
            Pool<Actor> npcPool = new Pool<Actor>();

            if (level <= 2)
            {
                npcPool.Add(Explorer.Create(level, new string[] { "Good luck, you will need it", "You should watch out for the rat-ant queen" }), 100);
            }
            else if (level <= 4)
            {
                npcPool.Add(Shopkeeper.Create(level), 100);
            }
            else if (level == 5)
            {
                npcPool.Add(Explorer.Create(level, new string[] { "Wow, it's cold down here" }), 100);
            }
            else if (level == 6)
            {
                npcPool.Add(Shopkeeper.Create(level), 100);
            }
            else if (level == 7)
            {
                npcPool.Add(Explorer.Create(level, new string[] { "Be weary of Orks" }), 100);
            }
            else
            {
                npcPool.Add(Shopkeeper.Create(level), 100);
            }

            Actor npc = npcPool.Get();
            npc.X = location.X;
            npc.Y = location.Y;

            return npc;
        }
    }
}
