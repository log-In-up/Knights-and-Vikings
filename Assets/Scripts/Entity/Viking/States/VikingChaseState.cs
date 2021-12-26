using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using UnityEngine;

namespace Entity.States
{
    public sealed class VikingChaseState : IEntityState
    {
        #region Parameters
        private bool canAttack;
        private float distanceToTarget;

        private readonly float attackRange;
        private readonly VikingBehaviour vikingBehaviour = null;

        private const float initialDistanceToTarget = 0.0f;
        #endregion

        public VikingChaseState(VikingBehaviour vikingBehaviour, EntityCharacteristics characteristics)
        {
            this.vikingBehaviour = vikingBehaviour;

            attackRange = characteristics.AttackRange;
        }

        #region Interface implementation
        public void Act()
        {
            CheckDistanceToTarget();
        }

        public void Close()
        {
            vikingBehaviour.agent.ResetPath();
        }

        public void Initialize()
        {
            SetDestination();

            canAttack = false;

            distanceToTarget = initialDistanceToTarget;
        }

        public void Sense()
        {
            distanceToTarget = Vector3.Distance(vikingBehaviour.transform.position, vikingBehaviour.enemy.transform.position);
        }

        public void Think()
        {
            canAttack = distanceToTarget <= attackRange;
        }
        #endregion

        #region Methods
        private void CheckDistanceToTarget()
        {
            if (canAttack)
            {
                vikingBehaviour.State = VikingState.Attack;
            }
        }

        private void SetDestination()
        {
            vikingBehaviour.enemy = vikingBehaviour.SetTarget(EntityType.Knight);

            vikingBehaviour.agent.SetDestination(vikingBehaviour.enemy.transform.position);
        }
        #endregion
    }
}