using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    public FishItem[] commonFish;
    public FishItem[] uncommonFish;
    public FishItem[] rareFish;

    public FishItem SelectFish()
    {
        int rand = Random.Range(0,10);
        if(rand < 7)
            return commonFish[Random.Range(0,commonFish.Length)];
        else if (rand < 9)
            return uncommonFish[Random.Range(0,uncommonFish.Length)];
        else if (rand == 9)
            return rareFish[Random.Range(0,rareFish.Length)];
        else
            return null;
    }
}
