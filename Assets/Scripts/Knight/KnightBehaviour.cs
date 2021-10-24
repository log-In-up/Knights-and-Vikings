using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public sealed class KnightBehaviour : MonoBehaviour
{
    #region Parameters
    [SerializeField] private KnightState knightState = KnightState.Return;

    private IKnightState knight = null;

    private NavMeshAgent agent = null;
    #endregion

    #region Properties
    public KnightState State
    {
        get => knightState;
        set
        {
            knightState = value;

            knight.Close();

            knight = knightState switch
            {
                KnightState.Attack => new KnightAttackState(),
                KnightState.Await => new KnightAwaitState(),
                KnightState.Chase => new KnightChaseState(),
                KnightState.Dead => new KnightDeadState(),
                KnightState.Return => new KnightReturnState(),
                _ => new KnightAwaitState()
            };

            knight.Initialize();
        }
    }
    #endregion

    #region MonoBehaviour API
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InitializeInitialState(knightState);
    }

    private void Update()
    {
        knight.Update();
    }
    #endregion

    #region Custom methods
    private void InitializeInitialState(KnightState state)
    {
        knight = state switch
        {
            KnightState.Attack => new KnightAttackState(),
            KnightState.Await => new KnightAwaitState(),
            KnightState.Chase => new KnightChaseState(),
            KnightState.Dead => new KnightDeadState(),
            KnightState.Return => new KnightReturnState(),
            _ => new KnightAwaitState()
        };

        knight.Initialize();
    }
    #endregion
}
