using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Systems
{
    public static class AbilityGenerator
    {
        public static Pool<Ability> _abilityPool = null;

        public static Ability CreateAbility()
        {
            if (_abilityPool == null)
            {
                _abilityPool = new Pool<Ability>();
                //_abilityPool.Add(new Heal(10, 0), 10);
                _abilityPool.Add(new MagicMissile(2, 80, 4, 3, 0, 0, 0, false, "Magic Missile"), 10);
                _abilityPool.Add(new RevealMap(15, 0, 0, "Reveal Map"), 10);
                _abilityPool.Add(new Whirlwind(1, 2, 1, 0, 0, 0, "Whirlwind"), 10);
                _abilityPool.Add(new Fireball(8, 60, 2, 10, 8, 0, 0, 0, "Fireball"), 10);
                _abilityPool.Add(new LightningBolt(6, 40, 10, 5, 0, 0, 0, "Lightning Bolt"), 10);
            }

            return _abilityPool.Get();
        }
    }
}
