using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactShaker : MonoBehaviour
{
    
    public CameraShake cameraShake;

    public void Shake(string dur_mag)
    {
        var dm = dur_mag.Split(",");
        StartCoroutine(cameraShake.Shake(float.Parse(dm[0]), float.Parse(dm[1])));
    }
}
