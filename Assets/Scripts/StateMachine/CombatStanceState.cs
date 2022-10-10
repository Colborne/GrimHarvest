using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public override State Tick(EnemyAiManager enemyAiManager, EnemyAnimatorManager enemyAnimatorManager)
    {
        return this;
    }
}
