using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArmature : MonoBehaviour
{
    public Transform bone;
    public EnemyManager enemyManager;
    void Update()
    {
        if(enemyManager != null){
        transform.position = bone.position;
        transform.rotation = bone.rotation * Quaternion.Euler(new Vector3(-90,0,0));
        }
    }
}
