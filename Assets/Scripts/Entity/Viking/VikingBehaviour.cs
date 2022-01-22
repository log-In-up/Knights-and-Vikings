using Entity.Enums;
using Entity.Interfaces;
using Entity.States;
using GameLogic;
using GameLogic.Mechanics;
using System.Collections.Generic;
using Zenject;

namespace Entity.Behaviours
{
    public sealed class VikingBehaviour : EntityBehaviour
    {
        #region Parameters 
        private VikingState vikingState;
        private Dictionary<VikingState, IEntityState> States;

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
                entityState = States[vikingState];
                entityState.Initialize();
            }
        }
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(EntityHandler entityHandler, BattleCurator battleCurator, PlayerBase playerBase)
        {
            EntityHandler = entityHandler;
            BattleCurator = battleCurator;
            PlayerBase = playerBase;
        }
        #endregion

        #region MonoBehaviour API
        protected override void Start()
        {
            States = new Dictionary<VikingState, IEntityState>
            {
                [VikingState.AttackBase] = new VikingAttackBaseState(this, EntityCharacteristics, EntityHandler, PlayerBase),
                [VikingState.Attack] = new VikingAttackState(this, EntityCharacteristics, EntityHandler),
                [VikingState.Chase] = new VikingChaseState(this, EntityCharacteristics),
                [VikingState.Dead] = new VikingDeadState(this, EntityCharacteristics, EntityHandler),
                [VikingState.MovementToZone] = new VikingMovementToZoneState(this, EntityCharacteristics, EntityHandler, PlayerBase)
            };

            base.Start();

            VikingState state = EntityHandler.AliveKnights.Count > noKnights ? VikingState.Chase : VikingState.MovementToZone;
            entityState = States[state];
            entityState.Initialize();
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion
    }
}
