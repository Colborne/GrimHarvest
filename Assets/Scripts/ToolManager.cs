using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    InputManager inputManager;
    FarmManager farmManager;
    ShovelManager shovelManager;
    WaterManager waterManager;
    public int currentTool;
    public Image[] tools;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        farmManager = GetComponent<FarmManager>();
        shovelManager = GetComponent<ShovelManager>();
        waterManager = GetComponent<WaterManager>();
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
    }

    void SelectTool(int tool)
    {
        if(tool == 0)
        {
            farmManager.enabled = true;
            shovelManager.enabled = false;
            waterManager.enabled = false;
        }
        else if(tool == 1)
        {
            farmManager.enabled = false;
            shovelManager.enabled = false;
            waterManager.enabled = true;
        }
        else if(tool == 2)
                {
            farmManager.enabled = false;
            shovelManager.enabled = true;
            waterManager.enabled = false;
        }
        else if(tool == 3)        
        {
            farmManager.enabled = false;
            shovelManager.enabled = false;
            waterManager.enabled = false;
        }
        else if(tool == 4)        
        {
            farmManager.enabled = false;
            shovelManager.enabled = false;
            waterManager.enabled = false;
        }
    }
}
