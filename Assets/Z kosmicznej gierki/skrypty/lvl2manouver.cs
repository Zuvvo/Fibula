using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2manouver : MonoBehaviour {

    public bool ManewrRozpoczety;
    public bool Ruszaj;
    public GameObject[] ufo = new GameObject[10];
    private Vector3[] startPos = new Vector3[10];
    private Vector3[] endPos = new Vector3[10];
    public float lerpTime;
    public float currentLerpTime;
    // Use this for initialization
    void Start () {
        lerpTime = 2;
        ManewrRozpoczety = false;
        Ruszaj = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (ManewrRozpoczety)
        {
            ManewrRozpoczety = false;
            PozycjaKoncowa();
            ufo = GameObject.FindGameObjectsWithTag("enemy");
            int i = 0;
            foreach (GameObject element in ufo)
            {
                startPos[i] = element.transform.position;
                element.GetComponent<BoxCollider2D>().enabled = false;
                element.GetComponent<Rigidbody2D>().isKinematic = true;
                i++;
            }
            Ruszaj = true;
        }
        if (Ruszaj) Move();
	}

    public void Move()
    {
        currentLerpTime += Time.deltaTime;
        for (int i = 0; i < ufo.Length; i++)
        {
            if(ufo[i]!=null) ufo[i].transform.position = Vector3.Lerp(startPos[i], endPos[i], currentLerpTime / lerpTime);
        }
        if (currentLerpTime >= lerpTime)
        {
            Ruszaj = false;
            PoczatkoweUstawienia();
            currentLerpTime = 0;
        }
    }
    public void PozycjaKoncowa()
    {
        Vector3 PierwszaPozycja = new Vector3(167.5f, 153f);
        for (int i = 0; i < 10; i++)
        {
            if(i%2==0)
            endPos[i] = new Vector3((PierwszaPozycja.x) + 1.54f*i ,PierwszaPozycja.y);
            else
            endPos[i] = new Vector3(((PierwszaPozycja.x) + 1.54f * i), PierwszaPozycja.y-1.5f);
        }
    }
    public void PoczatkoweUstawienia()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
            if (ufo[i] != null)
            {
                ufo[i].GetComponent<BoxCollider2D>().enabled = true;
                ufo[i].GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
