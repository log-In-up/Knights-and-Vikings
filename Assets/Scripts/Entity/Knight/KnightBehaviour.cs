using UnityEngine;

public sealed class KnightBehaviour : EntityBehaviour
{
    #region Parameters    
    private KnightState knightState;

    internal Vector3 rallyPointPosition;
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

            Debug.Log($"Knight change own state to {knightState}");
        }
    }
    #endregion

    #region MonoBehaviour API
    protected override void Start()
    {
        base.Start();

        KnightState state = curator.InBattle ? KnightState.Chase : KnightState.Return;

        InitializeState(state);
    }

    protected override void Update()
    {
        base.Update();
    }
    #endregion

    #region Methods
    private void InitializeState(KnightState state)
    {
        entityState = state switch
        {
            KnightState.Attack => new KnightAttackState(this, EntityCharacteristics.AttackInterval, EntityCharacteristics.Damage),
            KnightState.Await => new KnightAwaitState(this, EntityCharacteristics.AwaitingTime),
            KnightState.Chase => new KnightChaseState(this, EntityCharacteristics.AttackRange),
            KnightState.Dead => new KnightDeadState(this, curator),
            KnightState.Return => new KnightReturnState(this),
            _ => throw new System.NotImplementedException()
        };

        entityState.Initialize();
    }
    #endregion
}