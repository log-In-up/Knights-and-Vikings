using UnityEngine;
using Zenject;

namespace GameLogic.Mechanics
{
    public class EntityMaker : MonoBehaviour
    {
        #region Editor parameters
        [Header("Standard spawn settings")]
        [SerializeField] private protected Transform[] spawnPoints = null;
        #endregion

        #region Parameters
        private protected BattleCurator curator = null;
        private protected EntityHandler entityHandler = null;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(EntityHandler entityHandler, BattleCurator battleCurator)
        {
            this.entityHandler = entityHandler;
            
            curator = battleCurator;
        }
        #endregion
    }
}