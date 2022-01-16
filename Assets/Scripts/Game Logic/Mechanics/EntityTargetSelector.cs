using Entity.Behaviours;
using Entity.Enums;
using System.Collections.Generic;
using Utility;

namespace GameLogic.Mechanics
{
    public sealed class EntityTargetSelector
    {
        #region Parameters
        private readonly EntityBehaviour entityBehaviour = null;
        private readonly EntityHandler entityHandler = null;

        private const int noEnemies = 0;
        #endregion

        public EntityTargetSelector(EntityBehaviour entityBehaviour, EntityHandler entityHandler)
        {
            this.entityBehaviour = entityBehaviour;
            this.entityHandler = entityHandler;

        }

        #region Methods    
        public EntityBehaviour SetTarget(EntityType entityType)
        {
            List<EntityBehaviour> shooter = entityHandler.GetListOfSpecificEntities(EntitySubtype.Shooter, entityType);
            List<EntityBehaviour> swordsmans = entityHandler.GetListOfSpecificEntities(EntitySubtype.Swordsman, entityType);
            List<EntityBehaviour> twoHandedSwordsman = entityHandler.GetListOfSpecificEntities(EntitySubtype.TwoHandedSwordsman, entityType);

            List<EntityBehaviour> listOfEntities = entityBehaviour.EntitySubtype switch
            {
                EntitySubtype.Shooter => SetTargets(EntitySubtype.Shooter, shooter, swordsmans, twoHandedSwordsman),
                EntitySubtype.Swordsman => SetTargets(EntitySubtype.Swordsman, shooter, swordsmans, twoHandedSwordsman),
                EntitySubtype.TwoHandedSwordsman => SetTargets(EntitySubtype.TwoHandedSwordsman, shooter, swordsmans, twoHandedSwordsman),
                _ => throw new System.NotImplementedException($"Entity subtype {entityBehaviour.EntitySubtype} does not implemented.")
            };

            return GetClosestEntity(listOfEntities);
        }

        private EntityBehaviour GetClosestEntity(List<EntityBehaviour> entities)
        {
            KdTree<EntityBehaviour> kdEntities = new();
            kdEntities.AddAll(entities);

            return kdEntities.FindClosest(entityBehaviour.transform.position);
        }

        private List<EntityBehaviour> SetTargets(EntitySubtype subtype, List<EntityBehaviour> shooter, List<EntityBehaviour> swordsmans, List<EntityBehaviour> twoHandedSwordsman)
        {
            return subtype switch
            {
                EntitySubtype.Shooter => GetTargetsForShooter(shooter, swordsmans, twoHandedSwordsman),
                EntitySubtype.Swordsman => GetTargetsForSwordsman(shooter, swordsmans, twoHandedSwordsman),
                EntitySubtype.TwoHandedSwordsman => GetTargetsForTwoHandedSwordsman(shooter, swordsmans, twoHandedSwordsman),
                _ => throw new System.NotImplementedException($"Entity subtype {subtype} does not implemented.")
            };
        }

        private List<EntityBehaviour> GetTargetsForShooter(List<EntityBehaviour> shooter, List<EntityBehaviour> swordsmans, List<EntityBehaviour> twoHandedSwordsman)
        {
            if (twoHandedSwordsman.Count > noEnemies)
            {
                return twoHandedSwordsman;
            }
            else if (swordsmans.Count > noEnemies)
            {
                return swordsmans;
            }
            else
            {
                return shooter;
            }
        }

        private List<EntityBehaviour> GetTargetsForSwordsman(List<EntityBehaviour> shooter, List<EntityBehaviour> swordsmans, List<EntityBehaviour> twoHandedSwordsman)
        {
            if (shooter.Count > noEnemies)
            {
                return shooter;
            }
            else if (twoHandedSwordsman.Count > noEnemies)
            {
                return twoHandedSwordsman;
            }
            else
            {
                return swordsmans;
            }
        }

        private List<EntityBehaviour> GetTargetsForTwoHandedSwordsman(List<EntityBehaviour> shooter, List<EntityBehaviour> swordsmans, List<EntityBehaviour> twoHandedSwordsman)
        {
            if (swordsmans.Count > noEnemies)
            {
                return swordsmans;
            }
            else if (shooter.Count > noEnemies)
            {
                return shooter;
            }
            else
            {
                return twoHandedSwordsman;
            }
        }
        #endregion
    }
}