using UnityEngine;

public sealed class VikingBehaviour : EntityBehaviour
{
    #region Parameters 
    private VikingState vikingState;

    private const int noKnights = 0;
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
                State = VikingState.Dead;
            }
        }
    }

    public VikingState State
    {
        get => vikingState;
        set
        {
            vikingState = value;

            entityState.Close();

            InitializeState(vikingState);

            Debug.Log($"Viking change own state to {vikingState}");
        }
    }
    #endregion

    #region MonoBehaviour API
    protected override void Start()
    {
        base.Start();

        VikingState state = curator.EntityHandler.AliveKnights.Count > noKnights ? VikingState.Chase : VikingState.MovementToZone;// Null reference exeption

        InitializeState(state);
    }

    protected override void Update()
    {
        base.Update();
    }
    #endregion

    #region Methods
    private void InitializeState(VikingState state)
    {
        entityState = state switch
        {
            VikingState.AttackBase => new VikingAttackBaseState(),
            VikingState.Attack => new VikingAttackState(this, EntityCharacteristics.AttackInterval, EntityCharacteristics.Damage),
            VikingState.Chase => new VikingChaseState(this, EntityCharacteristics.AttackRange),
            VikingState.Dead => new VikingDeadState(this, curator),
            VikingState.MovementToZone => new VikingMovementToZoneState(),
            _ => throw new System.NotImplementedException()
        };

        entityState.Initialize();
    }
    #endregion
}
