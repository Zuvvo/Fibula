using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class strzelaniedefault : MonoBehaviour {
    public GameObject pocisk;
    public float szybkosc_pocisku;
    public float tempo_strzelanka;
    public int lvl;


    private bool strzelanko;
    private float time;
    // Use this for initialization

    // Update is called once per frame
    void Start()
    {
    }
    void Update()
    {
        klawisze();
        if (strzelanko == true) strzal();
    }
    public void strzal()
    {

        time += Time.deltaTime;
        if (time > tempo_strzelanka)
        {
            generate_pocisk();
            time = 0f;
        }
    }
    public void klawisze()
    {
        if (Input.GetButtonDown("Jump"))
        {
            strzelanko = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            strzelanko = false;
        }
        if (Input.GetKeyDown("g"))
        {
            tempo_strzelanka = 0.00001f;
        }
    }
    public void generate_pocisk()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        GameObject pocisktemp;
        pocisktemp = Instantiate(pocisk, gameObject.transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb;
        rb = pocisktemp.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * szybkosc_pocisku);
        rb.name = "pociskdefault";
        Destroy(pocisktemp, 1);
    }
    public void OnEnable()
    {
      //  strzelanko = true;
        if (Input.GetButton("Jump")) strzelanko = true;
        else strzelanko = false;
    }
}
