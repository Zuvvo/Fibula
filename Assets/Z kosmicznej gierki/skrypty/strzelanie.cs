using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class strzelanie : MonoBehaviour {
    public GameObject pocisk;
    public float szybkosc_pocisku;
    public bool odblokowane = true;
    public Vector2 kierunek_pocisku;
    public float tempo_strzelanka;
    private float time_temp_kukurydza = 0;
    private int obrot_temp_kukurydza = -1;
    private bool moveright = false;
    private bool strzelanko;
    private float time;

    // Use this for initialization
    void Start() {
        if (gameObject.name == "kukurydza left") transform.Rotate(0, 0, -75);
        if (gameObject.name == "kukurydza right") transform.Rotate(0, 0, 75);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.name != "kukurydza center")
        {
            obrot_armaty_kukurydzianej();
        }
        klawisze();
        if (strzelanko) strzal();
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

    public void generate_pocisk()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        GameObject pocisktemp;
        pocisktemp = Instantiate(pocisk, gameObject.transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb;
        rb = pocisktemp.GetComponent<Rigidbody2D>();
        kierunek_pocisku = new Vector2(transform.rotation.z*(-1), 1);
      //  Debug.Log(transform.rotation.z);
      //  Debug.Log(kierunek_pocisku);
        rb.AddForce(kierunek_pocisku * szybkosc_pocisku);
        rb.AddTorque(250);
        rb.name = "pociskkukurydza";
        Destroy(pocisktemp, 3f);
    }
    public void obrot_armaty_kukurydzianej()
    {
        time_temp_kukurydza += Time.deltaTime;
       // Debug.Log(transform.rotation.z);
        if (time_temp_kukurydza > 0.05f)
        {
            time_temp_kukurydza = 0;
            transform.Rotate(0, 0, 5 * obrot_temp_kukurydza);
        }
        if (moveright == true && transform.rotation.z > 0.6f)
        {
            obrot_temp_kukurydza *= -1;
            moveright = false;
        }
        if (moveright != true && transform.rotation.z < -0.6f)
        {
            moveright = true;
            obrot_temp_kukurydza *= -1;
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
    public void OnEnable()
    {
        //strzelanko = true;
        if (Input.GetButton("Jump")) strzelanko = true;
        else strzelanko = false;
    }
}
