using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override State Tick(EnemyManager enemyAiManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        return this;
    }
}
