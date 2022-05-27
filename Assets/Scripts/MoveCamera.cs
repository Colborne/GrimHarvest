using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;
    public float x,y,z;

    void Update() {
        transform.position = player.transform.position + new Vector3(x,y,z);
    }
}
