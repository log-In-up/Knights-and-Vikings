using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using UnityEngine;

namespace Entity.States
{
    public sealed class KnightChaseState : IEntityState
    {
        #region Parameters
        private bool canAttack;
        private float distanceToTarget;

        private readonly float attackRange;

        private readonly KnightBehaviour knightBehaviour = null;

        private const float initialDistanceToTarget = 0.0f;
        #endregion

        public KnightChaseState(KnightBehaviour knightBehaviour, EntityCharacteristics characteristics)
        {
            this.knightBehaviour = knightBehaviour;

            attackRange = characteristics.AttackRange;
        }

        #region Interface implementation
        public void Act()
        {
            StartAttackTarget();
        }

        public void Close()
        {
            knightBehaviour.agent.ResetPath();
        }

        public void Initialize()
        {
            SetDestination();

            canAttack = false;
            distanceToTarget = initialDistanceToTarget;
        }

        public void Sense()
        {
            distanceToTarget = Vector3.Distance(knightBehaviour.transform.position, knightBehaviour.enemy.transform.position);
        }

        public void Think()
        {
            canAttack = distanceToTarget <= attackRange;
        }
        #endregion

        #region Methods
        private void StartAttackTarget()
        {
            if (canAttack)
            {
                knightBehaviour.State = KnightState.Attack;
            }
        }

        private void SetDestination()
        {
            knightBehaviour.enemy = knightBehaviour.SetTarget(EntityType.Viking);

            knightBehaviour.agent.SetDestination(knightBehaviour.enemy.transform.position);//Null reference exception
        }
        #endregion
    }
}