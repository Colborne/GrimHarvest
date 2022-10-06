using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public Collider damageCollider;
    public float force;
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
            {
                //_rb.AddExplosionForce(15, transform.position, 25f, 0f, ForceMode.Impulse);
                _rb.AddForce(transform.forward * force);
            }
            RagdollController rdc = collision.GetComponent<RagdollController>();
            Rigidbody rb = collision.GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * force);
            rdc.EnableRagdoll();
        }
    }
}