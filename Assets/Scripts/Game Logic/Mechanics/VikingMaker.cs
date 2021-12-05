using UnityEngine;

public sealed class VikingMaker
{
    #region Parameters
    private readonly BattleCurator curator = null;
    private readonly Transform[] spawnPoints = null;

    private const int firstSpawnPoint = 0;
    #endregion

    public VikingMaker(BattleCurator curator, Transform[] spawnPoints)
    {
        this.curator = curator;
        this.spawnPoints = spawnPoints;
    }

    #region Methods
    public void SpawnViking(GameObject viking)
    {
        int randomPoint = Random.Range(firstSpawnPoint, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomPoint];

        GameObject vikingEntity = Object.Instantiate(viking, spawnPoint.position, spawnPoint.rotation);

        if (vikingEntity.TryGetComponent(out VikingBehaviour vikingBehaviour))
        {
            vikingBehaviour.BattleCurator = curator;
            curator.EntityHandler.AddAliveViking(vikingBehaviour);
        }
    }
    #endregion
}