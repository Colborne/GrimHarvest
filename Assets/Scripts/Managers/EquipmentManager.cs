using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameManager gameManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public DamageCollider rightHandDamageCollider;
    public DamageCollider leftHandDamageCollider;
    StatsManager stats;
    AnimatorManager animatorManager;
    public int weaponToLoad;
    public int weaponLeftToLoad;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponentInParent<StatsManager>();
    }

    void Start()
    {
        gameManager.LoadItem(weaponToLoad, "Weapon");
        gameManager.LoadItem(weaponLeftToLoad, "Shield");
        gameManager.LoadItem(6, "Helmet");
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(isLeft)
            LoadLeftWeaponDamageCollider();
        else
            LoadRightWeaponDamageCollider();      
    }

    #region Damage Colliders
    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = gameManager.spawnedShield.GetComponent<DamageCollider>();
        leftHandDamageCollider.damage = (int)leftWeapon.damage;   
    }

    private void LoadRightWeaponDamageCollider()
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