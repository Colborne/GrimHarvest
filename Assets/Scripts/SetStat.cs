using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetStat : MonoBehaviour
{
    public Slider[] slider;
    TMP_Text[] text;
    float animSpeed;
    float wlkSpd;
    float rollSpd;
    MovementManager stats;
    // Start is called before the first frame update
    void Awake()
    {
        text = new TMP_Text[3];
        stats = FindObjectOfType<MovementManager>();
        text[0] = slider[0].GetComponentInChildren<TMP_Text>();
        text[1] = slider[1].GetComponentInChildren<TMP_Text>();
        text[2] = slider[2].GetComponentInChildren<TMP_Text>();
        slider[0].value = stats.anim;
        slider[1].value = stats.rollSpeed;
        slider[2].value = stats.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        stats.anim = slider[0].value;
        text[0].text = slider[0].value.ToString();
        stats.rollSpeed = slider[1].value;
        text[1].text = slider[1].value.ToString();
        stats.movementSpeed = slider[2].value;
        text[2].text = slider[2].value.ToString();
    }
}
