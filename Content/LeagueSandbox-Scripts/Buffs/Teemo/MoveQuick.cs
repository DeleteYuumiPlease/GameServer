using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using LeagueSandbox.GameServer.GameObjects.Stats;
using GameServerCore.Scripting.CSharp;

namespace MoveQuick
{
    internal class MoveQuick : IBuffGameScript
{
        public BuffType BuffType => BuffType.HASTE;

        public BuffAddType BuffAddType => BuffAddType.STACKS_AND_OVERLAPS;

        public int MaxStacks => 1;

        public bool IsHidden => false;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();

        //TODO: particles..?
        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            StatsModifier.MoveSpeed.PercentBonus += (12f + ownerSpell.CastInfo.SpellLevel * 8) / 100f;
            unit.AddStatModifier(StatsModifier);
        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {

        }

        public void OnUpdate(float diff)
        {

        }
    }
}