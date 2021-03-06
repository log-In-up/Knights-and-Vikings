using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using GameLogic;
using GameLogic.Mechanics;
using UnityEngine;

namespace Entity.States
{
    public sealed class VikingMovementToZoneState : IEntityState
    {
        #region Parameters
        private bool enemyIsOnTheBattlefield, canAttackPlayerBase;
        private float distanceToPlayerBase;

        private readonly VikingBehaviour vikingBehaviour = null;
        private readonly EntityCharacteristics characteristics = null;
        private readonly PlayerBase playerBase = null;
        private readonly EntityHandler entityHandler = null;

        private const int absenceOfKnights = 0;
        #endregion

        public VikingMovementToZoneState(VikingBehaviour vikingBehaviour, EntityCharacteristics characteristics, EntityHandler entityHandler, PlayerBase playerBase)
        {
            this.vikingBehaviour = vikingBehaviour;
            this.entityHandler = entityHandler;
            this.characteristics = characteristics;
            this.playerBase = playerBase;
        }

        #region Interface implementation
        public void Act()
        {
            SearchTargets();

            CheckToAttack();
        }

        public void Close()
        {

        }

        public void Initialize()
        {
            enemyIsOnTheBattlefield = entityHandler.AliveKnights.Count > absenceOfKnights;

            vikingBehaviour.agent.SetDestination(playerBase.transform.position);
        }

        public void Sense()
        {
            distanceToPlayerBase = Vector3.Distance(vikingBehaviour.transform.position, playerBase.transform.position);
        }

        public void Think()
        {
            canAttackPlayerBase = distanceToPlayerBase < characteristics.AttackRange;
            enemyIsOnTheBattlefield = entityHandler.AliveKnights.Count > absenceOfKnights;
        }
        #endregion

        #region Methods
        private void SearchTargets()
        {
            if (enemyIsOnTheBattlefield)
            {
                vikingBehaviour.State = VikingState.Chase;
            }
        }

        private void CheckToAttack()
        {
            if (canAttackPlayerBase)
            {
                vikingBehaviour.agent.ResetPath();

                vikingBehaviour.State = VikingState.AttackBase;
            }
        }
        #endregion
    }
}