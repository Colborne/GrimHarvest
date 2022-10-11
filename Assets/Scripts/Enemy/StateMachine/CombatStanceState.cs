using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public ChaseState chaseState;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        if(enemyManager.isPerformingAction)
            enemyAnimatorManager.animator.SetFloat("V", 0, .1f, Time.deltaTime);

        if(enemyManager.currentRecoveryTime <= 0 &&  distanceFromTarget <= enemyManager.maximumAttackRange)
            return attackState;
        else if(distanceFromTarget > enemyManager.maximumAttackRange)
            return chaseState;
        else
            return this;
    }
}
