using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using LeagueSandbox.GameServer.GameObjects.Stats;
using GameServerCore.Scripting.CSharp;

namespace MoveQuickPassive
{
    internal class MoveQuickPassive : IBuffGameScript
    {
        public BuffType BuffType => BuffType.HASTE;

        public BuffAddType BuffAddType => BuffAddType.REPLACE_EXISTING;

        public int MaxStacks => 1;

        public bool IsHidden => false;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();
        //kinda useless until i find a way to make it work as a W passive
        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            StatsModifier.MoveSpeed.PercentBonus += (6f + ownerSpell.CastInfo.SpellLevel * 4) / 100f;
            unit.AddStatModifier(StatsModifier);
        }
        public void OnTakeDamage(IAttackableUnit unit, IAttackableUnit source)
        {
            var buff = unit.GetBuffWithName("MoveQuickPassive");
            if (buff != null)
            {
                buff.DeactivateBuff();
            }
        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {

        }

        public void OnUpdate(float diff)
        {

        }
    }
}