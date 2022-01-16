using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using GameLogic;
using GameLogic.Mechanics;
using UnityEngine;

namespace Entity.States
{
    public sealed class VikingAttackBaseState : IEntityState
    {
        #region Parameters
        private bool canAttack, enemyIsOnTheBattlefield, baseIsNotDestroyed;
        private float attackTime;

        private readonly float damageAmount, attackInterval;

        private readonly PlayerBase playerBase = null;
        private readonly VikingBehaviour vikingBehaviour = null;
        private readonly EntityHandler entityHandler = null;

        private const int absenceOfKnights = 0;
        private const float abscenceOfCapacity = 0.0f;
        #endregion

        public VikingAttackBaseState(VikingBehaviour vikingBehaviour, EntityCharacteristics characteristics, EntityHandler entityHandler, PlayerBase playerBase)
        {
            this.vikingBehaviour = vikingBehaviour;
            this.playerBase = playerBase;
            this.entityHandler = entityHandler;

            damageAmount = characteristics.Damage;
            attackInterval = characteristics.AttackInterval;
        }

        #region Interface implementation
        public void Act()
        {
            SearchTargets();

            AttackPlayerBase();
        }

        public void Close()
        {

        }

        public void Initialize()
        {
            enemyIsOnTheBattlefield = entityHandler.AliveKnights.Count > absenceOfKnights;

            attackTime = Time.time;
        }

        public void Sense()
        {
            canAttack = Time.time >= attackTime;
        }

        public void Think()
        {
            enemyIsOnTheBattlefield = entityHandler.AliveKnights.Count > absenceOfKnights;

            baseIsNotDestroyed = playerBase.BuildingCapacity > abscenceOfCapacity;
        }
        #endregion

        #region Methods
        private void AttackPlayerBase()
        {
            if (canAttack && baseIsNotDestroyed)
            {
                playerBase.ApplyDamage(new DamageInfo(damageAmount));

                attackTime = Time.time + attackInterval;
            }
        }

        private void SearchTargets()
        {
            if (enemyIsOnTheBattlefield)
            {
                vikingBehaviour.State = VikingState.Chase;
            }
        }
        #endregion
    }
}