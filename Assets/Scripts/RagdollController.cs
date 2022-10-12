using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    void Start()
    {
        SetRigidBodyState(true);
        SetColliderState(false);
    }

    public void EnableRagdoll()
    {
        GetComponentInChildren<Animator>().enabled = false;
        SetRigidBodyState(false);
        SetColliderState(true);
    }

    public void SetRigidBodyState(bool state)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    public void SetColliderState(bool state)
    {
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if(col.gameObject.layer != LayerMask.NameToLayer("CollisionBlocker"))
                col.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }
}
