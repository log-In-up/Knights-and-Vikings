using Entity.Behaviours;
using Entity.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.Mechanics
{
    public sealed class EntityHandler
    {
        #region Parameters
        private readonly List<KnightBehaviour> aliveKnights = null, deadKnights = null;
        private readonly List<VikingBehaviour> aliveVikings = null, deadVikings = null;

        private readonly KnightRallyPoint rallyPoint = null;
        private readonly Slider slider = null;
        #endregion

        #region Properties
        public List<KnightBehaviour> AliveKnights => aliveKnights;
        public List<VikingBehaviour> AliveVikings => aliveVikings;
        #endregion

        public EntityHandler(KnightRallyPoint rallyPoint, Slider slider)
        {
            this.rallyPoint = rallyPoint;
            this.slider = slider;

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

            slider.value += 1;
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

            slider.value -= 1;
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