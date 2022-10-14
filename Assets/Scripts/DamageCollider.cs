﻿using System.Collections;
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
            if(collision.GetComponent<EnemyDamageCollider>())
                collision.GetComponent<EnemyDamageCollider>().DisableDamageCollider();

            if(collision.GetComponentInChildren<Animator>())
                collision.GetComponentInChildren<Animator>().CrossFade("Damage", .2f);

            Rigidbody[] bodies = collision.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody _rb in bodies)
                _rb.AddForce(transform.forward * force);

            EnemyManager ai = collision.GetComponent<EnemyManager>();
            
            if(ai) ai.TakeDamage(damage);  
        }
    }
}