using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtakUfo : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;
    public float lerpTime;
    public float currentLerpTime;
    public GameObject Shield;

    private float SpawnShieldTime = 5;
    private void Awake()
    {
        currentLerpTime = 0;
    }
    // Use this for initialization
    void Start () {
        startPos = gameObject.transform.position;
        endPos = new Vector3(185, 153, 0);
        PrzylotUfo();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        transform.position = Vector3.Lerp(startPos, endPos, currentLerpTime / lerpTime);
        if (currentLerpTime==lerpTime) PowrotDoPoczatkowejPozycji();

        if (currentLerpTime > SpawnShieldTime)
        {
            SpawnShieldTime = 200;
            SpawnShield();
        }

    }
    public void PrzylotUfo()
    {
        
    }

    private void SpawnShield()
    {
        Shield.SetActive(true);
    }
    private void PowrotDoPoczatkowejPozycji()
    {
        currentLerpTime = 0;
        gameObject.transform.position = startPos;
        gameObject.SetActive(false);
    }
}
