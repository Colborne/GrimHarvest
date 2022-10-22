using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ingameEquipment
{
    public int ID;
    public string name = "Items";
    public GameObject prefab;
    public GameObject inventoryItem;
    public GameObject worldItem;
}

public class GameManager : MonoBehaviour
{
    public enum ItemType
    {
        MainHand,
        OffHand,
        Helmet,
        Chest
    }

    public static GameManager Instance;
    public InventorySlot[] inventorySlots;
    public Canvas interfaceCanvas;
    public Transform draggables;
    public PlayerModelManager PM;
    public ingameEquipment[] equipment;
    public int weaponID = -1;
    public int shieldID = -1;
    public int helmetID = -1;
    public int chestID = -1;
    public GameObject spawnedWeapon;
    public GameObject spawnedShield;
    public GameObject spawnedHelmet;
    public GameObject spawnedChest;
    public EquipmentManager equipmentManager;
    StatsManager statsManager;

    // Main //
    void Awake()
    {
        GameManager.Instance = this;
        equipmentManager = FindObjectOfType<EquipmentManager>();
        statsManager = FindObjectOfType<StatsManager>();
    }


    public void DestroyItem(GameObject item)
    {
        if (item != null)
        {
            Destroy(item);
        }
    }

    public void SpawnItem(string type, GameObject item)
    {
        DestroyItem(item);

        if(type == "Weapon" && weaponID != -1)
        {
            spawnedWeapon = Instantiate(equipment[weaponID].prefab, PM.rightHand);
            equipmentManager.rightWeapon = spawnedWeapon.GetComponent<WeaponItemLoader>().item;
            equipmentManager.LoadWeaponOnSlot(equipmentManager.rightWeapon, false);
        }

        if(type == "Shield" && shieldID != -1)
        {
            spawnedShield = Instantiate(equipment[shieldID].prefab, PM.leftHand);
            equipmentManager.leftWeapon = spawnedShield.GetComponent<WeaponItemLoader>().item;
            equipmentManager.LoadWeaponOnSlot(equipmentManager.leftWeapon, true);
        }

        if(type == "Helmet" && helmetID != -1)
        {
            spawnedHelmet = Instantiate(equipment[helmetID].prefab, PM.helmet);
        }
         
        if(type == "Chest" && chestID != -1)
        {
            spawnedChest = Instantiate(equipment[chestID].prefab, PM.chest);
        }
    }

