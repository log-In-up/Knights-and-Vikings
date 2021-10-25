using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public sealed class KnightBehaviour : MonoBehaviour
{
    #region Parameters
    private IKnightState knight = null;
    private BattleCurator curator = null;
    private NavMeshAgent agent = null;

    private KnightState knightState;
    #endregion

    #region Properties
    public KnightState State
    {
        get => knightState;
        set
        {
            knightState = value;

            knight.Close();

            InitializeState(knightState);
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
        KnightState state = curator.InBattle ? KnightState.Chase : KnightState.Return;

        InitializeState(state);
    }

    private void Update()
    {
        knight.Update();
    }
    #endregion

    #region Custom methods
    internal void SetCurator(BattleCurator value) => curator = value;

    private void InitializeState(KnightState state)
    {
        knight = state switch
        {
            KnightState.Attack => new KnightAttackState(this),
            KnightState.Await => new KnightAwaitState(this),
            KnightState.Chase => new KnightChaseState(this),
            KnightState.Dead => new KnightDeadState(this),
            KnightState.Return => new KnightReturnState(this),
            _ => new KnightAwaitState(this)
        };

        knight.Initialize();
    }
    #endregion
}
