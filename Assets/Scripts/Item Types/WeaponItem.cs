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
        slash,
        magic,
        range
    }

    [Header("Weapon Settings")]
    public bool isTwoHanded;
    public bool isOffhandable;
    public bool canCharge;
    [Range(0,2)] //0 = hand, 1 = crossbody, 2 = shoulder
    public int idleAnim;

    [Header("Attack Animations")]
    public string Attack;
    public string Heavy_Attack;
    public string Left_Attack;

    [Header("Stats")]
    public int baseStamina;
    public float heavyAttackMultiplier;
    public float attackDamage;
    public float attackSpeed;
    public DamageType damageType;
    
    [Header("If Ranged")]
    public GameObject projectile;
}