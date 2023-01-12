using UnityEngine;
using UnityEngine.UI;
 
public class DrawLine : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform object1;
    public RectTransform object2;

    void Update()
    {
        float distance = Vector2.Distance(object1.localPosition, object2.localPosition);
        rect.sizeDelta = new Vector2(1, distance);
        rect.LookAt(object2, Vector3.up);
    }
}