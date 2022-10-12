using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider blockerCollider;
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, blockerCollider, true);
    }


}
