using UnityEngine;

public sealed class VikingChaseState : IEntityState
{
    #region Parameters
    private readonly float attackRange;
    private readonly VikingBehaviour vikingBehaviour = null;

    private bool canAttack;
    private float distanceToTarget;
    #endregion

    public VikingChaseState(VikingBehaviour vikingBehaviour, float attackRange)
    {
        this.vikingBehaviour = vikingBehaviour;
        this.attackRange = attackRange;
    }

    #region Interface implementation
    public void Act()
    {
        CheckDistanceToTarget();
    }

    public void Close()
    {
        vikingBehaviour.agent.ResetPath();
    }

    public void Initialize()
    {
        SetDestination();

        distanceToTarget = 0.0f;
    }

    public void Sense()
    {
        distanceToTarget = Vector3.Distance(vikingBehaviour.transform.position, vikingBehaviour.enemy.transform.position);
    }

    public void Think()
    {
        canAttack = distanceToTarget <= attackRange;
    }
    #endregion

    #region Methods
    private void CheckDistanceToTarget()
    {
        if (canAttack)
        {
            vikingBehaviour.State = VikingState.Attack;
        }
    }    

    private void SetDestination()
    {
        vikingBehaviour.enemy = vikingBehaviour.SetTarget(EntityType.Knight);

        vikingBehaviour.agent.SetDestination(vikingBehaviour.enemy.transform.position);            
    }    
    #endregion
}