    public void PickupItem(int itemID)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isFull)
            {
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                break;
            }
        }
    }
    public void PickUpItem(int itemID, int quantityIncrease)
    {
        // Searches for identical item ID in inventory //
        for (int i = 0; i < inventorySlots.Length; i++)
        {    
            //Found a full slot and the id matches
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == itemID)
            {
                Debug.Log(i + ": Current Amount: " + inventorySlots[i].currentItem.currentAmount + " this " + quantityIncrease);
                int newAmount = inventorySlots[i].currentItem.currentAmount + quantityIncrease;
                if(inventorySlots[i].currentItem.MaxAmount >= newAmount)
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " >= " + newAmount);

                    inventorySlots[i].currentItem.currentAmount += quantityIncrease;
                    return;
                }
                else
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " < " + newAmount);
                    inventorySlots[i].currentItem.currentAmount = inventorySlots[i].currentItem.MaxAmount;

                    quantityIncrease = newAmount - inventorySlots[i].currentItem.MaxAmount;
                    Debug.Log("Remainder that should be rounded over: " + quantityIncrease);          
                }
            }
            //Found a full slot but the id does not match
            else if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID != itemID)
            {
                Debug.Log("Can't Place here");
            }
            //Found an empty slot
            else if (!inventorySlots[i].isFull)
            { 
                Debug.Log("Empty Slot");   
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                GO.GetComponent<InventoryItem>().originalSlot = inventorySlots[i].transform;
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                inventorySlots[i].currentItem.currentAmount = quantityIncrease;

                if(inventorySlots[i].currentItem.MaxAmount >= inventorySlots[i].currentItem.currentAmount)
                    return;
                else
                {
                    Debug.Log(inventorySlots[i].currentItem.MaxAmount + " < " + quantityIncrease);
                    inventorySlots[i].currentItem.currentAmount = inventorySlots[i].currentItem.MaxAmount;

                    quantityIncrease -= inventorySlots[i].currentItem.MaxAmount;
                    Debug.Log("Remainder that should be rounded over: " + quantityIncrease);      
                }
            }
            //No Slots Left
            else
            {
                Debug.Log("noslot");
                //var obj = Instantiate(equipment[itemID].worldItem, PM.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
                //obj.GetComponent<Pickup>().amount = quantityIncrease;
                return;
            }
        }
    }
    public void ClearItem(InventoryItem item)
    {
        Destroy(item.gameObject);
    }
    public void DropItem(InventoryItem item)
    {
        var drop = Instantiate(equipment[item.itemID].worldItem, PM.transform.position + PM.transform.forward * 4 + PM.transform.up * 2, Quaternion.identity);
        drop.GetComponent<Pickup>().playerDropped = true;
        Destroy(item.gameObject);
    }
    public void DropItem(InventoryItem item, int _amount)
    {
        var _newItem = Instantiate(equipment[item.itemID].worldItem, PM.transform.position + PM.transform.forward * 4 + PM.transform.up * 2, Quaternion.identity);
        _newItem.GetComponent<Pickup>().amount = _amount;
        _newItem.GetComponent<Pickup>().playerDropped = true;
        Destroy(item.gameObject);
    }
    public void StackRounding(int itemID, int quantityIncrease, Transform originalSlot)
    {
        // Searches for identical item ID in inventory //
        for (int i = 0; i < inventorySlots.Length; i++)
        {    
            if (!inventorySlots[i].isFull)
            { 
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                GO.GetComponent<InventoryItem>().originalSlot = inventorySlots[i].transform;
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                inventorySlots[i].currentItem.currentAmount = quantityIncrease;
                return;
            }
        }
    }
    public bool CheckInventoryForItem(InventoryItem item, int amount, bool remove)
    {
        int amountFound = 0;
        List<int> foundSlots = new List<int>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == item.itemID)
            {
                foundSlots.Add(i);
                amountFound += inventorySlots[i].currentItem.currentAmount; 
            }
            
            if(amountFound >= amount)
                break;     
        }

        if(amountFound < amount)
            return false;

        foreach(int i in foundSlots)
        {
            amountFound -= inventorySlots[i].currentItem.currentAmount;
            
            if(remove)
            {
                if(inventorySlots[i].currentItem.currentAmount > amount)
                    inventorySlots[i].currentItem.currentAmount -= amount;
                else
                    inventorySlots[i].currentItem.currentAmount = 0;

                if(inventorySlots[i].currentItem.currentAmount == 0)
                {
                    inventorySlots[i].currentItem = null;
                    inventorySlots[i].isFull = false;
                    Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
                }   
            }

            if(amountFound <= 0)
                return true;
        }
        return false;
    }
    public int ReplaceStack(InventoryItem item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == item.itemID)
            {
                int amount = inventorySlots[i].currentItem.currentAmount;
                inventorySlots[i].currentItem = null;
                inventorySlots[i].isFull = false;
                Destroy(inventorySlots[i].transform.GetChild(0).gameObject);
                return amount;
            }
        }
        return 0;
    }
    public bool CraftingCheck(InventoryItem[] items, int[] amounts) 
    {
        //Checks Entire Inventory For Possible Craftable Items
        for(int x = 0; x < items.Length; x++)
        {
            int amountFound = 0;
            for (int i = 10; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == items[x].itemID)
                    amountFound += inventorySlots[i].currentItem.currentAmount; 
            }

            if(amountFound < amounts[x])
                return false;     
        }
        return true;
    }
    public int CheckAmount(InventoryItem item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isFull && inventorySlots[i].currentItem.itemID == item.itemID)
            {
                return inventorySlots[i].currentItem.currentAmount;
            }
        }
        return 0;
    }
    public void Craft(InventoryItem[] items, int[] amounts) 
    {
        //Checks Entire Inventory For Possible Craftable Items
        for(int x = 0; x < items.Length; x++)
        {
            CheckInventoryForItem(items[x], amounts[x], true);
        }
    }
    public bool CheckIfEmpty()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isFull)
            {
                return true;
            }
        }
        return false;
    }
    public void LoadItem(int itemID, string type)
    {
        if(type == "Weapon")
        {
            weaponID = itemID;
            spawnedWeapon = Instantiate(equipment[weaponID].prefab, PM.rightHand);
            equipmentManager.rightWeapon = spawnedWeapon.GetComponent<WeaponItemLoader>().item;
            equipmentManager.LoadWeaponOnSlot(equipmentManager.rightWeapon, false);
        }

        if(type == "Shield" )
        {
            shieldID = itemID;
            spawnedShield = Instantiate(equipment[shieldID].prefab, PM.leftHand);
            equipmentManager.leftWeapon = spawnedShield.GetComponent<WeaponItemLoader>().item;
            equipmentManager.LoadWeaponOnSlot(equipmentManager.leftWeapon, true);
        }

        if(type == "Helmet")
        {
            helmetID = itemID;
            spawnedHelmet = Instantiate(equipment[helmetID].prefab, PM.helmet);
        }
         
        if(type == "Chest")
        {
            chestID = itemID;
            spawnedChest = Instantiate(equipment[chestID].prefab, PM.chest);
        }
    }
}