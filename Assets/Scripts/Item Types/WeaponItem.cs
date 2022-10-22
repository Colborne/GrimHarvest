using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public enum DamageType
    {
        crush,
        stab,
        slash
    }

    [Header("Weapon Settings")]
    public bool isTwoHanded;
    //[Range(0,2)] //0 = hand, 1 = crossbody, 2 = shoulder
    //public int idleAnim;

    [Header("Attack Animations")]
    public string Light_Attack;
    public string Heavy_Attack;

    [Header("Stats")]
    public int lightCost;
    public int heavyCost;
    public float damage;
}