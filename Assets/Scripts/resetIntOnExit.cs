using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetIntOnExit : StateMachineBehaviour
{   public string targetInt;
    public int status;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(targetInt, status);
    }
}
