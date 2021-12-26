using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using UnityEngine;

namespace Entity.States
{
    public sealed class KnightAttackState : IEntityState
    {
        #region Parameters
        private bool canAttack, canChase, enemyIsDead, enemyIsOnTheBattlefield;
        private float attackTime, distanceBetweenSelfAndEnemy;

        private readonly float attackInterval, attackRange, damageAmount;
        private readonly KnightBehaviour knightBehaviour = null;

        private const float noHealthPoints = 0.0f;
        private const int absenceVikings = 0;
        #endregion

        public KnightAttackState(KnightBehaviour knightBehaviour, EntityCharacteristics characteristics)
        {
            this.knightBehaviour = knightBehaviour;

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
            distanceBetweenSelfAndEnemy = Vector3.Distance(knightBehaviour.transform.position, knightBehaviour.enemy.transform.position);
        }

        public void Think()
        {
            enemyIsDead = knightBehaviour.enemy.HealthPoints <= noHealthPoints;
            enemyIsOnTheBattlefield = knightBehaviour.BattleCurator.EntityHandler.AliveVikings.Count > absenceVikings;

            canChase = distanceBetweenSelfAndEnemy > attackRange;

            canAttack = Time.time >= attackTime;
        }
        #endregion

        #region Methods
        private void AttackTarget()
        {
            if (canAttack)
            {
                knightBehaviour.CauseDamage(new DamageInfo(damageAmount));

                attackTime += attackInterval;
            }
        }

        private void CheckTarget()
        {
            if (enemyIsDead)
            {
                if (enemyIsOnTheBattlefield)
                {
                    knightBehaviour.enemy = knightBehaviour.SetTarget(EntityType.Viking);
                }
                else
                {
                    knightBehaviour.State = KnightState.Await;
                }
            }

            if (canChase)
            {
                knightBehaviour.State = KnightState.Chase;
            }
        }
        #endregion
    }
}