using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();

            // Revert OnDrag Changes
            newItem.canvasGroup.blocksRaycasts = true;

            if(OriginalSlot.currentItem.currentAmount <= 1)
                GameManager.Instance.DropItem(newItem);
            else
                GameManager.Instance.DropItem(newItem, newItem.currentAmount);

            if(OriginalSlot.GetComponentInChildren<InventoryItem>() == null)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
            }
            else
            {
                OriginalSlot.isFull = true;
            }          
        }
    }
}