using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public int[] amounts;
    void Start()
    {
        for(int i = 0; i < amounts.Length; i++)
        {
            for(int j = 0; j < amounts[i]; j++)
            {
                Vector3 spawnPos = new Vector3((int)Random.Range(-3,21), .5f, (int)Random.Range(-9,9));
                if(CheckIfPosEmpty(spawnPos, "Stone") && CheckIfPosEmpty(spawnPos, "Grass"))
                {
                    var obstacle = Instantiate(prefabs[i], spawnPos, Quaternion.identity);
                    obstacle.transform.Rotate(new Vector3(obstacle.transform.rotation.x, Random.Range(0f,360f), obstacle.transform.rotation.z));
                }
            }
        }
    }

    public bool CheckIfPosEmpty(Vector3 targetPos, string tag)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return false;
        }
        return true;
    }
}