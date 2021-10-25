public sealed class KnightBehaviour : EntityBehaviour
{
    #region Parameters    
    private KnightState knightState;
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

            InitializeState(knightState);
        }
    }
    #endregion

    #region MonoBehaviour API
    private void Start()
    {
        KnightState state = curator.InBattle ? KnightState.Chase : KnightState.Return;

        InitializeState(state);
    }

    private void Update()
    {
        entityState.Update();
    }
    #endregion

    #region Custom methods
    private void InitializeState(KnightState state)
    {
        entityState = state switch
        {
            KnightState.Attack => new KnightAttackState(this),
            KnightState.Await => new KnightAwaitState(this),
            KnightState.Chase => new KnightChaseState(this),
            KnightState.Dead => new KnightDeadState(this),
            KnightState.Return => new KnightReturnState(this),
            _ => new KnightAwaitState(this)
        };

        entityState.Initialize();
    }
    #endregion
}
