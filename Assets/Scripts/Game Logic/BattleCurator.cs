using TMPro;
using UnityEngine.UI;
using UnityEngine;

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
    [SerializeField] private Slider waveState = null;
    [SerializeField] private TextMeshProUGUI waveText = null;
    #endregion

    #region Parameters
    private bool inBattle;
    private float nextSpawnTime;
    private int currentWave, levelTokens;

    private EntityHandler entityHandler = null;
    private KnightMaker knightMaker = null;
    private KnightRallyPoint rallyPoint = null;
    private VikingMaker vikingMaker = null;

    private const int lackOfTokens = 0, lackOfOpponents = 0, firstWave = 0;
    #endregion

    #region Properties
    public bool InBattle => inBattle;

    public int CurrentWave
    {
        get => currentWave;
        set
        {
            if (value >= firstWave)
            {
                currentWave = value;

                float newWave = Mathf.Sqrt(currentWave);
                levelTokens = (int)Mathf.Round(newWave);

                waveState.maxValue = levelTokens;
                waveState.value = levelTokens;

                waveText.text = $"Wave: {currentWave}";
            }
        }
    }

    public EntityHandler EntityHandler => entityHandler;
    #endregion

    #region MonoBehaviour API
    private void Awake()
    {
        knightMaker = new KnightMaker(this, knightSpawnPoints);
        vikingMaker = new VikingMaker(this, vikingSpawnPoints);

        rallyPoint = new KnightRallyPoint(knightRallyPoint, knightSpawnSettings);
        entityHandler = new EntityHandler(rallyPoint);
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
    #endregion

    #region Methods
    private void TrackingAndCreatingVikings()
    {
        if (entityHandler.AliveVikings.Count < enemySpawnSettings.SimultaneousCount)
        {
            if (levelTokens > lackOfTokens && Time.time > nextSpawnTime)
            {
                vikingMaker.SpawnViking(enemySpawnSettings.Shooter);

                levelTokens--;
                nextSpawnTime = Time.time + enemySpawnSettings.SpawnDelay;
            }
        }
    }
    #endregion

    #region Button handlers
    public void CreateShooters()
    {
        knightMaker.CreateEntities(knightSpawnSettings.Shooter, knightSpawnSettings.ShooterSpawnCount);
    }

    public void CreateSwordsmen()
    {
        knightMaker.CreateEntities(knightSpawnSettings.Swordsman, knightSpawnSettings.SwordsmanSpawnCount);
    }

    public void CreateTwoHandedSwordsmen()
    {
        knightMaker.CreateEntities(knightSpawnSettings.TwoHandedSwordsman, knightSpawnSettings.TwoHandedSwordsmanSpawnCount);
    }

    public void StartBattle()
    {
        inBattle = true;
    }
    #endregion
}