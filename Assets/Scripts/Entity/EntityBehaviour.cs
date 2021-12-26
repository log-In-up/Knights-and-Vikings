using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using GameLogic;
using GameLogic.Mechanics;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Behaviours
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EntityBehaviour : MonoBehaviour
    {
        #region Editor parameters
        [SerializeField] private EntityCharacteristics characteristics = null;
        [SerializeField] private EntitySubtype entitySubtype = EntitySubtype.Shooter;
        #endregion

        #region Parameters
        internal NavMeshAgent agent = null;
        internal EntityBehaviour enemy = null;

        private protected BattleCurator curator = null;
        private protected IEntityState entityState = null;
        
        private EntityTargetSelector targetSelector = null;
        private float healthPoints;

        private protected const float noHealthPoints = 0.0f;
        #endregion

        #region Properties
        public PlayerBase PlayerBase { get; set; }

        public virtual float HealthPoints
        {
            get => healthPoints;
            set => healthPoints = value;
        }

        public BattleCurator BattleCurator
        {
            get => curator;
            set => curator = value;
        }

        public EntitySubtype EntitySubtype => entitySubtype;

        public EntityCharacteristics EntityCharacteristics => characteristics;
        #endregion

        #region MonoBehaviour API
        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            targetSelector = new EntityTargetSelector(this);
        }

        protected virtual void Start()
        {
            HealthPoints = characteristics.HealthPoints;
        }

        protected virtual void Update()
        {
            entityState.Update();
        }
        #endregion

        #region Methods
        public EntityBehaviour SetTarget(EntityType entityType)
        {
            return targetSelector.SetTarget(entityType);
        }

        public void CauseDamage(DamageInfo damageInfo)
        {
            enemy.HealthPoints -= damageInfo.Damage;
        }
        #endregion
    }
}