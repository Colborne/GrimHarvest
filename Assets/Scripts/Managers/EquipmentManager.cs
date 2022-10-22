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
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponentInParent<StatsManager>();
        
    }

    void Start()
    {
        gameManager.LoadItem(0, "Weapon");
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
}