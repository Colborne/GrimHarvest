using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public LayerMask detectionLayer;
    public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            StatsManager stats = colliders[i].transform.GetComponent<StatsManager>();

            if(stats != null)
            {
                Vector3 targetDirection = stats.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                if((viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle) || enemyManager.isTakingDamage)
                    enemyManager.currentTarget = stats;
            }
        }
        if(enemyManager.currentTarget != null)
            return chaseState;
        else
            return this;
    }
}
