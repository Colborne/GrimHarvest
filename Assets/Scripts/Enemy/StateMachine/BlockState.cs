using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    public CombatStanceState combatStanceState;
    public ChaseState chaseState;
    bool endBlock = false;
    bool setDamage = false;

    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        HandleRotateTowardsTarget(enemyManager);
        
        if(distanceFromTarget > enemyManager.maximumAggroRange)
            return chaseState;
        
        if(endBlock)
        {
            endBlock = false;
            setDamage = false;
            enemyAnimatorManager.animator.SetBool("isBlocking", false);
            return combatStanceState;
        }

        if(enemyManager.isBlocking == false)
            Block(enemyManager,enemyAnimatorManager);
        
        if(setDamage)
            enemyManager.SetDamageAbsorption(50);

        return this;
    }
  
    private void Block(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        if(enemyManager.allowBlock)
        {
            enemyManager.isBlocking = true;
            enemyAnimatorManager.animator.SetBool("isBlocking", true);
            enemyManager.enemyAnimatorManager.PlayTargetAnimation("Block", true);
            enemyManager.SetDamageAbsorption(100);
            StartCoroutine(ResetBlock(enemyManager, 2f));
            StartCoroutine(ResetDamageReduction(enemyManager, .5f));
        }

    }
    IEnumerator ResetDamageReduction(EnemyManager enemyManager, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        setDamage = true;
    }

    IEnumerator ResetBlock(EnemyManager enemyManager, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        enemyManager.isBlocking = false;
        endBlock = true;
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
            Quaternion targetRotation = Quaternion.LookRotation(enemyManager.currentTarget.transform.position - enemyManager.transform.position);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, .051f);
        }
    }
}