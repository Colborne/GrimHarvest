using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyDamageCollider : MonoBehaviour
{
    public Collider[] damageCollider;
    public int damage;
    private void Awake() 
    {
        for(int i = 0; i < damageCollider.Length; i++)
        {
            damageCollider[i].gameObject.SetActive(true);
            damageCollider[i].isTrigger = true;
            damageCollider[i].enabled = false;
        }
    }
    public void EnableDamageCollider(int index)
    {
        damageCollider[index].enabled = true;
    }
    public void DisableDamageCollider()
    {
        for(int i = 0; i < damageCollider.Length; i++)
            damageCollider[i].enabled = false;
    }

    public void ResetRecoveryTime()
    {
        GetComponentInParent<EnemyManager>().currentRecoveryTime = 0;
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<StatsManager>().isTakingDamage == false && collision.GetComponent<StatsManager>().isInvincible == false)
            {
                collision.GetComponent<StatsManager>().TakeDamage(damage);
            } 
        }
    }
}