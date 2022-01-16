using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Interfaces;
using GameLogic.Mechanics;

namespace Entity.States
{
    public sealed class VikingDeadState : IEntityState
    {
        #region Parameters
        private readonly float healthPoints;

        private readonly VikingBehaviour vikingBehaviour = null;
        private readonly EntityHandler entityHandler = null;
        #endregion

        public VikingDeadState(VikingBehaviour vikingBehaviour,  EntityCharacteristics entityCharacteristics, EntityHandler entityHandler)
        {
            this.vikingBehaviour = vikingBehaviour;
            this.entityHandler = entityHandler;

            healthPoints = entityCharacteristics.HealthPoints;
        }

        #region Interface implementation
        public void Act()
        {

        }

        public void Close()
        {
            entityHandler.AddAliveViking(vikingBehaviour);

            vikingBehaviour.HealthPoints = healthPoints;
        }

        public void Initialize()
        {
            entityHandler.AddDeadViking(vikingBehaviour);
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