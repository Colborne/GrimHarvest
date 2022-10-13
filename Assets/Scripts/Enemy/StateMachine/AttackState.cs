using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    
    public EnemyAttackAction currentAttack;

    public CombatStanceState combatStanceState;
    public ChaseState chaseState;

    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        HandleRotateTowardsTarget(enemyManager);
        
        if(distanceFromTarget > enemyManager.maximumAttackRange)
            return chaseState;

        //if can combo
        if(!enemyManager.isPerformingAction)
        {
            AttackTarget(enemyManager, enemyAnimatorManager);
            //roll for combo
        }

        return combatStanceState;
    }

    public void AttackTarget(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
    }
    public void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        direction.y = 0;
        direction.Normalize();
        
        if(enemyManager.isPerformingAction)
        {
            if(direction == Vector3.zero)
                direction = enemyManager.transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            enemyManager.agent.enabled = true;
            enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.agent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }
}