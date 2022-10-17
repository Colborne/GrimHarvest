using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    public CombatStanceState combatStanceState;
    public ChaseState chaseState;
    bool hasPerfomedDodge = false;
    bool hasRandomDodgeDirection;
    Quaternion targetDodgeDirection;
    
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        
        if(distanceFromTarget > enemyManager.maximumAggroRange)
        {
            ResetStateFlags();
            return chaseState;   
        }

        if(hasPerfomedDodge && !enemyAnimatorManager.animator.GetBool("isDodging"))
        {
            ResetStateFlags();
            enemyManager.SetDamageAbsorption(0);
            return combatStanceState;
        }
        
        if(!hasPerfomedDodge)
        {
            Dodge(enemyManager,enemyAnimatorManager);
        }
        return this;
    }

    private void ResetStateFlags()
    {
        hasPerfomedDodge = false;
        hasRandomDodgeDirection = false;
    }    
    public void Dodge(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.SetDamageAbsorption(100);
        if(!hasRandomDodgeDirection)
        {
            float randomDodgeDirection;
            hasRandomDodgeDirection = true;
            randomDodgeDirection = Random.Range(0,360);
            targetDodgeDirection = Quaternion.Euler(enemyManager.transform.eulerAngles.x, randomDodgeDirection, enemyManager.transform.eulerAngles.z);
        }

        if(enemyManager.transform.rotation != targetDodgeDirection)
        {
            Quaternion targetRotation = Quaternion.Slerp(enemyManager.transform.rotation, targetDodgeDirection, 1f);
            enemyManager.transform.rotation = targetRotation;

            float targetYDirection = targetDodgeDirection.eulerAngles.y;
            float currentYRotation = enemyManager.transform.eulerAngles.y;
            float rotationDifference = Mathf.Abs(targetYDirection - currentYRotation);

            if(rotationDifference <= 5)
            {
                hasPerfomedDodge = true;
                enemyManager.transform.rotation = targetDodgeDirection;
                enemyAnimatorManager.PlayTargetAnimation("Roll", true);
                enemyManager.agent.SetDestination(enemyManager.transform.position + enemyManager.transform.forward * 8f);
            }
        }
        
    }
}