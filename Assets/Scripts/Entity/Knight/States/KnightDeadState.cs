using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Interfaces;
using GameLogic;

namespace Entity.States
{
    public sealed class KnightDeadState : IEntityState
    {
        #region Parameters
        private readonly KnightBehaviour knightBehaviour = null;
        private readonly BattleCurator curator = null;

        private readonly float healthPoints;
        #endregion

        public KnightDeadState(KnightBehaviour knightBehaviour, EntityCharacteristics entityCharacteristics, BattleCurator curator)
        {
            this.knightBehaviour = knightBehaviour;
            this.curator = curator;

            healthPoints = entityCharacteristics.HealthPoints;
        }

        #region Interface implementation
        public void Act()
        {

        }

        public void Close()
        {
            curator.EntityHandler.AddAliveKnight(knightBehaviour);

            knightBehaviour.HealthPoints = healthPoints;
        }

        public void Initialize()
        {
            curator.EntityHandler.AddDeadKnight(knightBehaviour);
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