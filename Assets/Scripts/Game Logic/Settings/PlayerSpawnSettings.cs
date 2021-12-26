using UnityEngine;

namespace GameLogic.Settings
{
    [CreateAssetMenu(fileName = fileName, menuName = menuName, order = order)]
    public sealed class PlayerSpawnSettings : ScriptableObject
    {
        #region Create asset menu constants
        private const string fileName = "Player Spawn Settings", menuName = "Game settings/Player Spawn settings";
        private const int order = 1;
        #endregion

        #region Parameters
#pragma warning disable IDE0044
        [Header("Shooter spawn settings")]
        [SerializeField] private GameObject shooter = null;
        [SerializeField, Min(0)] private int shooterSpawnCount = 2;

        [Header("Swordsman spawn settings")]
        [SerializeField] private GameObject swordsman = null;
        [SerializeField, Min(0)] private int swordsmanSpawnCount = 3;

        [Header("Two-Handed Swordsman spawn settings")]
        [SerializeField] private GameObject twoHandedSwordsman = null;
        [SerializeField, Min(0)] private int twoHandedSwordsmanSpawnCount = 1;

        [Header("Rally point settings")]
        [SerializeField, Min(1)] private int countInLine = 5;
        [SerializeField, Min(0.0f)] private float horizontalInterval = 1.0f;
        [SerializeField, Min(0.0f)] private float verticalInterval = 1.0f;
#pragma warning restore IDE0044
        #endregion

        #region Properties
        public GameObject Shooter => shooter;
        public GameObject Swordsman => swordsman;
        public GameObject TwoHandedSwordsman => twoHandedSwordsman;

        public int ShooterSpawnCount => shooterSpawnCount;
        public int SwordsmanSpawnCount => swordsmanSpawnCount;
        public int TwoHandedSwordsmanSpawnCount => twoHandedSwordsmanSpawnCount;
        public int CountInLine => countInLine;

        public float HorizontalInterval => horizontalInterval;
        public float VerticalInterval => verticalInterval;
        #endregion
    }
}