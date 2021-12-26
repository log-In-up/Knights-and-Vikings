using GameLogic.Mechanics;
using GameLogic.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    [DisallowMultipleComponent]
    public sealed class BattleCurator : MonoBehaviour
    {
        #region Editor parameters
        [Header("Knights spawn settings")]
        [SerializeField] private PlayerSpawnSettings knightSpawnSettings = null;
        [SerializeField] private Transform knightRallyPoint = null;
        [SerializeField] private Transform[] knightSpawnPoints = null;

        [Header("Vikings spawn settings")]
        [SerializeField] private EnemySpawnSettings enemySpawnSettings = null;
        [SerializeField] private Transform[] vikingSpawnPoints = null;

        [Header("Other components")]
        [SerializeField] private PlayerBase playerBase = null;
        [SerializeField] private Slider waveState = null;
        [SerializeField] private TextMeshProUGUI waveText = null;

        [Header("Control buttons")]
        [SerializeField] private Button spawnShooter = null;
        [SerializeField] private Button spawnSwordsman = null;
        [SerializeField] private Button spawnTwoHandedSwordsman = null;
        [SerializeField] private Button startGame = null;
        #endregion

        #region Parameters
        private bool inBattle;
        private float nextSpawnTime;
        private uint currentWave, levelTokens;

        private EntityHandler entityHandler = null;
        private KnightMaker knightMaker = null;
        private KnightRallyPoint rallyPoint = null;
        private VikingMaker vikingMaker = null;

        private const int lackOfTokens = 0, lackOfOpponents = 0;
        #endregion

        #region Properties
        public bool InBattle { get => inBattle; set => inBattle = value; }

        public uint CurrentWave
        {
            get => currentWave;
            set
            {
                currentWave = value;

                float newWave = Mathf.Sqrt(currentWave);
                levelTokens = (uint)Mathf.Round(newWave);

                waveState.maxValue = levelTokens;
                waveState.value = levelTokens;

                waveText.text = $"Wave: {currentWave}";
            }
        }

        public EntityHandler EntityHandler => entityHandler;
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            knightMaker = new KnightMaker(this, knightSpawnPoints);
            vikingMaker = new VikingMaker(this, playerBase, vikingSpawnPoints);

            rallyPoint = new KnightRallyPoint(knightRallyPoint, knightSpawnSettings);
            entityHandler = new EntityHandler(rallyPoint, waveState);
        }

        private void OnEnable()
        {
            spawnShooter.onClick.AddListener(CreateShooters);
            spawnSwordsman.onClick.AddListener(CreateSwordsmen);
            spawnTwoHandedSwordsman.onClick.AddListener(CreateTwoHandedSwordsmen);

            startGame.onClick.AddListener(StartBattle);
        }

        private void Start()
        {
            nextSpawnTime = Time.time;

            waveState.wholeNumbers = true;
            waveState.minValue = lackOfOpponents;

            CurrentWave = 3;

            inBattle = false;
        }

        private void Update()
        {
            if (inBattle == true)
            {
                TrackingAndCreatingVikings();
            }
        }

        private void OnDisable()
        {
            spawnShooter.onClick.RemoveListener(CreateShooters);
            spawnSwordsman.onClick.RemoveListener(CreateSwordsmen);
            spawnTwoHandedSwordsman.onClick.RemoveListener(CreateTwoHandedSwordsmen);

            startGame.onClick.RemoveListener(StartBattle);
        }
        #endregion

        #region Methods
        private void TrackingAndCreatingVikings()
        {
            if (entityHandler.AliveVikings.Count < enemySpawnSettings.SimultaneousCount)
            {
                if (levelTokens > lackOfTokens && Time.time > nextSpawnTime)
                {
                    vikingMaker.CreateViking(enemySpawnSettings.Shooter);

                    levelTokens--;

                    nextSpawnTime = Time.time + enemySpawnSettings.SpawnDelay;
                }
            }
        }
        #endregion

        #region Button handlers
        private void CreateShooters()
        {
            knightMaker.CreateKnights(knightSpawnSettings.Shooter, knightSpawnSettings.ShooterSpawnCount);
        }

        private void CreateSwordsmen()
        {
            knightMaker.CreateKnights(knightSpawnSettings.Swordsman, knightSpawnSettings.SwordsmanSpawnCount);
        }

        private void CreateTwoHandedSwordsmen()
        {
            knightMaker.CreateKnights(knightSpawnSettings.TwoHandedSwordsman, knightSpawnSettings.TwoHandedSwordsmanSpawnCount);
        }

        private void StartBattle()
        {
            inBattle = true;
        }
        #endregion
    }
}