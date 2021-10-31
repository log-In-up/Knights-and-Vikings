using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public class EntityBehaviour : MonoBehaviour
{
    #region Parameters
    [SerializeField] private protected EntityCharacteristics characteristics = null;

    internal NavMeshAgent agent = null;
    private protected BattleCurator curator = null;
    private protected IEntityState entityState = null;

    private float healthPoints;

    private protected const float noHealthPoints = 0.0f;
    #endregion

    #region Properties
    public virtual float HealthPoints
    {
        get => healthPoints;
        set => healthPoints = value;
    }
    #endregion

    #region MonoBehaviour API
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        HealthPoints = characteristics.HealthPoints;
    }
    #endregion

    #region Custom methods
    internal void SetCurator(BattleCurator value) => curator = value;
    #endregion
}
