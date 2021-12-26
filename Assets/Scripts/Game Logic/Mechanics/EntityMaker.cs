using UnityEngine;

namespace GameLogic.Mechanics
{
    public class EntityMaker
    {
        #region Parameters
        private protected readonly BattleCurator curator = null;
        private protected readonly Transform[] spawnPoints = null;
        #endregion

        public EntityMaker(BattleCurator curator, Transform[] spawnPoints)
        {
            this.curator = curator;
            this.spawnPoints = spawnPoints;
        }
    }
}