using UnityEngine;

public sealed class VikingAttackState : IEntityState
{
    #region Parameters
    private readonly float attackInterval, damageAmount;
    private readonly VikingBehaviour vikingBehaviour = null;

    private bool canAttack, enemyIsDead, enemyIsOnTheBattlefield;
    private float attackTime;

    private const float noHealthPoints = 0.0f;
    private const int absenceOfKnights = 0;
    #endregion

    public VikingAttackState(VikingBehaviour vikingBehaviour, float attackInterval, float damageAmount)
    {
        this.vikingBehaviour = vikingBehaviour;
        this.attackInterval = attackInterval;
        this.damageAmount = damageAmount;
    }

    #region Interface implementation
    public void Act()
    {
        AttackTarget();
    }

    public void Close()
    {

    }

    public void Initialize()
    {
        canAttack = false;
        attackTime = Time.time;
    }

    public void Sense()
    {
        enemyIsDead = vikingBehaviour.enemy.HealthPoints <= noHealthPoints;
        enemyIsOnTheBattlefield = vikingBehaviour.BattleCurator.EntityHandler.AliveKnights.Count > absenceOfKnights;
    }

    public void Think()
    {
        canAttack = Time.time >= attackTime;

        CheckTarget();
    }
    #endregion

    #region Methods
    private void AttackTarget()
    {
        if (canAttack)
        {
            vikingBehaviour.CauseDamage(damageAmount);

            attackTime += attackInterval;
        }
    }

    private void CheckTarget()
    {
        if (enemyIsDead)
        {
            if (enemyIsOnTheBattlefield)
            {
                vikingBehaviour.enemy = vikingBehaviour.SetTarget(EntityType.Knight);

                float attackRange = vikingBehaviour.EntityCharacteristics.AttackRange;
                float distanceBetweenSelfAndEnemy = Vector3.Distance(vikingBehaviour.transform.position, vikingBehaviour.enemy.transform.position);

                if (distanceBetweenSelfAndEnemy > attackRange)
                {
                    vikingBehaviour.State = VikingState.Chase;
                }
            }
            else
            {
                vikingBehaviour.State = VikingState.MovementToZone;
            }
        }
    }
    #endregion
}