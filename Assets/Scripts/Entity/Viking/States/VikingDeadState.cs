using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Interfaces;
using GameLogic;

namespace Entity.States
{
    public sealed class VikingDeadState : IEntityState
    {
        #region Parameters
        private readonly float healthPoints;

        private readonly VikingBehaviour vikingBehaviour = null;
        private readonly BattleCurator curator = null;
        #endregion

        public VikingDeadState(VikingBehaviour vikingBehaviour, EntityCharacteristics entityCharacteristics, BattleCurator curator)
        {
            this.vikingBehaviour = vikingBehaviour;
            this.curator = curator;

            healthPoints = entityCharacteristics.HealthPoints;
        }

        #region Interface implementation
        public void Act()
        {

        }

        public void Close()
        {
            curator.EntityHandler.AddAliveViking(vikingBehaviour);

            vikingBehaviour.HealthPoints = healthPoints;
        }

        public void Initialize()
        {
            curator.EntityHandler.AddDeadViking(vikingBehaviour);
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