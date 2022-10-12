using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public CombatStanceState combatStanceState;
    public RotateTowardsState rotateTowardsState;
    Vector3 normalVector;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

        HandleRotateTowardsTarget(enemyManager);
        
        enemyManager.agent.transform.localPosition = Vector3.zero;
        enemyManager.agent.transform.localRotation = Quaternion.identity;

        if(viewableAngle > 65 || viewableAngle < -65)
            return rotateTowardsState;

        if(enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.animator.SetFloat("V", 0, .1f, Time.deltaTime);
            return this;
        }
        
        if(distanceFromTarget > enemyManager.maximumAttackRange){
            enemyAnimatorManager.animator.SetFloat("V", 1, 0.1f, Time.deltaTime);
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

        if(enemyManager.isPerformingAction)
        {
            if(direction == Vector3.zero)
                direction = enemyManager.transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(direction * 4, normalVector);
            enemyManager.rigidbody.velocity = projectedVelocity;

            enemyManager.agent.enabled = true;
            enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.agent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }
}