using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public ChaseState chaseState;
    public EnemyAttackAction[] enemyAttacks;
    bool randomDestinationSet = false;
    float verticalMovement = 0;
    float horizontalMovement = 0;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        enemyAnimatorManager.animator.SetFloat("V", verticalMovement, .2f, Time.deltaTime);
        enemyAnimatorManager.animator.SetFloat("H", verticalMovement, .2f, Time.deltaTime);

        if(enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.animator.SetFloat("V", 0);
            enemyAnimatorManager.animator.SetFloat("H", 0);
            return this;
        }

        if(distanceFromTarget > enemyManager.maximumAttackRange)
            return chaseState;

        if(!randomDestinationSet)
        {
            DecideCircleAction(enemyAnimatorManager);
            randomDestinationSet = true;
        }

        HandleRotateTowardsTarget(enemyManager);

        if(enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null)
        {
            randomDestinationSet = false;
            return attackState;
        }
        else
        {
            GetNewAttack(enemyManager);    
        }
        return this;
    }

    private void DecideCircleAction(EnemyAnimatorManager enemyAnimatorManager)
    {
        WalkAroundTarget(enemyAnimatorManager);
    }
    private void WalkAroundTarget(EnemyAnimatorManager enemyAnimatorManager)
    {
        verticalMovement = .5f;
        horizontalMovement = Random.Range(-1,1);

        if(horizontalMovement <= 1 && horizontalMovement >= 0)
            horizontalMovement = .5f;
        else if (horizontalMovement >= -1 && horizontalMovement < 0)
            horizontalMovement = -.5f;
    }

    private void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetsDistance = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDistance, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        int maxScore = 0;

        for( int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
            && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if(viewableAngle <= enemyAttackAction.maximumAttackAngle
                && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int tempScore = 0;

        for(int i = 0; i < enemyAttacks.Length;i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
            && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if(viewableAngle <= enemyAttackAction.maximumAttackAngle
                && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if(attackState.currentAttack != null)
                        return;

                    tempScore += enemyAttackAction.attackScore;
                }

                if(tempScore > randomValue)
                    attackState.currentAttack = enemyAttackAction;
            }
        }
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
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(direction * 4, Vector3.up);
            enemyManager.rigidbody.velocity = projectedVelocity;

            enemyManager.agent.enabled = true;
            enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.agent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }
}