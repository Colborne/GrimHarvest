using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Items/Fish Item")]
public class FishItem : Item
{
    public GameObject Fish;
    public int variety = 0;
    public int baseStrength = 20;
    public int burstStrength = 50;
    public float switchTime = 2.5f;
    public float minSpeed = -3f;
    public float maxSpeed = 3f;
    public float rotateSpeed = 5f;
    public int burstLikelihood = 750;

}