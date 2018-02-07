using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {
    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

    }
}
