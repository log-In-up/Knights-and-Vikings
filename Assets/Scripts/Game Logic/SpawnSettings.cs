using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = menuName, order = order)]
public sealed class SpawnSettings : ScriptableObject
{
    #region Create asset menu constants
    private const string fileName = "Spawn Settings";
    private const string menuName = "Scriptable Objects/Spawn Settings";
    private const int order = 1;
    #endregion

    #region Parameters
#pragma warning disable IDE0044
    [Header("Shooter spawn settings")]
    [SerializeField] private GameObject shooter = null;
    [SerializeField] private int shooterSpawnCount = 2;

    [Header("Swordsman spawn settings")]
    [SerializeField] private GameObject swordsman = null;
    [SerializeField] private int swordsmanSpawnCount = 3;

    [Header("Two-Handed Swordsman spawn settings")]
    [SerializeField] private GameObject twoHandedSwordsman = null;
    [SerializeField] private int twoHandedSwordsmanSpawnCount = 1;
#pragma warning restore IDE0044
    #endregion

    #region Properties
    public GameObject Shooter => shooter;
    public GameObject Swordsman => swordsman;
    public GameObject TwoHandedSwordsman => twoHandedSwordsman;

    public int ShooterSpawnCount => shooterSpawnCount;
    public int SwordsmanSpawnCount => swordsmanSpawnCount;
    public int TwoHandedSwordsmanSpawnCount => twoHandedSwordsmanSpawnCount;
    #endregion
}