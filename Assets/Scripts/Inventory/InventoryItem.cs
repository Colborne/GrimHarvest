using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public Transform originalSlot;
    InputManager inputManager;
    public int itemID;
    [Range(1,100)]
    public int MaxAmount = 1;
    public int currentAmount = 1;
    public Text textAmount;
    public Item item;
    public float totalWeight = 0;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        textAmount = GetComponentInChildren<Text>();
        inputManager = FindObjectOfType<InputManager>();
    }

    public void Update() 
    {
        totalWeight = item.weight * currentAmount;
        if(textAmount != null)
        {
            if(currentAmount > 1)
                textAmount.text = currentAmount.ToString();
            else
                textAmount.text = "";
        }
    }

    public void WeightCalculation() 
    {
        totalWeight = item.weight * currentAmount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GameManager.Instance.interfaceCanvas.scaleFactor;
        gameObject.transform.SetParent(GameManager.Instance.draggables);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(currentAmount > 1)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();
            InventoryItem _item  = Instantiate(newItem, transform.position, transform.rotation);
            
            _item.currentAmount = (int)Mathf.Floor(currentAmount/2);
            _item.transform.SetParent(newItem.originalSlot);
            OriginalSlot.currentItem = _item;
            OriginalSlot.currentItem.rectTransform.localScale = new Vector3(1f, 1f, 1f);

            currentAmount -= _item.currentAmount;
        }
 
        canvasGroup.blocksRaycasts = false;
        rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        originalSlot = transform.parent.transform;  
    } 
    public void OnEndDrag(PointerEventData eventData)
    {
        // Revert State //
        canvasGroup.blocksRaycasts = true;
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        if (transform.parent == GameManager.Instance.draggables)
        {
            if(originalSlot.GetComponentInChildren<InventoryItem>() != null)
            {   
                int amount = originalSlot.GetComponentInChildren<InventoryItem>().currentAmount;
                Debug.Log("Adding " + amount + " to " + currentAmount);
                currentAmount += amount;
                Destroy(originalSlot.GetComponentInChildren<InventoryItem>().gameObject);    
            }
            transform.SetParent(originalSlot);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !eventData.dragging)
        {
            InventorySlot currentSlot = transform.parent.GetComponent<InventorySlot>();
        }
    }
}
