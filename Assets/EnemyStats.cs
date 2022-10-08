using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int hp = 3;
    public EnemyAnimatorManager eam;
    private Vector3 previousPosition;
    public float curSpeed;
    void Update()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
        if(!eam.animator.GetBool("isInteracting"))
            eam.UpdateAnimatorValues(0, curSpeed);
    }
}