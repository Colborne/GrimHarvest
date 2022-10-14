using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider blockerCollider;
    
    void Awake()
    {
        Physics.IgnoreCollision(characterCollider, blockerCollider, true);
    }
}