using UnityEngine;
public class HoeManager : MonoBehaviour
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
            if(CheckIfPosEmpty(placement.position, "Ground") 
            && CheckIfPosEmpty(placement.position, "Plant") 
            && CheckIfPosEmpty(placement.position, "Stone") 
            && CheckIfPosEmpty(placement.position, "Soil"))
                Dig(player.transform.position + player.transform.forward);
        }
    }

    private void Dig(Vector3 clickPoint)
    {
        var placed = Instantiate(soil);
        placed.transform.position = grid.GetNearestPointOnGrid(clickPoint);
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