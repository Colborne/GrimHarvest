using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetOnExit : StateMachineBehaviour
{   public string targetBool;
    public bool status;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
    }
}
