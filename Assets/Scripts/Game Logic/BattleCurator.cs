using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class BattleCurator : MonoBehaviour
{
    #region Parameters
    [Header("Spawn settings")]
    [SerializeField] private SpawnSettings spawnSettings = null;
    [SerializeField] private Transform[] spawnPoints = null;

    private bool inBattle;
    private KnightMaker knightMaker = null;

    private List<KnightBehaviour> aliveKnights = null, deadKnights = null;
    private List<VikingBehaviour> aliveVikings = null, deadVikings = null;
    #endregion

    #region Properties
    public bool InBattle
    {
        get => inBattle;
    }
    #endregion

    #region MonoBehaviour API
    private void Start()
    {
        inBattle = true;
        knightMaker = new KnightMaker(this, spawnPoints);

        aliveKnights = new List<KnightBehaviour>();
        aliveVikings = new List<VikingBehaviour>();
        deadKnights = new List<KnightBehaviour>();
        deadVikings = new List<VikingBehaviour>();
    }
    #endregion

    #region Custom methods
    internal void AddAliveKnight(KnightBehaviour knight) => aliveKnights.Add(knight);

    internal void AddAliveViking(VikingBehaviour viking) => aliveVikings.Add(viking);

    internal void AddDeadKnight(KnightBehaviour knight)
    {
        aliveKnights.Remove(knight);
        deadKnights.Add(knight);
    }

    internal void AddDeadViking(VikingBehaviour viking)
    {
        aliveVikings.Remove(viking);
        deadVikings.Add(viking);
    }
    #endregion

    #region Button handlers
    public void CreateShooters()
    {
        knightMaker.CreateEntities(spawnSettings.Shooter, spawnSettings.ShooterSpawnCount);
    }

    public void CreateSwordsmen()
    {
        knightMaker.CreateEntities(spawnSettings.Swordsman, spawnSettings.SwordsmanSpawnCount);
    }

    public void CreateTwoHandedSwordsmen()
    {
        knightMaker.CreateEntities(spawnSettings.TwoHandedSwordsman, spawnSettings.TwoHandedSwordsmanSpawnCount);
    }
    #endregion
}