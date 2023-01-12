using UnityEngine;
using UnityEngine.UI;
public class FishManager : MonoBehaviour
{    
    private Grid grid;
    private InputManager inputManager;
    public Transform placement;
    public GameObject player;
    public Mesh Select;
    public bool isFishing = false;
    public bool isHooked = false;
    public bool isCatching = false;
    float CatchTimer = 3f;
    public Canvas FishCanvas;
    public Slider CatchSlider;
    public Slider TensionSlider;
    public float CatchMeter;
    public float TensionMeter;
    public GameObject fx;
    public GameObject spawned;
    public FishItem currentFish;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        placement.position = grid.GetNearestPointOnGrid(player.transform.position + player.transform.forward);
        placement.rotation = Quaternion.identity;

        if (inputManager.interactInput && !isCatching)
        {
            inputManager.interactInput = false;
            Fish(CheckIfWater(placement.position));
        }

        if(isFishing)
            RollForFish();
        
        if(isHooked && !isCatching)
            CatchTimer -= Time.deltaTime;

        if(CatchTimer <= 0){
            isFishing = false;
            isHooked = false;
            Destroy(spawned);
            CatchTimer = 3f;
        }

        if(isCatching)
        {
            CatchTimer = 3f;
            if(Vector2.Distance(Vector2.zero, FindObjectOfType<FishMovement>().rect.localPosition) < 35f)
                CatchSlider.value += Time.deltaTime * 15f;
            else
                CatchSlider.value -= Time.deltaTime * Vector2.Distance(Vector2.zero, FindObjectOfType<FishMovement>().rect.localPosition) / 25f;

            if(inputManager.horizontalInput != 0 || inputManager.verticalInput != 0 )
                TensionSlider.value += Time.deltaTime * 25f * inputManager.TestAngle;
            else
                TensionSlider.value -= Time.deltaTime * (180 - Vector2.Distance(Vector2.zero, FindObjectOfType<FishMovement>().rect.localPosition)) / 5f;

            CatchMeter = CatchSlider.value;
            TensionMeter = TensionSlider.value;

            if(CatchMeter >= 100)
            {
                Instantiate(currentFish.Fish, 
                placement.position + new Vector3(0,1f,0), 
                Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360)));

                Reset();
            }
            else if(CatchMeter <= 0)
                Reset();
            else if(TensionMeter >= 100)
                Reset();
        }
    }

    private void Fish(GameObject water)
    {   
        if(water == null)
            return;

        isFishing = !isFishing;

        if(isHooked)
        {
            FishCanvas.gameObject.SetActive(true);
            FindObjectOfType<FishMovement>().SetCurrentFish(currentFish);
            isCatching = true;
            Destroy(spawned);
        }
        else
        {
            isCatching = false;
            FishCanvas.gameObject.SetActive(false);
        }
    }

    public GameObject CheckIfWater(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Water");
        foreach(GameObject current in allMovableThings)
        {
            if(current.transform.position == targetPos)
            {
                currentFish = current.GetComponent<FishingSpot>().SelectFish();
                return current;
            }
        }
        return null;
    }

    public void RollForFish()
    {
        if(isHooked)
            return;

        int hooked = Random.Range(0,100);
        if(hooked == 0){
            isHooked = true;
            spawned = Instantiate(fx, placement.position, Quaternion.identity);
        }
    }

    public void Reset()
    {
        isCatching = false;
        isHooked = false;
        isFishing = false;
        CatchSlider.value = 20;
        TensionSlider.value = 10;
        FindObjectOfType<FishMovement>().SetCurrentFish(currentFish);
        FishCanvas.gameObject.SetActive(false);
    }
}