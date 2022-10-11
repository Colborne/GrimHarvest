using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsState : State
{
    public CombatStanceState combatStanceState;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.animator.SetFloat("Vertical", 0);

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

        if(viewableAngle >= 100 && viewableAngle <= 180 & !enemyManager.isPerformingAction){
            enemyAnimatorManager.PlayTargetAnimation("Turn 180", true);
            return this;
        }
        
        return combatStanceState;
    }
}
