using UnityEngine;
public class PickaxeManager : MonoBehaviour
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
            BreakStone(CheckIfStone(placement.position));
        }
    }

    private void BreakStone(GameObject stone)
    {   
        if(stone != null){
            Destroy(stone);
        }
    }

    public GameObject CheckIfStone(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Stone");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return current;
        }
        return null;
    }
}