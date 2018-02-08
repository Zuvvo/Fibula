using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : Przeciwnik {

    public GameObject grzyb;
    public float szybkosc_grzyba;

    private float time_temp = 0;

    void Start () {
    }
	
	void Update () {
        zrzucaniebomb();

    }
    public void zrzucaniebomb()
    {
        time_temp += Time.deltaTime;
        if (time_temp > 0.2f)
        {
            int rnd = (int)Random.Range(0, 100);
            time_temp = 0;
            if (rnd < 2)
            {
                GameObject grzybtemp;
                grzybtemp = Instantiate(grzyb, gameObject.transform.position, Quaternion.identity) as GameObject;
                Rigidbody2D rb;
                rb = grzybtemp.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.down * szybkosc_grzyba);
                grzybtemp.name = "grzyb";
                Destroy(grzybtemp, 3f);
            }
        }
    }
}
