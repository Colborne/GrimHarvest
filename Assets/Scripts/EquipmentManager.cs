using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    GameManager gameManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    StatsManager stats;
    AnimatorManager animatorManager;

    public bool isTwoHanded = false;
    public float animSpeedControl = 1f;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponentInParent<StatsManager>();
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(weaponItem.isTwoHanded)
        {        
            animSpeedControl = weaponItem.attackSpeed;
            animatorManager.animator.SetFloat("animSpeed", animSpeedControl); 
            LoadRightWeaponDamageCollider();
            animatorManager.animator.SetBool("isTwoHanded", true);
        }
        else
        {
            animatorManager.animator.SetBool("isTwoHanded", false);
            if(isLeft)
                LoadLeftWeaponDamageCollider();
            else
            {
                LoadRightWeaponDamageCollider();
                animSpeedControl = weaponItem.attackSpeed;
                animatorManager.animator.SetFloat("animSpeed", animSpeedControl); 
            }
        }
    }
    #region Damage Colliders
    public void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = gameManager.spawnedShield.GetComponentInChildren<DamageCollider>();   
    }

    public void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = gameManager.spawnedWeapon.GetComponentInChildren<DamageCollider>();
    }

    public void OpenLeftDamageCollider()
    {  
        leftHandDamageCollider.EnableDamageCollider();
    }
    public void OpenRightDamageCollider()
    {  
        rightHandDamageCollider.EnableDamageCollider();
    }
    public void CloseLeftDamageCollider()
    {  
        leftHandDamageCollider.DisableDamageCollider();
    }
    public void CloseRightDamageCollider()
    {  
        rightHandDamageCollider.DisableDamageCollider();
    }
    #endregion

    public void DrainStaminaHeavy()
    {
        float multiplier = rightWeapon.heavyAttackMultiplier;
        stats.UseStamina(Mathf.RoundToInt(rightWeapon.baseStamina * multiplier));
    }
    public void DrainStaminaLight()
    {
        stats.UseStamina(Mathf.RoundToInt(rightWeapon.baseStamina));
    }
}
