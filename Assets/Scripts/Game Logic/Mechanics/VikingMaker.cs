using Entity.Behaviours;
using GameLogic.Settings;
using UnityEngine;
using Zenject;

namespace GameLogic.Mechanics
{
    public sealed class VikingMaker : EntityMaker
    {
        #region Parameters
        private float nextSpawnTime;

        private EnemySpawnSettings enemySpawnSettings = null;

        private const int firstSpawnPoint = 0;
        private const int lackOfTokens = 0;
        #endregion

        #region Zenject
        [Inject]
        private DiContainer diContainer = null;

        [Inject]
        private void Constructor(EnemySpawnSettings enemySpawnSettings)
        {
            this.enemySpawnSettings = enemySpawnSettings;
        }
        #endregion

        #region MonoBehaviour API
        private void Start()
        {
            nextSpawnTime = Time.time;
        }

        private void Update()
        {
            if (curator.InBattle)
            {
                TrackingAndCreatingVikings();
            }
        }
        #endregion

        #region Methods
        private void TrackingAndCreatingVikings()
        {
            if (entityHandler.AliveVikings.Count < enemySpawnSettings.SimultaneousCount)
            {
                if (curator.LevelTokens > lackOfTokens && Time.time > nextSpawnTime)
                {
                    CreateViking(enemySpawnSettings.Shooter);

                    curator.LevelTokens--;

                    nextSpawnTime = Time.time + enemySpawnSettings.SpawnDelay;
                }
            }
        }

        public void CreateViking(GameObject viking)
        {
            int randomPoint = Random.Range(firstSpawnPoint, spawnPoints.Length);

            GameObject vikingEntity = diContainer.InstantiatePrefab(viking, spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation, null);

            if (vikingEntity.TryGetComponent(out VikingBehaviour vikingBehaviour))
            {
                entityHandler.AddAliveViking(vikingBehaviour);
            }
            else
            {
                Debug.LogError($"Object ({viking.name}) does not contain VikingBehaviour script.");
            }
        }
        #endregion
    }
}