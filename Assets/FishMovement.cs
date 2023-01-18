using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public FishItem currentFish;
    public float strength;
    public RectTransform rect;
    public float Timer = 0f;
    int H, V;
    public float Radius = 10f;
    public float newSpeed = 5f;
    public float currentRotationSpeed = 5f;
    private Vector2 _centre = Vector2.zero;
    private float _angle;
    public float angleFromCenter;
    public float Distance;

    void Awake()
    {
        H = 1;
        V = 1;
        rect = GetComponent<RectTransform>();
    }

    public void SetCurrentFish(FishItem fish)
    {
        currentFish = fish;
        currentRotationSpeed = currentFish.rotateSpeed;
        rect.localPosition = Vector2.zero;
        Radius = 10f;
        strength = currentFish.baseStrength;
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector2.Distance(Vector2.zero, rect.localPosition);

        if(Timer <= 0)
        {
            newSpeed = Random.Range(currentFish.minSpeed, currentFish.maxSpeed);
            Timer = currentFish.switchTime;
        }

        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, newSpeed, 1f * Time.deltaTime);
        Timer -= 1f * Time.deltaTime;
    
        if(currentFish.variety == 1)
        {
            if((int)Random.Range(0,currentFish.burstLikelihood) == 0)
                strength = currentFish.burstStrength;
        }

        strength = Mathf.Lerp(strength, currentFish.baseStrength, 1f * Time.deltaTime);
        _angle += currentRotationSpeed * Time.deltaTime;
        
        if(Distance < 180)
            Radius += strength * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        rect.localPosition = _centre + offset;
        angleFromCenter = (int)Mathf.Abs(getAngle(Vector2.zero, rect.localPosition));
    }

    public float getAngle(Vector2 me, Vector2 target) {
        return Mathf.Atan2(target.y - me.y, target.x - me.x) * (180/Mathf.PI);
    }
}      