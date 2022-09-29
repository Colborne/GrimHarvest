using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    InputManager inputManager;
    int horizontal;
    int vertical;
    float tempV = 1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        horizontal = Animator.StringToHash("H");
        vertical = Animator.StringToHash("V");
    }
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontal;
        float snappedVertical;

        snappedHorizontal = horizontalMovement;

        if(verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if(verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -.55f)
        {
            snappedVertical = -0.5f;
        }
        else if(verticalMovement < -0.55f)
        {
            snappedVertical = -1;   
        }
        else
        {
            snappedVertical = 0;
        }

        if(inputManager.sprintInput && inputManager.animatorManager.animator.GetInteger("combo") == -1 && snappedVertical != 0)
        {
            tempV += .01f;
            snappedVertical = Mathf.Min(tempV, 2f);
        }
        else
            tempV = 1f;

        animator.SetFloat(horizontal, snappedHorizontal);//, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
