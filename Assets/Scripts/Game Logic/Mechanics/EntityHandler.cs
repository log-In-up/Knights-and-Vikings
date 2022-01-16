using Entity.Behaviours;
using Entity.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Mechanics
{
    [RequireComponent(typeof(BattleCurator))]
    public sealed class EntityHandler : MonoBehaviour
    {
        #region Editor parameters
        [SerializeField] private KnightRallyPoint rallyPoint = null;
        #endregion

        #region Parameters
        private BattleCurator curator = null;

        private List<KnightBehaviour> aliveKnights = null, deadKnights = null;
        private List<VikingBehaviour> aliveVikings = null, deadVikings = null;
        #endregion

        #region Properties
        public List<KnightBehaviour> AliveKnights => aliveKnights;
        public List<VikingBehaviour> AliveVikings => aliveVikings;
        #endregion

        private void Awake()
        {
            curator = GetComponent<BattleCurator>();

            aliveKnights = new List<KnightBehaviour>();
            aliveVikings = new List<VikingBehaviour>();
            deadKnights = new List<KnightBehaviour>();
            deadVikings = new List<VikingBehaviour>();
        }

        #region Methods
        public List<EntityBehaviour> GetListOfSpecificEntities(EntitySubtype subtype, EntityType type)
        {
            List<EntityBehaviour> entities = new(), necessaryEntities = new();

            switch (type)
            {
                case EntityType.Knight:
                    entities.AddRange(aliveKnights);
                    break;
                case EntityType.Viking:
                    entities.AddRange(aliveVikings);
                    break;
                default:
                    Debug.LogError($"Entity of type {type} does not supported.");
                    break;
            }

            foreach (EntityBehaviour entity in entities)
            {
                if (entity.EntitySubtype == subtype)
                {
                    necessaryEntities.Add(entity);
                }
            }

            return necessaryEntities;
        }

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

            curator.WaveEnemiesCount += 1;            
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

            curator.WaveEnemiesCount -= 1;
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
    }
}