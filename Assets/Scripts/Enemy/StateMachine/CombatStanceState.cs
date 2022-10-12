using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public ChaseState chaseState;
    public EnemyAttackAction[] enemyAttacks;
    bool randomDestinationSet = false;
    bool directionSet = false;
    float verticalMovement = 0;
    float horizontalMovement = 0;
    int randomDirection;
    public int randomAction;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        HandleRotateTowardsTarget(enemyManager);

        if(enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.animator.SetFloat("V", 0);
            enemyAnimatorManager.animator.SetFloat("H", 0);
            enemyManager.agent.ResetPath();
            return this;
        }

        if(distanceFromTarget > enemyManager.maximumAggroRange)
            return chaseState;

        if(!directionSet)
        {
            int rand = Random.Range(0,2);
            if(rand != 0)
                randomDirection = -1;
            else
                randomDirection = 1;
            directionSet = true;
        }

        if(!randomDestinationSet){
            randomAction = Random.Range(0,2);
            randomDestinationSet = true;
        }

        if(enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            if(attackState.currentAttack != null)
            {
                directionSet = false;
                randomDestinationSet = false;
                return attackState;
            }
            else
                GetNewAttack(enemyManager);    
        }
        else
        {
            ChooseCombatAction(enemyManager, enemyAnimatorManager);
            return this;
        }

        return this;
    }

    private void ChooseCombatAction(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        if(randomAction == 0)
            WalkAroundTarget(enemyManager, enemyAnimatorManager);
        else if(randomAction == 1)
            Backstep(enemyManager, enemyAnimatorManager);
  
    }
    
    private void StandGround(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        if(!enemyAnimatorManager.animator.GetBool("isInteracting"))
            enemyAnimatorManager.PlayTargetAnimation("Block_Idle", true);
        enemyManager.transform.LookAt(enemyManager.currentTarget.transform.position);
        enemyAnimatorManager.animator.SetFloat("V", 0, .2f, Time.deltaTime);
        enemyAnimatorManager.animator.SetFloat("H", 0, .2f, Time.deltaTime);
    }

    private void Backstep(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.animator.SetFloat("V", -.5f, .2f, Time.deltaTime);
        enemyManager.transform.LookAt(enemyManager.currentTarget.transform.position);
        enemyManager.agent.SetDestination(enemyManager.transform.position - enemyManager.transform.forward * 10f);
    }

    private void WalkAroundTarget(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.transform.LookAt(enemyManager.currentTarget.transform.position);
        enemyAnimatorManager.animator.SetFloat("V", 0, .2f, Time.deltaTime);
        enemyAnimatorManager.animator.SetFloat("H", randomDirection, .2f, Time.deltaTime);
        Vector3 targetsDistance = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        var dir = Vector3.Cross(targetsDistance, Vector3.up);
        enemyManager.agent.SetDestination(enemyManager.transform.position + (dir * 10f * randomDirection));
    }

    private void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetsDistance = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
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
                direction = transform.forward;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }
}