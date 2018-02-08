using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBokStrzelanie : MonoBehaviour {
    private float CzasDoStrzalu = 0;
    private float CzasDoPrzesuniecia = 0;

    public bool strzelanko = false;
    public GameObject PociskPrefab;

    private Vector3 PoczatkowaPozycja;
    private Vector3 PozycjaPoObnizeniuPocisku;
    private Vector3[] Pozycja = new Vector3[5];
    // Use this for initialization
    void Start () {
        PoczatkowaPozycja = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 PrzesuniecieDoGory = new Vector3(0,0.5f,0);
        for(int i = 0; i < 5; i++)
        {
            Pozycja[i] = gameObject.transform.position + (PrzesuniecieDoGory * (i+1));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (strzelanko)
        {
            Strzelaj();
            Przesuniecie();
        }
	}
    private void Strzelaj()
    {
        CzasDoStrzalu += Time.deltaTime;
        if (CzasDoStrzalu >= 1)
        {
            PozycjaPoObnizeniuPocisku = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
            GameObject pocisk = Instantiate(PociskPrefab, PozycjaPoObnizeniuPocisku, Quaternion.identity) as GameObject;
            pocisk.name = "grzyb";
            Rigidbody2D rb;
            rb = pocisk.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            if (gameObject.name == "UfoBok1")
            {
                rb.AddForce(Vector2.left * 100);
                pocisk.transform.Rotate(0, 0, 90);
            }
            if (gameObject.name == "UfoBok2")
            {
                rb.AddForce(Vector2.right * 100);
                pocisk.transform.Rotate(0, 0, -90);
            }
            CzasDoStrzalu = 0;
        }
    }
    private void Przesuniecie()
    {
        CzasDoPrzesuniecia += Time.deltaTime;
        if (CzasDoPrzesuniecia>=4)
        {
            int rnd = Random.Range(0, 4);
            gameObject.transform.position = Pozycja[rnd];
            CzasDoPrzesuniecia = 0;
        }
    }
    public void PowrotDoPoczatkowejPozycji()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.transform.position = PoczatkowaPozycja;
        if (gameObject.name == "UfoBok1") gameObject.transform.Rotate(0, 0, 135);
        if (gameObject.name == "UfoBok2") gameObject.transform.Rotate(0, 0, -135);
        strzelanko = false;
    }
}
