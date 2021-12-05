using UnityEngine;

public sealed class KnightChaseState : IEntityState
{
    #region Parameters
    private readonly float attackRange;

    private readonly KnightBehaviour knightBehaviour = null;

    private bool canAttack;
    private float distanceToTarget;
    #endregion

    public KnightChaseState(KnightBehaviour knightBehaviour, float attackRange)
    {
        this.knightBehaviour = knightBehaviour;
        this.attackRange = attackRange;        
    }

    #region Interface implementation
    public void Act()
    {
        StartAttackTarget();
    }

    public void Close()
    {
        knightBehaviour.agent.ResetPath();
    }

    public void Initialize()
    {
        SetDestination();

        canAttack = false;
        distanceToTarget = 0.0f;
    }

    public void Sense()
    {
        distanceToTarget = Vector3.Distance(knightBehaviour.transform.position, knightBehaviour.enemy.transform.position);
    }

    public void Think()
    {
        canAttack = distanceToTarget <= attackRange;        
    }
    #endregion

    #region Methods
    private void StartAttackTarget()
    {
        if (canAttack)
        {
            knightBehaviour.State = KnightState.Attack;
        }
    }

    private void SetDestination()
    {
        knightBehaviour.enemy = knightBehaviour.SetTarget(EntityType.Viking);

        knightBehaviour.agent.SetDestination(knightBehaviour.enemy.transform.position);
    }
    #endregion
}