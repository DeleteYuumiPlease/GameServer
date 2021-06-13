using GameServerCore.Domain.GameObjects.Spell;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Enums;

namespace Spells
{
    class AatroxQ : ISpellScript
    {

        public ISpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true
            // TODO
        };
        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
            PlayAnimation(owner, "Spell1");
        }

        public void OnSpellCast(ISpell spell)
        {
            var owner = spell.CastInfo.Owner;

            if (spell.CastInfo.Owner is IChampion c)
            {
                var damage = 200 + (100 * (spell.CastInfo.SpellLevel - 1)) + (c.Stats.AbilityPower.Total);

                var target = spell.CastInfo.TargetPosition;
                var current = new Vector2(owner.Position.X, owner.Position.Y);
                var to = new Vector2(target.X, target.Z);
                var range = to * 800;

                LogDebug($"{spell.CastInfo.TargetPosition} TARGET   ${to} TO");
                
                CreateTimer(0.5f, () => {
                    ForceMovement(c, "Spell1", to, 2200, 0, 10, 300);
                    var units = GetUnitsInRange(c.Position, 450f, true);
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (units[i].Team != c.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret))
                        {
                            units[i].TakeDamage(c, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                        }
                    }
                });



            }
        }

        public void OnSpellPostCast(ISpell spell)
        {

        }

        public void OnSpellChannel(ISpell spell)
        {
        }

        public void OnSpellChannelCancel(ISpell spell)
        {
        }

        public void OnSpellPostChannel(ISpell spell)
        {
        }

        public void OnUpdate(float diff)
        {
        }

    }
}
