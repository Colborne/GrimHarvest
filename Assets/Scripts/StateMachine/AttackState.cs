using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override State Tick(EnemyAiManager enemyAiManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        return this;
    }
}
