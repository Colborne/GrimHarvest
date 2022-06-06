using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material mat;
    public Light light;
    [ColorUsage(true, true)]
    public Color _emissionColorValue;
    public float _intensity;
    public float intensity;
    void Update()
    {
        _intensity = Mathf.Lerp(_intensity, Random.Range(3f, 6f), .1f);
        intensity = Mathf.Lerp(intensity, Random.Range(3f, 6f), .1f);
        light.intensity = intensity;
        mat.SetColor("_EmissionColor", _emissionColorValue * _intensity);
    }
}
