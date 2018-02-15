using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagment : MonoBehaviour {
    private GameObject player;
    public GameObject Minimap;

    // Use this for initialization
    private void Awake()
    {
        SetPositionsOf(MapSystem.Map1Server);
        MapSystem.Map1Local = MapSystem.Map1Server;
    }
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (!Minimap.active) Minimap.SetActive(true);
            else Minimap.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Tab)) player.GetComponent<RenderBody>().RunPanel();
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(MapSystem.ObjListSerwer.Count);
            DisplayObjectsInList();
        }

    }
    void DisplayObjectsInList()
    {
        foreach(FibulaObject obj in MapSystem.ObjListSerwer)
        {
            Debug.Log("Pozycja na wirtualnej mapie: " + obj.position.x + "," + obj.position.y);
        }
    }
    private void SetPositionsOf(SingleTile[,] Map)
    {
        Vector3 position = new Vector3(0, 0, 0);
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                Map[i, j].position.x = (int)position.x;
                Map[i, j].position.y = (int)position.y;
                position.y += 1;
            }
            position.y = 0;
            position.x += 1;
        }
    }
}
