using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public CombatStanceState combatStanceState;
    Vector3 normalVector;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);
        
        if(enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.animator.SetFloat("V", 0, .1f, Time.deltaTime);
            enemyAnimatorManager.animator.SetFloat("H", 0);
            return this;
        }

        HandleRotateTowardsTarget(enemyManager);
        
        if(distanceFromTarget > enemyManager.maximumAttackRange)
        {
            enemyAnimatorManager.animator.SetFloat("V", 1, 0.1f, Time.deltaTime);
            enemyAnimatorManager.animator.SetFloat("H", 0);
            enemyAnimatorManager.animator.SetBool("isInteracting", false);

        }

        if(distanceFromTarget <= enemyManager.maximumAttackRange)
            return combatStanceState;
        else
            return this;
    }

    public void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        direction.y = 0;
        direction.Normalize();
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(direction * 4, normalVector);
        enemyManager.rb.velocity = projectedVelocity;

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
            Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .051f);
        }
    }
}