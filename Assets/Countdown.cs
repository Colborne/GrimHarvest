using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float timer = 5f;
    public float triggerAliveTimer = .5f;
    public GameObject explosion;
    public Collider damageCollider;
    public int damage;

    void Start()
    {
        Invoke("Begin", timer);
    }

    void Begin()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        damageCollider.enabled = true;
        Invoke("End", triggerAliveTimer);
    }

    void End()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
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
