using Entity.Behaviours;
using GameLogic.Settings;
using UnityEngine;

namespace GameLogic.Mechanics
{
    [RequireComponent(typeof(BattleCurator))]
    public sealed class VikingMaker : EntityMaker
    {
        #region Editor parameters
        [Header("Vikings spawn settings")]
        [SerializeField] private PlayerBase playerBase = null;
        [SerializeField] private EnemySpawnSettings enemySpawnSettings = null;
        #endregion

        #region Parameters
        private float nextSpawnTime;

        private const int firstSpawnPoint = 0;
        private const int lackOfTokens = 0;
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            nextSpawnTime = Time.time;
        }

        private void Update()
        {
            if(curator.InBattle)
            {
                TrackingAndCreatingVikings();
            }
        }

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

            GameObject vikingEntity = Instantiate(viking, spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

            if (vikingEntity.TryGetComponent(out VikingBehaviour vikingBehaviour))
            {
                vikingBehaviour.EntityHandler = entityHandler;
                vikingBehaviour.BattleCurator = curator;
                vikingBehaviour.PlayerBase = playerBase;

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