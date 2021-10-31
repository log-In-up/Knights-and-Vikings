using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public sealed class BattleCurator : MonoBehaviour
{
    #region Parameters
    [Header("Knights spawn settings")]
    [SerializeField] private PlayerSpawnSettings vikingSpawnSettings = null;
    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private RallyPointSettings rallyPointSettings = null;
    [SerializeField] private Transform knightRallyPoint = null;

    [Header("Vikings spawn settings")]
    [SerializeField] private EnemySpawnSettings enemySpawnSettings = null;

    private bool inBattle;

    private KnightMaker knightMaker = null;
    private KnightRallyPoint rallyPoint = null;

    private List<KnightBehaviour> aliveKnights = null, deadKnights = null;
    private List<VikingBehaviour> aliveVikings = null, deadVikings = null;
    #endregion

    #region Properties
    public bool InBattle => inBattle;
    #endregion

    #region MonoBehaviour API
    private void Start()
    {
        inBattle = false;

        knightMaker = new KnightMaker(this, spawnPoints);
        rallyPoint = new KnightRallyPoint(knightRallyPoint, rallyPointSettings);

        aliveKnights = new List<KnightBehaviour>();
        aliveVikings = new List<VikingBehaviour>();
        deadKnights = new List<KnightBehaviour>();
        deadVikings = new List<VikingBehaviour>();
    }
    #endregion

    #region Custom methods
    internal void AddAliveKnight(KnightBehaviour knight)
    {
        if (deadKnights.Contains(knight))
        {
            deadKnights.Remove(knight);
        }
        aliveKnights.Add(knight);

        SetRallyPoints();
    }

    internal void AddAliveViking(VikingBehaviour viking)
    {
        if (deadVikings.Contains(viking))
        {
            deadVikings.Remove(viking);
        }
        aliveVikings.Add(viking);
    }

    internal void AddDeadKnight(KnightBehaviour knight)
    {
        aliveKnights.Remove(knight);
        deadKnights.Add(knight);

        SetRallyPoints();
    }

    internal void AddDeadViking(VikingBehaviour viking)
    {
        aliveVikings.Remove(viking);
        deadVikings.Add(viking);
    }

    private void SetRallyPoints()
    {
        int count = aliveKnights.Count;

        Vector3[] positions = rallyPoint.GetPointsForPlacement(count);

        for (int index = 0; index < count; index++)
        {
            aliveKnights[index].rallyPointPosition = positions[index];
        }
    }
    #endregion

    #region Button handlers
    public void CreateShooters()
    {
        knightMaker.CreateEntities(vikingSpawnSettings.Shooter, vikingSpawnSettings.ShooterSpawnCount);
    }

    public void CreateSwordsmen()
    {
        knightMaker.CreateEntities(vikingSpawnSettings.Swordsman, vikingSpawnSettings.SwordsmanSpawnCount);
    }

    public void CreateTwoHandedSwordsmen()
    {
        knightMaker.CreateEntities(vikingSpawnSettings.TwoHandedSwordsman, vikingSpawnSettings.TwoHandedSwordsmanSpawnCount);
    }
    #endregion
}
