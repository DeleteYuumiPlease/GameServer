using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Scripting.CSharp;
using System.Linq;
using GameServerCore;

namespace Spells
{
    public class LissandraW : ISpellScript
    {
        public ISpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
        };


        private float _getBaseDamageByLevel(byte lvl)
        {
            var damage = 0;
            switch (lvl)
            {
                case 1:
                    damage = 70;
                    break;
                case 2:
                    damage = 105;
                    break;
                case 3:
                    damage = 140;
                    break;
                case 4:
                    damage = 170;
                    break;
                case 5:
                    damage = 200;
                    break;
                    
            }
            return damage;
        }

        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
            var ap = spell.CastInfo.Owner.Stats.AbilityPower.Total;
            var damage = _getBaseDamageByLevel(spell.CastInfo.SpellLevel) + ap * 0.4f;

            var stunDuraton = 1 + spell.CastInfo.SpellLevel / 10;

            PlayAnimation(owner, "Spell2");
            AddParticle(owner, null, "Lissandra_W_nova.troy", owner.Position);
            AddParticle(owner, null, "Lissandra_W_Missile.troy", owner.Position);

            var units = GetUnitsInRange(owner.Position, 450f, true);

            foreach(IAttackableUnit unit in units
                .Where(x => x.Team == CustomConvert.GetEnemyTeam(spell.CastInfo.Owner.Team)))
            {
                if (unit is IBaseTurret)
                {
                    continue;
                }

                AddBuff("Stun", stunDuraton, 1, spell, unit, owner);

                unit.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, DamageResultType.RESULT_NORMAL);

                if (unit is IChampion)
                {
                    AddParticleTarget(owner, unit, "Lissandra_W_root_champion.troy", unit);
                }
                else
                {
                    AddParticleTarget(owner, unit, "Lissandra_W_root_minion.troy", unit);
                }
            }
        }

        public void OnSpellCast(ISpell spell)
        {
            
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