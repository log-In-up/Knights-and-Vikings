using UnityEngine;

namespace Entity.Characteristics
{
    public class EntityCharacteristics : ScriptableObject
    {
        #region Parameters
#pragma warning disable IDE0044
        [Header("Survival characteristics.")]
        [SerializeField, Min(0.0f)] private float healthPoints = 50.0f;

        [Header("Combat characteristics.")]
        [SerializeField, Min(0.0f)] private float attackRange = 5.0f;
        [SerializeField, Min(0.0f)] private float attackInterval = 1.0f;
        [SerializeField, Min(0.0f)] private float damageAmount = 5.0f;

        [Header("Common characteristics.")]
        [SerializeField, Min(0.0f)] private float awaitingTime = 5.0f;
#pragma warning restore IDE0044
        #endregion

        #region Properties
        public float AwaitingTime => awaitingTime;
        public float AttackRange => attackRange;
        public float AttackInterval => attackInterval;
        public float Damage => damageAmount;
        public float HealthPoints => healthPoints;
        #endregion
    }
}