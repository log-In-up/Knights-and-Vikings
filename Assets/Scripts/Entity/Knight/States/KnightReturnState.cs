using Entity.Behaviours;
using Entity.Enums;
using Entity.Interfaces;
using GameLogic.Mechanics;

namespace Entity.States
{
    public sealed class KnightReturnState : IEntityState
    {
        #region Parameters
        private bool enemyIsOnTheBattlefield;

        private readonly KnightBehaviour knightBehaviour = null;
        private readonly EntityHandler entityHandler = null;

        private const int absenceVikings = 0;
        #endregion

        public KnightReturnState(KnightBehaviour knightBehaviour, EntityHandler entityHandler)
        {
            this.knightBehaviour = knightBehaviour;
            this.entityHandler = entityHandler;
        }

        #region Interface implementation
        public void Act()
        {
            CheckForTargets();
        }

        public void Close()
        {
            knightBehaviour.agent.ResetPath();
        }

        public void Initialize()
        {
            knightBehaviour.agent.SetDestination(knightBehaviour.rallyPointPosition);

            enemyIsOnTheBattlefield = entityHandler.AliveVikings.Count > absenceVikings;
        }

        public void Sense()
        {

        }

        public void Think()
        {
            enemyIsOnTheBattlefield = entityHandler.AliveVikings.Count > absenceVikings;
        }
        #endregion

        #region Methods
        private void CheckForTargets()
        {
            if (enemyIsOnTheBattlefield)
            {
                knightBehaviour.enemy = knightBehaviour.SetTarget(EntityType.Viking);

                knightBehaviour.State = KnightState.Chase;
            }
        }
        #endregion
    }
}