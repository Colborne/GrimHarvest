using UnityEngine;
public class ShovelManager : MonoBehaviour
{    
    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public GameObject soil;
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
            if(checkIfPosEmpty(placement.position))
                Dig(player.transform.position + player.transform.forward);
        }
    }

    private void Dig(Vector3 clickPoint)
    {
        var placed = Instantiate(soil);
        placed.transform.position = grid.GetNearestPointOnGrid(clickPoint);
    }

    public bool checkIfPosEmpty(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Ground");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return false;
        }
        return true;
    }
}