using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmableObject : MonoBehaviour
{
    public float growthTime;
    void Start()
    {
        StartCoroutine(GrowthCycle(growthTime));
    }
    IEnumerator GrowthCycle(float time)
    {
        yield return new WaitForSeconds(time);
        transform.localScale *= 2;
        if(transform.localScale.x < 1)
            StartCoroutine(GrowthCycle(growthTime));  
    }

}
