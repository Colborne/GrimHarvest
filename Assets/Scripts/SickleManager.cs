using UnityEngine;
public class SickleManager : MonoBehaviour
{    
    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public GameObject player;
    public Mesh Select;

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
            Harvest(CheckIfCrop(placement.position));
        }
    }

    private void Harvest(GameObject stone)
    {   
        if(stone != null){
            Destroy(stone);
        }
    }

    public GameObject CheckIfCrop(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Plant");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return current;
        }
        return null;
    }
}