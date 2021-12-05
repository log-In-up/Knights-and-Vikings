using UnityEngine;

public sealed class KnightAttackState : IEntityState
{
    #region Parameters
    private readonly KnightBehaviour knightBehaviour = null;
    private readonly float attackInterval, damageAmount;

    private bool canAttack;
    private float attackTime;

    private const float noHealthPoints = 0.0f;
    private const int absenceVikings = 0;

    private bool enemyIsDead, enemyIsOnTheBattlefield;
    #endregion

    public KnightAttackState(KnightBehaviour knightBehaviour, float attackInterval, float damageAmount)
    {
        this.knightBehaviour = knightBehaviour;
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
        enemyIsDead = knightBehaviour.enemy.HealthPoints <= noHealthPoints;
        enemyIsOnTheBattlefield = knightBehaviour.BattleCurator.EntityHandler.AliveVikings.Count > absenceVikings;
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
            knightBehaviour.CauseDamage(damageAmount);

            attackTime += attackInterval;
        }
    }

    private void CheckTarget()
    {
        if (enemyIsDead)
        {
            if (enemyIsOnTheBattlefield)
            {
                knightBehaviour.enemy = knightBehaviour.SetTarget(EntityType.Viking);

                float attackRange = knightBehaviour.EntityCharacteristics.AttackRange;
                float distanceBetweenSelfAndEnemy = Vector3.Distance(knightBehaviour.transform.position, knightBehaviour.enemy.transform.position);

                if (distanceBetweenSelfAndEnemy > attackRange)
                {
                    knightBehaviour.State = KnightState.Chase;
                }
            }
            else
            {
                knightBehaviour.State = KnightState.Await;
            }
        }
    }
    #endregion
}