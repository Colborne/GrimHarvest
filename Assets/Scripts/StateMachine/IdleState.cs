using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override State Tick(EnemyManager enemyAiManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        return this;
    }
}
