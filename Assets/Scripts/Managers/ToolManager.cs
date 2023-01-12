using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    InputManager inputManager;
    FarmManager farmManager;
    HoeManager hoeManager;
    PickaxeManager pickaxeManager;
    WaterManager waterManager;
    SickleManager sickleManager;
    FishManager fishManager;
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
        hoeManager = GetComponent<HoeManager>();
        waterManager = GetComponent<WaterManager>();
        pickaxeManager = GetComponent<PickaxeManager>();
        sickleManager = GetComponent<SickleManager>();
        fishManager = GetComponent<FishManager>();
    }
    void Update()
    {
        if(inputManager.toolbar1Input) currentTool = 0;
        else if(inputManager.toolbar2Input) currentTool = 1;
        else if(inputManager.toolbar3Input) currentTool = 2;
        else if(inputManager.toolbar4Input) currentTool = 3;
        else if(inputManager.toolbar5Input) currentTool = 4;
        else if(inputManager.toolbar6Input) currentTool = 5;
        else if(inputManager.toolbar7Input) currentTool = 6;

        for(int i = 0; i < 7; i++)
        {
            if(i == currentTool)
                tools[i].color = new Color32(255,255,255,255);
            else
                tools[i].color = new Color32(255,255,255,128);
        }

        SelectTool(currentTool);

        if(inputManager.isInteracting)
        {
            toolModel[currentTool].SetActive(true);
        }
        else
        {
            for(int i = 0; i < 6; i++)
                toolModel[i].SetActive(false);
        }
            
    }

    void SelectTool(int tool)
    {
        if(tool == 0) //Soil
        {
            farmManager.enabled = false;
            hoeManager.enabled = true;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;
            fishManager.enabled = false;
            sickleManager.enabled = false;

            hoeManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            hoeManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
        else if(tool == 1) //Plant
        {
            farmManager.enabled = true;
            hoeManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;
            fishManager.enabled = false;
            sickleManager.enabled = false;

            farmManager.placement.GetComponent<MeshRenderer>().material = farmManager.mat;
        }
        else if(tool == 2) //Water
        {
            farmManager.enabled = false;
            hoeManager.enabled = false;
            waterManager.enabled = true;
            pickaxeManager.enabled = false;
            sickleManager.enabled = false;
            fishManager.enabled = false;

            waterManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            waterManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
        else if(tool == 3) //Sickle      
        {
            farmManager.enabled = false;
            hoeManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;
            sickleManager.enabled = true;
            fishManager.enabled = false;

            sickleManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            sickleManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
        else if(tool == 4) //Axe      
        {
            farmManager.enabled = false;
            hoeManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;
            sickleManager.enabled = false;
            fishManager.enabled = false;
        }
        else if(tool == 5) //Pickaxe
        {
            farmManager.enabled = false;
            hoeManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = true;
            sickleManager.enabled = false;
            fishManager.enabled = false;

            pickaxeManager.placement.GetComponent<MeshFilter>().sharedMesh = Placement;
            pickaxeManager.placement.GetComponent<MeshRenderer>().material = Select;
        }
        else if(tool == 6) //Fishing Rod
        {
            farmManager.enabled = false;
            hoeManager.enabled = false;
            waterManager.enabled = false;
            pickaxeManager.enabled = false;
            sickleManager.enabled = false;
            fishManager.enabled = true;

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
            animatorManager.animator.CrossFade("Hoe", .2f);
        }
        else if(currentTool == 3)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Axe", .2f);
        }
        else if(currentTool == 4)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Axe", .2f);
        }
        else if(currentTool == 5)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Hoe", .2f);
        }
        else if(currentTool == 6)
        {
            animatorManager.animator.SetBool("isInteracting", true);
            animatorManager.animator.CrossFade("Hoe", .2f);
        }
    }
}
