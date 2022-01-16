using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    [DisallowMultipleComponent]
    public sealed class BattleCurator : MonoBehaviour
    {
        #region Editor parameters
        [Header("Other components")]
        [SerializeField] private Slider waveState = null;
        [SerializeField] private TextMeshProUGUI waveText = null;

        [Header("Control buttons")]
        [SerializeField] private Button startGame = null;
        #endregion

        #region Parameters
        private bool inBattle;
        private uint currentWave, levelTokens;

        private const int lackOfOpponents = 0;
        #endregion

        #region Properties
        public bool InBattle { get => inBattle; set => inBattle = value; }

        public uint CurrentWave
        {
            get => currentWave;
            set
            {
                currentWave = value;

                float newWave = Mathf.Sqrt(currentWave);
                levelTokens = (uint)Mathf.Round(newWave);

                waveState.maxValue = levelTokens;
                waveState.value = levelTokens;

                waveText.text = $"Wave: {currentWave}";
            }
        }

        public uint LevelTokens { get => levelTokens; set => levelTokens = value; }

        public uint WaveEnemiesCount { get => (uint)waveState.value; set => waveState.value = value; }
        #endregion

        #region MonoBehaviour API
        private void OnEnable()
        {
            startGame.onClick.AddListener(StartBattle);
        }

        private void Start()
        {
            waveState.wholeNumbers = true;
            waveState.minValue = lackOfOpponents;

            CurrentWave = 3;

            inBattle = false;
        }

        private void OnDisable()
        {
            startGame.onClick.RemoveListener(StartBattle);
        }
        #endregion

        #region Button handlers
        private void StartBattle()
        {
            inBattle = true;
        }
        #endregion
    }
}