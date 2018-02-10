using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapScanner : MonoBehaviour {
    int columns = MapSystem.Map1Server.GetLength(0);
    int rows = MapSystem.Map1Server.GetLength(1);
    // Use this for initialization
    void Start()
    {
        Debug.Log("Map1 Serwer[10,10]" +  MapSystem.Map1Server[10, 10].position);
        Debug.Log("Map1 Local[10,10]" + MapSystem.Map1Local[10, 10].position);

        Debug.Log("Map1 Serwer[5,15]" + MapSystem.Map1Server[5, 15].position);
        Debug.Log("Map1 Local[5,15]" + MapSystem.Map1Local[5, 15].position);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
