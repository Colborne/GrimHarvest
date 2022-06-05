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
            Water(CheckIfSoil(placement.position));
        }
    }

    private void Water(GameObject obj)
    {
        if(obj != null){
            obj.GetComponent<MeshRenderer>().material = watered;
            obj.GetComponent<Soil>().watered = true;
        }
    }

    public GameObject CheckIfSoil(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Soil");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return current;
        }
        return null;
    }
}