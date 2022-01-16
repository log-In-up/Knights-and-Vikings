using Entity.Behaviours;
using GameLogic.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.Mechanics
{
    public sealed class KnightMaker : EntityMaker
    {
        #region Editor parameters
        [Header("Knights spawn settings")]
        [SerializeField] private PlayerSpawnSettings knightSpawnSettings = null;

        [Header("Control buttons")]
        [SerializeField] private Button spawnShooter = null;
        [SerializeField] private Button spawnSwordsman = null;
        [SerializeField] private Button spawnTwoHandedSwordsman = null;
        #endregion

        #region Parameters

        #endregion

        #region MonoBehaviour API
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            spawnShooter.onClick.AddListener(CreateShooters);
            spawnSwordsman.onClick.AddListener(CreateSwordsmen);
            spawnTwoHandedSwordsman.onClick.AddListener(CreateTwoHandedSwordsmen);
        }

        private void OnDisable()
        {
            spawnShooter.onClick.RemoveListener(CreateShooters);
            spawnSwordsman.onClick.RemoveListener(CreateSwordsmen);
            spawnTwoHandedSwordsman.onClick.RemoveListener(CreateTwoHandedSwordsmen);
        }
        #endregion

        #region Custom methods
        private void CreateKnights(GameObject knight, int count)
        {
            int spawnIndex = 0;

            for (int index = 0; index < count; index++)
            {
                Transform spawnPosition = spawnPoints[spawnIndex];
                spawnIndex = ++spawnIndex % spawnPoints.Length;

                GameObject knightEntity = Instantiate(knight, spawnPosition.position, spawnPosition.rotation);

                if (knightEntity.TryGetComponent(out KnightBehaviour knightBehaviour))
                {
                    knightBehaviour.EntityHandler = entityHandler;
                    knightBehaviour.BattleCurator = curator;

                    entityHandler.AddAliveKnight(knightBehaviour);
                }
                else
                {
                    Debug.LogError($"Object ({knight.name}) does not contain KnightBehaviour script.");
                }
            }
        }
        #endregion

        #region Button handlers
        private void CreateShooters()
        {
            CreateKnights(knightSpawnSettings.Shooter, knightSpawnSettings.ShooterSpawnCount);
        }

        private void CreateSwordsmen()
        {
            CreateKnights(knightSpawnSettings.Swordsman, knightSpawnSettings.SwordsmanSpawnCount);
        }

        private void CreateTwoHandedSwordsmen()
        {
            CreateKnights(knightSpawnSettings.TwoHandedSwordsman, knightSpawnSettings.TwoHandedSwordsmanSpawnCount);
        }
        #endregion
    }
}