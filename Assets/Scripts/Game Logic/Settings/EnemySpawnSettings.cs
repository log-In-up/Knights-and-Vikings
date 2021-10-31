using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = menuName, order = order)]
public sealed class EnemySpawnSettings : ScriptableObject
{
    #region Create asset menu constants
    private const string fileName = "Enemy Spawn Settings", menuName = "Game settings/Enemy Spawn settings";
    private const int order = 1;
    #endregion

    #region Parameters
    [SerializeField] private GameObject shooter = null;
    [SerializeField] private GameObject swordsman = null;
    [SerializeField] private GameObject twoHandedSwordsman = null;
    #endregion

    #region Properties
    public GameObject Shooter => shooter;
    public GameObject Swordsman => swordsman;
    public GameObject TwoHandedSwordsman => twoHandedSwordsman;    
    #endregion
}
