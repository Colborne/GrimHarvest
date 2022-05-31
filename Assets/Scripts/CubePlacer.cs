using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public GameObject plant;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(inputManager.mouseInput);

        if (Physics.Raycast(ray, out hitInfo))
        {
            placement.position = grid.GetNearestPointOnGrid(hitInfo.point) + new Vector3(0,.5f,0);

            if (inputManager.interactInput)
            {
                inputManager.interactInput = false;
                PlaceCubeNear(hitInfo.point);
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var placed = Instantiate(plant);
        placed.transform.position = grid.GetNearestPointOnGrid(clickPoint) + new Vector3(0,.5f,0);
        placed.transform.Rotate(new Vector3(placed.transform.rotation.x, Random.Range(0f,360f), placed.transform.rotation.z));
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
}