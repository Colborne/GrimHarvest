using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class Farmable : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem[] seed;
    public GameObject prefab;
    public Mesh mesh;
    public int value;
    public Quaternion orientation;

    FarmSystem buildSystem;

    private void Awake() {
        mesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;  
        buildSystem = FindObjectOfType<FarmSystem>();
    }
    
    private void Start()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(seed[0].item.itemName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            buildSystem.iteration = value;
            buildSystem.CloseWindow();
        }  
    }      
}
