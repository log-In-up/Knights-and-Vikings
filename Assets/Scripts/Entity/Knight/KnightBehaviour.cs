using Entity.Enums;
using Entity.Interfaces;
using Entity.States;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Behaviours
{
    public sealed class KnightBehaviour : EntityBehaviour
    {
        #region Parameters    
        private KnightState knightState;
        private Dictionary<KnightState, IEntityState> States;

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
                entityState = States[knightState];
                entityState.Initialize();
            }
        }
        #endregion

        #region MonoBehaviour API
        protected override void Start()
        {
            States = new Dictionary<KnightState, IEntityState>
            {
                [KnightState.Attack] = new KnightAttackState(this, EntityCharacteristics, EntityHandler),
                [KnightState.Await] = new KnightAwaitState(this, EntityCharacteristics),
                [KnightState.Chase] = new KnightChaseState(this, EntityCharacteristics),
                [KnightState.Dead] = new KnightDeadState(this, EntityCharacteristics, EntityHandler),
                [KnightState.Return] = new KnightReturnState(this, EntityHandler)
            };

            base.Start();

            KnightState state = EntityHandler.AliveVikings.Count > noVikings ? KnightState.Chase : KnightState.Return;
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