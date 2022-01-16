using UnityEngine;

namespace GameLogic.Mechanics
{
    [RequireComponent(typeof(BattleCurator))]
    public class EntityMaker : MonoBehaviour
    {
        #region Editor parameters
        [Header("Standart spawn settings")]
        [SerializeField] private protected EntityHandler entityHandler = null;
        [SerializeField] private protected Transform[] spawnPoints = null;
        #endregion

        #region Parameters
        private protected BattleCurator curator = null;
        #endregion

        protected virtual void Awake()
        {
            curator = GetComponent<BattleCurator>();
        }
    }
}