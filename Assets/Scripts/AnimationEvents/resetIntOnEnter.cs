﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetIntOnEnter : StateMachineBehaviour
{   public string targetInt;
    public int status;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(targetInt, status);
    }
}