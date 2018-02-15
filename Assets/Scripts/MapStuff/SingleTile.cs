using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//0 - grass
//1 - ground

public class SingleTile{
    private int maxObjectsOnSingleTile = 100;
    public int tileTypeId;
    
    public bool isBlocked;
	public FibulaObject[] objectsOnTile;
    public FibulaObject EnemyOnTile;
    public FibulaPosition position;
    public bool isPlayerOnTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public SingleTile()
    {
        isPlayerOnTile = false;
        isBlocked = false;
        objectsOnTile = new FibulaObject[maxObjectsOnSingleTile];
        EnemyOnTile = null;
    }
}
