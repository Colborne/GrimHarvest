using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountController : MonoBehaviour
{
    EnemyManager enemy;
    void Awake()
    {
        enemy = GetComponentInParent<EnemyManager>();
    }
    void Update()
    {
        GetComponent<Animator>().SetFloat("V", enemy.enemyAnimatorManager.animator.GetFloat("V"));
    }
}
