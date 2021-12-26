using Entity.Behaviours;
using UnityEngine;

namespace GameLogic.Mechanics
{
    public sealed class KnightMaker : EntityMaker
    {
        #region Parameters

        #endregion

        public KnightMaker(BattleCurator curator, Transform[] spawnPoints) : base(curator, spawnPoints)
        {

        }

        #region Custom methods
        internal void CreateKnights(GameObject knight, int count)
        {
            int spawnIndex = 0;

            for (int index = 0; index < count; index++)
            {
                Transform spawnPosition = spawnPoints[spawnIndex];
                spawnIndex = ++spawnIndex % spawnPoints.Length;

                GameObject knightEntity = Object.Instantiate(knight, spawnPosition.position, spawnPosition.rotation);

                if (knightEntity.TryGetComponent(out KnightBehaviour knightBehaviour))
                {
                    knightBehaviour.BattleCurator = curator;

                    curator.EntityHandler.AddAliveKnight(knightBehaviour);
                }
                else
                {
                    Debug.LogError($"Object ({knight.name}) does not contain KnightBehaviour script.");
                }
            }
        }
        #endregion
    }
}