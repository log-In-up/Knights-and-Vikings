using UnityEngine;

public sealed class KnightMaker
{
    #region Parameters
    private readonly BattleCurator curator = null;
    private readonly Transform[] spawnPoints = null;
    #endregion

    public KnightMaker(BattleCurator curator, Transform[] spawnPoints)
    {
        this.curator = curator;
        this.spawnPoints = spawnPoints;
    }

    #region Custom methods
    internal void CreateEntities(GameObject entity, int count)
    {
        int spawnIndex = 0;

        for (int index = 0; index < count; index++)
        {
            Transform spawnPosition = spawnPoints[spawnIndex];
            spawnIndex = ++spawnIndex % spawnPoints.Length;

            GameObject knight = Object.Instantiate(entity, spawnPosition.position, spawnPosition.rotation);

            if (knight.TryGetComponent(out KnightBehaviour knightBehaviour))
            {
                knightBehaviour.SetCurator(curator);
                curator.EntityHandler.AddAliveKnight(knightBehaviour);
            }
        }
    }
    #endregion
}