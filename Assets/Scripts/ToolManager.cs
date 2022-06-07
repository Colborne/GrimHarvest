using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    InputManager inputManager;
    FarmManager farmManager;
    ShovelManager shovelManager;
    PickaxeManager pickaxeManager;
    WaterManager waterManager;
    AnimatorManager animatorManager;
    public int currentTool;
    public Image[] tools;
    public GameObject[] toolModel;
    public Mesh Placement;
    public Material Select;

    void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        farmManager = GetComponent<FarmManager>();
        shovelManager = GetComponent<ShovelManager>();
        waterManager = GetComponent<WaterManager>();
        pickaxeManager = GetComponent<PickaxeManager>();
    }
    void Update()
    {
        if(inputManager.toolbar1Input) currentTool = 0;
        else if(inputManager.toolbar2Input) currentTool = 1;
        else if(inputManager.toolbar3Input) currentTool = 2;
        else if(inputManager.toolbar4Input) currentTool = 3;
        else if(inputManager.toolbar5Input) currentTool = 4;

        for(int i = 0; i < 5; i++)
        {
            if(i == currentTool)
                tools[i].color = new Color32(255,255,255,255);
            else
                tools[i].color = new Color32(255,255,255,128);
        }

        SelectTool(currentTool);

        if(inputManager.isInteracting)
        {
            for(int i = 0; i < 5; i++)
            {
                if(i == currentTool)
                    toolModel[i].SetActive(true);
                else
                    toolModel[i].SetActive(false);
            }
        }
    }

    void SelectTool(int tool)
    {
        if(tool == 0) //Soil
        {
            farmManager.enabled = false;
            shovelManager.enabled = true;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;

            shovelManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            shovelManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
        else if(tool == 1) //Plant
        {
            farmManager.enabled = true;
            shovelManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;

            farmManager.placement.GetComponent<MeshRenderer>().material = farmManager.mat;
        }
        else if(tool == 2) //Water
        {
            farmManager.enabled = false;
            shovelManager.enabled = false;
            waterManager.enabled = true;
            pickaxeManager.enabled = false;

            waterManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            waterManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
        else if(tool == 3)        
        {
            farmManager.enabled = false;
            shovelManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;
        }
        else if(tool == 4) //Pickaxe
        {
            farmManager.enabled = false;
            shovelManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = true;

            pickaxeManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            pickaxeManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
    }

    public void UseTool()
    {
        if(currentTool == 0)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Hoe", .2f);
        }
        else if(currentTool == 1)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Plant", .2f);
        }
        else if(currentTool == 2)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Axe", .2f);
        }
        else if(currentTool == 3)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Axe", .2f);
        }
        else if(currentTool == 4)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Overhead", .2f);
        }
    }
}
