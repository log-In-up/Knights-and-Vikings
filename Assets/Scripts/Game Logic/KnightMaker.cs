using UnityEngine;

[DisallowMultipleComponent]
public sealed class KnightMaker : MonoBehaviour
{
    #region Parameters
    [SerializeField] private SpawnSettings spawnSettings = null;

    [SerializeField] private Transform[] spawnPoints = null;
    #endregion

    #region Custom methods
    public void CreateShooters()
    {
        CreateEntities(spawnSettings.Shooter, spawnSettings.ShooterSpawnCount);
    }

    public void CreateSwordsmen()
    {
        CreateEntities(spawnSettings.Swordsman, spawnSettings.SwordsmanSpawnCount);
    }

    public void CreateTwoHandedSwordsmen()
    {
        CreateEntities(spawnSettings.TwoHandedSwordsman, spawnSettings.TwoHandedSwordsmanSpawnCount);
    }

    private void CreateEntities(GameObject entity, int count)
    {
        int spawnIndex = 0;

        for (int index = 0; index < count; index++)
        {
            Transform spawnPosition = spawnPoints[spawnIndex];
            spawnIndex = ++spawnIndex % spawnPoints.Length;

            Instantiate(entity, spawnPosition.position, spawnPosition.rotation);
        }
    }
    #endregion
}