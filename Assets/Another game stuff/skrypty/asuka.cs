using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class asuka : MonoBehaviour {
    public GameObject bomba;
    public float szybkosc_bomby;
    public GameObject kierunek_strzalu;

    private float time_temp = 0;
    private float hp;

    void Start () {
        GameObject.FindGameObjectWithTag("tlo").GetComponent<start>().asuka_background();
    }
	
	// Update is called once per frame
	void Update () {

        strzal_bombami();


    }
    public void zrzucaniebomb()
    {
        time_temp += Time.deltaTime;
        if (time_temp > 0.2f)
        {
            int rnd = (int)Random.Range(0, 100);
            time_temp = 0;
            if(rnd<6)
            {
                GameObject bombatemp;
                bombatemp = Instantiate(bomba, gameObject.transform.position, Quaternion.identity) as GameObject;
                Rigidbody2D rb;
                rb = bombatemp.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.down * szybkosc_bomby);
                bombatemp.name = "bomba";
                Destroy(bombatemp, 3f);
            }
        }
    }
    public void strzal_bombami()
    {
        time_temp += Time.deltaTime;
        if (time_temp > 0.2f)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 200));
            time_temp = 0;
            transform.Rotate(0, 0, 5);
            GameObject bombatemp;
            bombatemp = Instantiate(bomba, gameObject.transform.position, Quaternion.identity) as GameObject;
            Rigidbody2D rb;
            rb = bombatemp.GetComponent<Rigidbody2D>();
            rb.AddForce((kierunek_strzalu.transform.position - transform.position).normalized * szybkosc_bomby);
            bombatemp.name = "bomba";
            Destroy(bombatemp, 3f);
        }
        //  Debug.Log(transform.rotation.z);
        //  Debug.Log(kierunek_pocisku);
    }
    //   InvokeRepeating("powiekszanie", 0.5f, 5f);
    //   zrzucaniebomb();
    //   przesuwanie();
    //public void Sethpbar(GameObject healthBarPanel)
    //{
    //    this.healthbar = healthBarPanel;
    //    healthbar.SetActive(false);
    //}
    //public void damage(float obrazenia, GameObject hpbar)
    //{
    //    hp -= obrazenia;
    //    if(hpbar.active==false) hpbar.SetActive(true);
    //    hpbar.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = hp / starthp;
    //    if(hp<=0)
    //    {
    //        Die(hpbar);
    //    }
    //}
    //public void przesuwanie()
    //{
    //    if (Time.time> 4f && Time.time <4.5f) ustawieniedopionu();
    //    if (Time.time > 5f)
    //    {
    //        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * szybkosc_poruszania;
    //    }

    //}
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "pociskkukurydza")
    //    {
    //        damage(100, healthbar);
    //        Destroy(collision.gameObject);
    //    }
    //    if (collision.gameObject.name == "pociskdefault")
    //    {
    //        damage(40, healthbar);
    //        Destroy(collision.gameObject);
    //    }
    //}
    //public void Die(GameObject hpbar)
    //{
    //    statystyki.staty.Score += 1;
    //    gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
    //    gameObject.GetComponent<Renderer>().enabled = false;
    //    gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    //    Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length);
    //    Destroy(hpbar);
    //    gameObject.GetComponent<asuka>().enabled = false;
    //}
}
