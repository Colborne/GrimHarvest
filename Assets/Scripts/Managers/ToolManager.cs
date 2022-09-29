using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    InputManager inputManager;
    AnimatorManager animatorManager;

    void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
    }
    public void UseTool()
    {
        //if(animatorManager.animator.GetFloat("V") > 1.25f)
            //animatorManager.animator.CrossFade("Jump smash", .2f);

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
