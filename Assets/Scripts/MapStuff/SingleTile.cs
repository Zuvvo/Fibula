using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTile{
    private int maxObjectsOnSingleTile = 100;

	public bool isBlocked;
	public FibulaObject[] objectsOnTile;
    public FibulaPosition position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public SingleTile()
    {
        isBlocked = false;
        objectsOnTile = new FibulaObject[maxObjectsOnSingleTile];
    }
}
