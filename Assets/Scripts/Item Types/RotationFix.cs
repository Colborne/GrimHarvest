using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour
{
    public int x;
    public int y;
    public int z;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Vector3.up + new Vector3(x,y,z));
    }
}
