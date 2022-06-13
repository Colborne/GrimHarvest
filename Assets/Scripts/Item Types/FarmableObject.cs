using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmableObject : MonoBehaviour
{
    public float growthTime;
    public GameObject drop;
    public GameObject fx;
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

    public void OnDestroy() 
    {
        if(transform.localScale.x == 1){
            Instantiate(drop, transform.position, Quaternion.identity);
            Instantiate(fx, transform.position, Quaternion.identity);
        }
    }
}
