using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Interfaces;
using GameLogic.Mechanics;

namespace Entity.States
{
    public sealed class KnightDeadState : IEntityState
    {
        #region Parameters
        private readonly float healthPoints;

        private readonly KnightBehaviour knightBehaviour = null;
        private readonly EntityHandler entityHandler = null;
        #endregion

        public KnightDeadState(KnightBehaviour knightBehaviour, EntityCharacteristics entityCharacteristics, EntityHandler entityHandler)
        {
            this.knightBehaviour = knightBehaviour;
            this.entityHandler = entityHandler;

            healthPoints = entityCharacteristics.HealthPoints;
        }

        #region Interface implementation
        public void Act()
        {

        }

        public void Close()
        {
            entityHandler.AddAliveKnight(knightBehaviour);

            knightBehaviour.HealthPoints = healthPoints;
        }

        public void Initialize()
        {
            entityHandler.AddDeadKnight(knightBehaviour);
        }

        public void Sense()
        {

        }

        public void Think()
        {

        }
        #endregion

        #region Methods

        #endregion
    }
}