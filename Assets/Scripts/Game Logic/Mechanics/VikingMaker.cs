using Entity.Behaviours;
using UnityEngine;

namespace GameLogic.Mechanics
{
    public sealed class VikingMaker : EntityMaker
    {
        #region Parameters
        private readonly PlayerBase playerBase = null;

        private const int firstSpawnPoint = 0;
        #endregion

        public VikingMaker(BattleCurator curator, PlayerBase playerBase, Transform[] spawnPoints) : base(curator, spawnPoints)
        {
            this.playerBase = playerBase;
        }

        #region Methods
        public void CreateViking(GameObject viking)
        {
            int randomPoint = Random.Range(firstSpawnPoint, spawnPoints.Length);

            GameObject vikingEntity = Object.Instantiate(viking, spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

            if (vikingEntity.TryGetComponent(out VikingBehaviour vikingBehaviour))
            {
                vikingBehaviour.BattleCurator = curator;
                vikingBehaviour.PlayerBase = playerBase;

                curator.EntityHandler.AddAliveViking(vikingBehaviour);
            }
            else
            {
                Debug.LogError($"Object ({viking.name}) does not contain VikingBehaviour script.");
            }
        }
        #endregion
    }
}