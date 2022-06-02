using UnityEngine;
public class FarmManager : MonoBehaviour
{    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public GameObject[] plant;
    public GameObject player;
    public int currentPlant;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        int delta = 0;
        if(inputManager.scrollInput > 0)
            delta = 1;
        else if(inputManager.scrollInput < 0)
            delta = -1;
        
        if((currentPlant < plant.Length - 1 && delta == 1) || (currentPlant > 0 && delta == -1))
            currentPlant += delta;
        //RaycastHit hitInfo;
        //Ray ray = Camera.main.ScreenPointToRay(inputManager.mouseInput);
         
        placement.position = grid.GetNearestPointOnGrid(player.transform.position + player.transform.forward);
        placement.rotation = Quaternion.identity;
        placement.GetComponent<MeshFilter>().sharedMesh = plant[currentPlant].GetComponent<MeshFilter>().sharedMesh;
        if (inputManager.interactInput)
        {
            inputManager.interactInput = false;
            if(checkIfPosEmpty(placement.position))
                Plant(player.transform.position + player.transform.forward);
        }
    }

    private void Plant(Vector3 clickPoint)
    {
        var placed = Instantiate(plant[currentPlant]);
        placed.transform.position = grid.GetNearestPointOnGrid(clickPoint);
        placed.transform.Rotate(new Vector3(placed.transform.rotation.x, Random.Range(0f,360f), placed.transform.rotation.z));
    }

    public bool checkIfPosEmpty(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Plant");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
                return false;
        }
        return true;
    }
}