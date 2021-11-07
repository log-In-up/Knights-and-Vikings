using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = menuName, order = order)]
public sealed class EnemySpawnSettings : ScriptableObject
{
    #region Create asset menu constants
    private const string fileName = "Enemy Spawn Settings", menuName = "Game settings/Enemy Spawn settings";
    private const int order = 1;
    #endregion

    #region Parameters
    [Header("Shooter spawn settings")]
    [SerializeField] private GameObject shooter = null;

    [Header("Swordsman spawn settings")]
    [SerializeField] private GameObject swordsman = null;

    [Header("Two-Handed Swordsman spawn settings")]
    [SerializeField] private GameObject twoHandedSwordsman = null;

    [Header("Common settings")]
    [SerializeField, Min(1)] private int simultaneousCount = 7;
    [SerializeField, Min(0.0f)] private float spawnDelay = 1.5f;
    #endregion

    #region Properties
    public GameObject Shooter => shooter;
    public GameObject Swordsman => swordsman;
    public GameObject TwoHandedSwordsman => twoHandedSwordsman;

    public float SpawnDelay => spawnDelay;

    public int SimultaneousCount => simultaneousCount;
    #endregion
}