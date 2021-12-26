using Entity;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    [DisallowMultipleComponent]
    public sealed class PlayerBase : MonoBehaviour
    {
        #region Editor parameters
        [SerializeField] private Slider playerBaseHealthBar = null;
        [SerializeField] private TextMeshProUGUI buildingCapacity = null;
        #endregion

        #region Parameters
        private float healthPointsOfBuilding, maxHealthPointsOfBuilding;
        private uint buildingLevel;

        private const float minHealthPointsOfBuilding = 0.0f;
        private const int healthPointsDegree = 2;
        private const string buildingIsDestroyed = "Building is destroyed.";
        #endregion

        #region Properties
        public float BuildingCapacity
        {
            get => healthPointsOfBuilding;
            set => healthPointsOfBuilding = value;
        }

        public uint BuildingLevel
        {
            get => buildingLevel;
            set
            {
                buildingLevel = value;

                float newHP = (float)Math.Round(Mathf.Sqrt(buildingLevel), healthPointsDegree);

                healthPointsOfBuilding = maxHealthPointsOfBuilding = newHP;

                playerBaseHealthBar.minValue = minHealthPointsOfBuilding;
                playerBaseHealthBar.maxValue = healthPointsOfBuilding;
                playerBaseHealthBar.value = healthPointsOfBuilding;

                buildingCapacity.text = $"Capacity is = {healthPointsOfBuilding}";
            }
        }
        #endregion

        #region MonoBehaviour API
        private void Start()
        {
            playerBaseHealthBar.wholeNumbers = false;

            BuildingLevel = 156;
        }
        #endregion

        #region Methods
        public void ApplyDamage(DamageInfo damageInfo)
        {
            healthPointsOfBuilding -= damageInfo.Damage;

            healthPointsOfBuilding = Mathf.Clamp(healthPointsOfBuilding, minHealthPointsOfBuilding, maxHealthPointsOfBuilding);
            playerBaseHealthBar.value = healthPointsOfBuilding;
            buildingCapacity.text = $"Capacity is = {healthPointsOfBuilding}";

            if (healthPointsOfBuilding <= minHealthPointsOfBuilding)
            {
                Debug.LogWarning(buildingIsDestroyed);
            }
        }
        #endregion
    }
}