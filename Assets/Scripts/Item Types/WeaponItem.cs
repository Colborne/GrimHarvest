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

    [Header("Attack Animations")]
    public string Light_Attack;
    public string Heavy_Attack;
    public string Left_Attack;
    public string Special_Attack;

    [Header("Stats")]
    public int lightCost;
    public int heavyCost;
    public float damage;
}