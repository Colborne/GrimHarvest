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
            statsManager.heavyModifier = 2;
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

    public void Roll()
    {
        if(statsManager.currentStamina >= statsManager.rollCost)
        {
            animatorManager.animator.CrossFade("Roll", .2f);
            animatorManager.animator.SetInteger("combo", 0);
        }
    }
}