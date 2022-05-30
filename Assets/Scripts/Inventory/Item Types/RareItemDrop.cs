using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItemDrop : MonoBehaviour
{
    public GameObject[] items;
    
    [Header("1 / Rarity")] public int[] rarity;
    private void OnDestroy() 
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(Random.Range(0, rarity[i]) == 0)
            {
                Instantiate(items[i], transform.position + transform.up * 2f, Quaternion.identity);
            }
        }
    }
}
