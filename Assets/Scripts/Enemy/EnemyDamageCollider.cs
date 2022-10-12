using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyDamageCollider : MonoBehaviour
{
    public Collider damageCollider;
    public int damage;
    private void Awake() 
    {
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }
    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }
    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
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