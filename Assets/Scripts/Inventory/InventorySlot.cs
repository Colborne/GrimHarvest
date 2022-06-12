using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem currentItem;
    public bool isFull = false;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();

            // Original Slot != This Slot //
            if (newItem.originalSlot != this.transform)
            {
                // Slot == Full //
                if (isFull)
                {
                    //Stack
                    if(currentItem.itemID == newItem.itemID)
                    {
                        // Filling This Slot
                        int remainder = currentItem.currentAmount + newItem.currentAmount;
                        if(remainder <= currentItem.MaxAmount)
                        {
                            currentItem.currentAmount += newItem.currentAmount;
                            eventData.pointerDrag.transform.SetParent(gameObject.transform);
                            currentItem.originalSlot = this.transform;
                        }
                        else
                        {
                            currentItem.currentAmount = currentItem.MaxAmount;
                            remainder -= currentItem.currentAmount;
                            Debug.Log("Stack Rounding");
                            GameManager.Instance.StackRounding(newItem.itemID, remainder, OriginalSlot.currentItem.originalSlot);
                        }

                        if(OriginalSlot.GetComponentInChildren<InventoryItem>() == null)
                        {
                            // Emptying Original Slot
                            OriginalSlot.isFull = false;
                            OriginalSlot.currentItem = null;
                        }
                        Destroy(newItem.gameObject);
                    }
                    else
                    {
                        Swap(newItem, OriginalSlot);

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;
                    }
                }
                // Slot != Full //
                else if (!isFull)
                {
                    // Moving Into Weapon Slot
                    
                        if(OriginalSlot.GetComponentInChildren<InventoryItem>() == null)
                        {
                            // Emptying Original Slot
                            OriginalSlot.isFull = false;
                            OriginalSlot.currentItem = null;
                        }

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;
                }
            }
        }
    }
    public void Swap(InventoryItem newItem, InventorySlot OriginalSlot) 
    {
        // Swapping InventoryItem:currentItem 
        InventoryItem swapCurrent = currentItem;
        currentItem = newItem;
        OriginalSlot.currentItem = swapCurrent;            
    }
}