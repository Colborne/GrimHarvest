using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    InputManager inputManager;
    AnimatorManager animatorManager;
    StatsManager statsManager;
    EquipmentManager equipmentManager;
    void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        statsManager = GetComponent<StatsManager>();
        equipmentManager = GetComponent<EquipmentManager>();
    }

    public void Use()
    {
        if(statsManager.currentStamina > statsManager.actionCost)
        {
            statsManager.heavyModifier = 1;
            statsManager.UseStamina(equipmentManager.rightWeapon.lightCost);
            animatorManager.animator.SetInteger("random", Random.Range(0,3));
            
            if(animatorManager.animator.GetInteger("combo") == -1)
            {
                    animatorManager.animator.SetInteger("combo", 0);
                    animatorManager.animator.SetBool("isInteracting", true);
                    animatorManager.animator.CrossFade(equipmentManager.rightWeapon.Light_Attack, .2f);
            }
            else
                animatorManager.animator.SetInteger("combo", animatorManager.animator.GetInteger("combo") + 1);
        }
    }

    public void UseHeavy()
    {
        if(statsManager.currentStamina > statsManager.actionCost)
        {
            statsManager.heavyModifier = 1.5f;
            statsManager.UseStamina(equipmentManager.rightWeapon.heavyCost);
            if(animatorManager.animator.GetInteger("combo") == -1)
            {
                animatorManager.animator.SetInteger("combo", 0);
                animatorManager.animator.SetBool("isInteracting", true);
                animatorManager.animator.CrossFade(equipmentManager.rightWeapon.Heavy_Attack, .2f);
            }
            else
                animatorManager.animator.SetInteger("combo", animatorManager.animator.GetInteger("combo") + 1);
        }
    }

        public void UseLeft()
    {
        if(statsManager.currentStamina > statsManager.actionCost)
        {
            statsManager.heavyModifier = 1;
            statsManager.UseStamina(equipmentManager.leftWeapon.lightCost);
            animatorManager.animator.SetInteger("random", Random.Range(0,3));
            
            if(animatorManager.animator.GetInteger("combo") == -1)
            {
                    animatorManager.animator.SetInteger("combo", 0);
                    animatorManager.animator.SetBool("isInteracting", true);
                    animatorManager.animator.CrossFade(equipmentManager.leftWeapon.Left_Attack, .2f);
            }
            else
                animatorManager.animator.SetInteger("combo", animatorManager.animator.GetInteger("combo") + 1);
        }
    }

    public void UseSpecial()
    {
        if(statsManager.currentStamina > statsManager.actionCost)
        {
            statsManager.heavyModifier = 1.5f;
            statsManager.UseStamina(equipmentManager.rightWeapon.heavyCost);
            if(animatorManager.animator.GetInteger("combo") == -1)
            {
                animatorManager.animator.SetInteger("combo", 0);
                animatorManager.animator.SetBool("isInteracting", true);
                animatorManager.animator.CrossFade(equipmentManager.rightWeapon.Special_Attack, .2f);
            }
            else
                animatorManager.animator.SetInteger("combo", animatorManager.animator.GetInteger("combo") + 1);
        }
    }

    public void Roll()
    {
        if(statsManager.currentStamina >= statsManager.rollCost)
        {
            statsManager.UseStamina(15f);
            animatorManager.animator.CrossFade("Roll", .2f);
            animatorManager.animator.SetInteger("combo", 0);
        }
    }
}