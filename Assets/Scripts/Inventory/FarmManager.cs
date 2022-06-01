using UnityEngine;
public class FarmManager : MonoBehaviour
{    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public GameObject plant;
    public GameObject player;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        //RaycastHit hitInfo;
        //Ray ray = Camera.main.ScreenPointToRay(inputManager.mouseInput);
        //Ray ray = Camera.main.ScreenPointToRay(player.transform.forward + new Vector3(1,0,0));

        //if (Physics.Raycast(ray, out hitInfo))
        //{
            //placement.position = grid.GetNearestPointOnGrid(hitInfo.point) + new Vector3(0,.5f,0);            
            placement.position = grid.GetNearestPointOnGrid(player.transform.position + player.transform.forward);
            placement.rotation = Quaternion.identity;
            if (inputManager.interactInput)
            {
                inputManager.interactInput = false;
                if(checkIfPosEmpty(placement.position))
                    Plant(player.transform.position + player.transform.forward);
            }
        //}
    }

    private void Plant(Vector3 clickPoint)
    {
        var placed = Instantiate(plant);
        placed.transform.position = grid.GetNearestPointOnGrid(clickPoint);
        placed.transform.Rotate(new Vector3(placed.transform.rotation.x, Random.Range(0f,360f), placed.transform.rotation.z));
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
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