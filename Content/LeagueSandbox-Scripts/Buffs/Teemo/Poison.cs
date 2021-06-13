using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using LeagueSandbox.GameServer.GameObjects.Stats;
using GameServerCore.Scripting.CSharp;

namespace Poison
{
    internal class Poison : IBuffGameScript
    {
        public BuffType BuffType => BuffType.POISON;

        public BuffAddType BuffAddType => BuffAddType.REPLACE_EXISTING;

        public int MaxStacks => 1;

        public bool IsHidden => false;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();

        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            var damage = 6 * ownerSpell.CastInfo.SpellLevel + ownerSpell.CastInfo.Owner.Stats.AbilityPower.Total * 0.3f;
            unit.TakeDamage(ownerSpell.CastInfo.Owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, false);
        }
        public void OnTakeDamage(IAttackableUnit unit, IAttackableUnit source)
        {

        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {

        }

        public void OnUpdate(float diff)
        {

        }
    }
}