using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    GameManager gameManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public DamageCollider rightHandDamageCollider;
    public DamageCollider leftHandDamageCollider;
    StatsManager stats;
    AnimatorManager animatorManager;
    public int weaponToLoad;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponentInParent<StatsManager>();
    }

    void Start()
    {
        gameManager.LoadItem(weaponToLoad, "Weapon");
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(weaponItem.isTwoHanded)
        {
            LoadRightWeaponDamageCollider();
            //animatorManager.animator.SetBool("isTwoHanded", true);
        }
        else
        {
            //animatorManager.animator.SetBool("isTwoHanded", false);
            if(isLeft)
                LoadLeftWeaponDamageCollider();
            else
                LoadRightWeaponDamageCollider();      
        }
    }
    #region Damage Colliders
    public void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = gameManager.spawnedShield.GetComponent<DamageCollider>();   
    }

    public void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = gameManager.spawnedWeapon.GetComponent<DamageCollider>();
        rightHandDamageCollider.damage = (int)rightWeapon.damage;
    }

    public void OpenLeftDamageCollider()
    {  
        if(leftHandDamageCollider != null) leftHandDamageCollider.EnableDamageCollider();
    }
    public void OpenRightDamageCollider()
    {  
        if(rightHandDamageCollider != null) rightHandDamageCollider.EnableDamageCollider();
    }
    public void CloseLeftDamageCollider()
    {  
        if(leftHandDamageCollider != null) leftHandDamageCollider.DisableDamageCollider();
    }
    public void CloseRightDamageCollider()
    {  
        if(rightHandDamageCollider != null) rightHandDamageCollider.DisableDamageCollider();
    }
    #endregion
}