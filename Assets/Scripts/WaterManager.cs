using UnityEngine;
public class WaterManager : MonoBehaviour
{    
    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public Material watered;
    public GameObject player;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        placement.position = grid.GetNearestPointOnGrid(player.transform.position + player.transform.forward);
        placement.rotation = Quaternion.identity;
        
        if (inputManager.interactInput)
        {
            inputManager.interactInput = false;
            if(CheckIfSoil(placement.position))
                Water(player.transform.position + player.transform.forward);
        }
    }

    private void Water(Vector3 clickPoint)
    {
        placement.GetComponent<MeshRenderer>().material = watered;
    }

    public bool CheckIfSoil(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Soil");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return true;
        }
        return false;
    }
}