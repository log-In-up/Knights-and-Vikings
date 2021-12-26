using Entity.Behaviours;
using Entity.Enums;
using Entity.Interfaces;

namespace Entity.States
{
    public sealed class KnightReturnState : IEntityState
    {
        #region Parameters
        private bool enemyIsOnTheBattlefield;

        private readonly KnightBehaviour knightBehaviour = null;

        private const int absenceVikings = 0;
        #endregion

        public KnightReturnState(KnightBehaviour knightBehaviour)
        {
            this.knightBehaviour = knightBehaviour;
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

            enemyIsOnTheBattlefield = knightBehaviour.BattleCurator.EntityHandler.AliveVikings.Count > absenceVikings;
        }

        public void Sense()
        {

        }

        public void Think()
        {
            enemyIsOnTheBattlefield = knightBehaviour.BattleCurator.EntityHandler.AliveVikings.Count > absenceVikings;
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