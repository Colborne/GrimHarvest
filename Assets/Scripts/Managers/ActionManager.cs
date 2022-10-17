using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    InputManager inputManager;
    AnimatorManager animatorManager;
    StatsManager statsManager;

    void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        statsManager = GetComponent<StatsManager>();
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
                    animatorManager.animator.CrossFade("Hit1", .2f);
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
                animatorManager.animator.CrossFade("Heavy Attack", .2f);
            }
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