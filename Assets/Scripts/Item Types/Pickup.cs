using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pickup : MonoBehaviour
{
    public int itemID;
    public int amount;
    public string _name;
    InputManager inputManager;
    public bool autoPickup = false;
    public bool playerDropped = false;

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
        Invoke("ActivatePickup", 1f);
    }

    void ActivatePickup()
    {
        if(!playerDropped)
            autoPickup = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.Instance.PM.gameObject) 
        {
            if(inputManager.interactInput || autoPickup)
            {   
                inputManager.interactInput = false;
                GameManager.Instance.PickUpItem(itemID, amount);
                Destroy(gameObject);
                return;
            }
        }
    }
}
