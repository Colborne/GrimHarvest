using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public ChaseState chaseState;
    public DodgeState dodgeState;
    public BlockState blockState;
    public EnemyAttackAction[] enemyAttacks;
    bool randomDestinationSet = false;
    bool directionSet = false;
    float verticalMovement = 0;
    float horizontalMovement = 0;
    int randomDirection;
    public int randomAction;
    bool willPerformBlock = false;
    bool willPerformDodge = false;
    bool hasRolled = false;
    bool hasBlocked = false;
    
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        if(enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.animator.SetFloat("V", 0);
            enemyAnimatorManager.animator.SetFloat("H", 0);
            return this;
        }

        HandleRotateTowardsTarget(enemyManager);

        if(distanceFromTarget > enemyManager.maximumAggroRange)
            return chaseState;

        if(enemyManager.currentTarget.isAttacking && enemyManager.allowBlock && !hasBlocked)
            RollForBlockChance(enemyManager);

        if(willPerformBlock)
        {
            ResetStateFlags();
            return blockState;
        }
        
        if(enemyManager.currentTarget.isAttacking && enemyManager.allowDodge && !hasRolled)
            RollForDodgeChance(enemyManager);

        if(willPerformDodge)
        {
            ResetStateFlags();
            return dodgeState;
        }

        if(!directionSet)
        {
            directionSet = true;
            if(Random.Range(0,2) != 0)
                randomDirection = -1;
            else
                randomDirection = 1;   
        }

        if(!randomDestinationSet)
        {
            randomAction = Random.Range(0,2);
            randomDestinationSet = true;
        }

        if(enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            GetNewAttack(enemyManager); 
            if(attackState.currentAttack != null)
            {
                ResetStateFlags();
                return attackState;   
            }      
        }
        else
        {
            if(!enemyManager.isPerformingAction)
                ChooseCombatAction(enemyManager, enemyAnimatorManager);
            else
                Approach(enemyManager,enemyAnimatorManager);
        }
        return this;
    }

    private void RollForBlockChance(EnemyManager enemy)
    {
        hasBlocked = true;
        int blockChance = Random.Range(0,100);
        if(blockChance <= enemy.blockPercent)
            willPerformBlock = true;
        else
            willPerformBlock = false;
    }

    private void RollForDodgeChance(EnemyManager enemy)
    {
        hasRolled = true;
        int dodgeChance = Random.Range(0,100);
        if(dodgeChance <= enemy.dodgePercent)
            willPerformDodge = true;
        else
            willPerformDodge = false;
    }

    private void ResetStateFlags()
    {
        hasRolled = false;
        hasBlocked = false; 
        randomDestinationSet = false;
        directionSet = false;
        willPerformBlock = false;
        willPerformDodge = false;
    }

    private void ChooseCombatAction(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        if(randomAction == 0)
            WalkAroundTarget(enemyManager, enemyAnimatorManager);
        else if(randomAction == 1)
            Backstep(enemyManager, enemyAnimatorManager);
        
    }

    private void Rush(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.animator.SetFloat("V", 1f, .2f, Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .051f);
        enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position); 
    }

    private void Approach(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {       
        enemyAnimatorManager.animator.SetFloat("V", .5f, .2f, Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .051f);
        enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position);     
    }
    
    private void Backstep(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.animator.SetFloat("V", -.5f, .2f, Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .051f);
        enemyManager.agent.SetDestination(enemyManager.transform.position - enemyManager.transform.forward * 10f);
    }

    private void WalkAroundTarget(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .1f);
        enemyAnimatorManager.animator.SetFloat("V", 0, .2f, Time.deltaTime);
        enemyAnimatorManager.animator.SetFloat("H", randomDirection, .2f, Time.deltaTime);
        Vector3 targetsDistance = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        var dir = Vector3.Cross(targetsDistance, Vector3.up);
        enemyManager.agent.SetDestination(enemyManager.transform.position + (dir * randomDirection)); 
    }

    private void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetsDistance = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetsDistance, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        List<EnemyAttackAction> possible = new List<EnemyAttackAction>();
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
                    possible.Add(enemyAttackAction);
                }
            }
        }

        int randomValue = Random.Range(0, possible.Count);

        foreach(EnemyAttackAction action in possible)
        {
            if(randomValue == possible.IndexOf(action))
                attackState.currentAttack = action;
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
            Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .051f);
        }
    }
}