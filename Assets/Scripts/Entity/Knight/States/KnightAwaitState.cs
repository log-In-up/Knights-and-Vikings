using Entity.Behaviours;
using Entity.Characteristics;
using Entity.Enums;
using Entity.Interfaces;
using UnityEngine;

namespace Entity.States
{
    public sealed class KnightAwaitState : IEntityState
    {
        #region Parameters
        private bool canReturn;
        private float initializeTime;

        private readonly KnightBehaviour knightBehaviour = null;
        private readonly float awaitingTime;
        #endregion

        public KnightAwaitState(KnightBehaviour knightBehaviour, EntityCharacteristics characteristics)
        {
            this.knightBehaviour = knightBehaviour;

            awaitingTime = characteristics.AwaitingTime;
        }

        #region Interface implementation
        public void Act()
        {
            Expect();
        }

        public void Close()
        {

        }

        public void Initialize()
        {
            canReturn = false;

            initializeTime = Time.time;
        }

        public void Sense()
        {

        }

        public void Think()
        {
            canReturn = Time.time >= initializeTime + awaitingTime;
        }
        #endregion

        #region Methods
        private void Expect()
        {
            if (canReturn)
            {
                knightBehaviour.State = KnightState.Return;
            }
        }
        #endregion
    }
}