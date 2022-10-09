using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    StatsManager statsManager;

    void Awake()
    {
        statsManager = GetComponent<StatsManager>();
    }

    void Update()
    {
        statsManager.RegenerateStamina();
    }
}
