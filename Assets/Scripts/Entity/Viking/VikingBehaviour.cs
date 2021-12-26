using Entity.Enums;
using Entity.States;

namespace Entity.Behaviours
{
    public sealed class VikingBehaviour : EntityBehaviour
    {
        #region Parameters 
        private VikingState vikingState;

        private const int noKnights = 0;
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
                    State = VikingState.Dead;
                }
            }
        }

        public VikingState State
        {
            get => vikingState;
            set
            {
                vikingState = value;

                entityState.Close();

                InitializeState(vikingState);
            }
        }
        #endregion

        #region MonoBehaviour API
        protected override void Start()
        {
            base.Start();

            VikingState state = curator.EntityHandler.AliveKnights.Count > noKnights ? VikingState.Chase : VikingState.MovementToZone;

            InitializeState(state);
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion

        #region Methods
        private void InitializeState(VikingState state)
        {
            entityState = state switch
            {
                VikingState.AttackBase => new VikingAttackBaseState(this, EntityCharacteristics, PlayerBase),
                VikingState.Attack => new VikingAttackState(this, EntityCharacteristics),
                VikingState.Chase => new VikingChaseState(this, EntityCharacteristics),
                VikingState.Dead => new VikingDeadState(this, EntityCharacteristics, curator),
                VikingState.MovementToZone => new VikingMovementToZoneState(this, EntityCharacteristics, PlayerBase),
                _ => throw new System.NotImplementedException($"State {state} does not implemented.")
            };

            entityState.Initialize();
        }
        #endregion
    }
}
