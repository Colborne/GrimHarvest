using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Item : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    [Multiline()]
    public string description;
    
    public float weight;

    public virtual string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(description).AppendLine();
        builder.Append("Weight: ").Append(weight);
        
        return builder.ToString();
    }
}
