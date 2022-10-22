using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountController : MonoBehaviour
{
    public Animator animator;
    RagdollController ragdollController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        ragdollController = GetComponent<RagdollController>();
    }
    public void Dead()
    {
        ragdollController.EnableRagdoll();
    }
}
