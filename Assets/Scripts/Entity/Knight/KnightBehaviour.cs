using Entity.Enums;
using Entity.States;
using UnityEngine;

namespace Entity.Behaviours
{
    public sealed class KnightBehaviour : EntityBehaviour
    {
        #region Parameters    
        private KnightState knightState;

        internal Vector3 rallyPointPosition;

        private const int noVikings = 0;
        #endregion

        #region Properties
        public override float HealthPoints
        {
            get => base.HealthPoints;
            set
            {
                base.HealthPoints = value;

                if (base.HealthPoints <= noHealthPoints)
                {
                    State = KnightState.Dead;
                }
            }
        }

        public KnightState State
        {
            get => knightState;
            set
            {
                knightState = value;

                entityState.Close();

                InitializeState(knightState);
            }
        }
        #endregion

        #region MonoBehaviour API
        protected override void Start()
        {
            base.Start();

            KnightState state = curator.EntityHandler.AliveVikings.Count > noVikings ? KnightState.Chase : KnightState.Return;

            InitializeState(state);
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion

        #region Methods
        private void InitializeState(KnightState state)
        {
            entityState = state switch
            {
                KnightState.Attack => new KnightAttackState(this, EntityCharacteristics),
                KnightState.Await => new KnightAwaitState(this, EntityCharacteristics),
                KnightState.Chase => new KnightChaseState(this, EntityCharacteristics),
                KnightState.Dead => new KnightDeadState(this, EntityCharacteristics, curator),
                KnightState.Return => new KnightReturnState(this),
                _ => throw new System.NotImplementedException($"State {state} does not implemented.")
            };

            entityState.Initialize();
        }
        #endregion
    }
}