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
        //ui = FindObjectOfType<InteractableUI>();
        //_name = GetComponent<SaveableObject>().AssetPath.Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
        Invoke("ActivatePickup", 1f);
    }

    void ActivatePickup()
    {
        if(!playerDropped)
            autoPickup = true;
    }
}
