using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "Items/Plantable Item")]
public class PlantableItem : Item
{
    public int growTime;
    public FarmableObject crop;
}
