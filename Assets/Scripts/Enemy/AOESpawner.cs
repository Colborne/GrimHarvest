using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpawner : MonoBehaviour
{
    EnemyManager enemyManager;
    public GameObject attack;
    public float attackRadius = 5f;
    public int amount;

    public void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }
    public void SpawnAttack()
    {
        for(int i = 0; i < amount; i++)
        {
            Vector3 spawnPoint = new Vector3(
                enemyManager.currentTarget.transform.position.x + Random.Range(-attackRadius, attackRadius), 
                enemyManager.currentTarget.transform.position.y, 
                enemyManager.currentTarget.transform.position.z + Random.Range(-attackRadius, attackRadius));
            Instantiate(attack, spawnPoint, Quaternion.identity);
        }
    }
}
