using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Przeciwnik : MonoBehaviour {

    [System.Serializable]
    public class Drop
    {
        public GameObject item;
        public int szansa;
        public float opadanie;
    }
    public Drop[] drop;
    public Drop[] drop_rownoczesny;
    public float starthp;
    public int punkty;
    public GameObject hpbarprefab;
    private RectTransform barpanel;
    private GameObject healthbar;
    private float hp;
    // Use this for initialization
    private void Awake()
    {
        starthp *= statystyki.staty.level;
        hp = starthp;
        barpanel = GameObject.FindGameObjectWithTag("canvashp").GetComponent<RectTransform>();
        GenerateHealthBar(gameObject.transform);

    }
    void Start()
    {
    }
    private void GenerateHealthBar(Transform enemy)
    {
        GameObject hpbar = Instantiate(hpbarprefab) as GameObject;
        hpbar.transform.SetParent(barpanel, false);
        hpbar.GetComponent<hpbar>().SetHealthBarData(enemy, barpanel);
        this.healthbar = hpbar;
        hpbar.SetActive(false);
    }
    public void Sethpbar(GameObject healthBarPanel)
    {
        this.healthbar = healthBarPanel;
        healthbar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void damage(float obrazenia, GameObject hpbar)
    {
        hp -= obrazenia;
        hpbar.SetActive(true);
        hpbar.transform.GetChild(0).GetComponentInChildren<Image>().fillAmount = hp / starthp;
        if (hp <= 0)
        {
            Die(hpbar);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "pociskkukurydza")
        {
            damage(60, healthbar);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "pociskdefault")
        {
            damage(40, healthbar);
            Destroy(collision.gameObject);
        }
    }
    public virtual void Die(GameObject hpbar)
    {
        statystyki.staty.AddScore(punkty);
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        GetComponent<Renderer>().enabled = false;
        if(GetComponent<BoxCollider2D>()!=null) GetComponent<BoxCollider2D>().enabled = false;
        if (GetComponent<EdgeCollider2D>() != null) GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length);
        GetComponent<MonoBehaviour>().enabled = false;
        Destroy(hpbar);
        wydropitem();
        wydropitemrownoczesny();
    }


    public void wydropitem()
    {
        int rnd = Random.Range(1, 101);
        int procent = 0;
        foreach (Drop el in drop)
        {
            if (rnd <= el.szansa + procent && rnd > procent)
            {
                GameObject objectemp;
                Rigidbody2D rb;
                objectemp = Instantiate(el.item, gameObject.transform.position, Quaternion.identity) as GameObject;
                rb = objectemp.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.down * el.opadanie);
                objectemp.name = el.item.name;
                Destroy(objectemp, 3f);
            }
            procent += el.szansa;
        }
    }
    public void wydropitemrownoczesny()
    {

        int rnd = Random.Range(1, 101);
        foreach (Drop el in drop_rownoczesny)
        {
            if (rnd <= el.szansa)
            {
                GameObject objectemp;
                Rigidbody2D rb;
                objectemp = Instantiate(el.item, gameObject.transform.position, Quaternion.identity) as GameObject;
                rb = objectemp.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.down * el.opadanie);
                objectemp.name = el.item.name;
                Destroy(objectemp, 3f);
            }
        }

    }
}
