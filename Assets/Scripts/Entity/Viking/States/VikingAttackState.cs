using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using UnityEngine;

namespace Entity.States
{
    public sealed class VikingAttackState : IEntityState
    {
        #region Parameters
        private bool canAttack, canChase, enemyIsDead, enemyIsOnTheBattlefield;
        private float attackTime, distanceBetweenSelfAndEnemy;

        private readonly float attackInterval, attackRange, damageAmount;
        private readonly VikingBehaviour vikingBehaviour = null;

        private const float noHealthPoints = 0.0f;
        private const int absenceOfKnights = 0;
        #endregion

        public VikingAttackState(VikingBehaviour vikingBehaviour, EntityCharacteristics characteristics)
        {
            this.vikingBehaviour = vikingBehaviour;

            attackInterval = characteristics.AttackInterval;
            attackRange = characteristics.AttackRange;
            damageAmount = characteristics.Damage;
        }

        #region Interface implementation
        public void Act()
        {
            CheckTarget();

            AttackTarget();
        }

        public void Close()
        {

        }

        public void Initialize()
        {
            canAttack = canChase = enemyIsDead = enemyIsOnTheBattlefield = false;

            attackTime = Time.time;
        }

        public void Sense()
        {
            distanceBetweenSelfAndEnemy = Vector3.Distance(vikingBehaviour.transform.position, vikingBehaviour.enemy.transform.position);
        }

        public void Think()
        {
            enemyIsDead = vikingBehaviour.enemy.HealthPoints <= noHealthPoints;
            enemyIsOnTheBattlefield = vikingBehaviour.BattleCurator.EntityHandler.AliveKnights.Count > absenceOfKnights;

            canChase = distanceBetweenSelfAndEnemy > attackRange;

            canAttack = Time.time >= attackTime;
        }
        #endregion

        #region Methods
        private void AttackTarget()
        {
            if (canAttack)
            {
                vikingBehaviour.CauseDamage(new DamageInfo(damageAmount));

                attackTime = Time.time + attackInterval;
            }
        }

        private void CheckTarget()
        {
            if (enemyIsDead)
            {
                if (enemyIsOnTheBattlefield)
                {
                    vikingBehaviour.enemy = vikingBehaviour.SetTarget(EntityType.Knight);
                }
                else
                {
                    vikingBehaviour.State = VikingState.MovementToZone;
                }
            }

            if (canChase)
            {
                vikingBehaviour.State = VikingState.Chase;
            }
        }
        #endregion
    }
}