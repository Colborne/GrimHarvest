using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DamageCollider : MonoBehaviour
{
    public Collider damageCollider;
    public float force;
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

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.tag == "Enemy")
        {
            Rigidbody[] bodies = collision.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody _rb in bodies)
                _rb.AddForce(transform.forward * force);

            EnemyManager ai = collision.GetComponent<EnemyManager>();
            
            if(ai)
            {
                if(!ai.isTakingDamage)
                {
                    ai.TakeHit(damage * GetComponent<StatsManager>().heavyModifier);
                    if(ai.isBlocking) 
                    {
                        GetComponent<AnimatorManager>().PlayTargetAnimation("Impact", true);
                        GetComponent<Rigidbody>().AddForce(-transform.forward * force);
                    }
                }
            }
        }
    }
}