using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystem : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] Transform Farmer;
    [SerializeField] GameObject FarmWindow;
    [SerializeField] Farmable[] farmables;
    public int iteration = 0;
    public LayerMask ground;
    RaycastHit Hit;
    Quaternion orientation;
    InputManager input;
    int farmRotation = 0;
    public int last = -1;
    public float distance = 22.5f;

    private void Start() 
    {
        input = FindObjectOfType<InputManager>();
    }
/*
    private void Update() 
    {
        if(input.scrollInput > 0)
        {
            farmRotation++;
            input.scrollInput = 0;
        }
        else if(input.scrollInput < 0)
        {
            farmRotation--;
            input.scrollInput = 0;
        }
        
        if(farmRotation > 16)
            farmRotation = -16;
        if(farmRotation < -16)
            farmRotation = 16;

        if(input.rightMouseInput && !input.inventoryFlag )
        {
            input.rightMouseInput = !input.rightMouseInput;
            CloseWindow();
        }

        if(iteration != last)
        {
            Farmer.GetComponent<MeshFilter>().mesh = farmables[iteration].mesh;
            orientation = farmables[iteration].orientation;
            Farmer.localScale = farmables[iteration].prefab.transform.localScale;
            last = iteration;
        }

        if(Physics.Raycast(Cam.position, Cam.forward, out Hit, distance, ground))
        {     
            Farmer.position = new Vector3(
            Hit.point.x,
            Hit.point.y, // + Farmer.localScale.y,
            Hit.point.z);

            orientation = Quaternion.Euler(0, 45 * farmRotation, 0);
            
            Farmer.eulerAngles = orientation.eulerAngles; //new Vector3(0,Mathf.RoundToInt(Cam.eulerAngles.y) != 0 ? Mathf.RoundToInt(Cam.eulerAngles.y / 90f) * 90 : 0, 0) + orientation.eulerAngles;           

            if(input.leftMouseInput && !input.inventoryFlag && !input.farmWindowFlag)
            {
                input.leftMouseInput = false;

                if (checkIfPosEmpty(Farmer.position, Farmer.rotation) 
                && GameManager.Instance.CraftingCheck(farmables[iteration].seed, new int[1] {1}))
                {
                    Instantiate(farmables[iteration].prefab, Farmer.position, Farmer.rotation);
                    GameManager.Instance.Craft(farmables[iteration].seed, new int[1] {1});
                }
            }
        }
    }
    */
    public bool checkIfPosEmpty(Vector3 targetPos, Quaternion targetRot)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Build");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos && current.transform.rotation == targetRot)
                return false;
        }
        return true;
    }

    public void CloseWindow()
    {
        FarmWindow.SetActive(!FarmWindow.active);
    }
}