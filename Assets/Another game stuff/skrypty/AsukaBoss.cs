using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsukaBoss : Przeciwnik{
  //  Przeciwnik Asuka = new Przeciwnik();

    public GameObject bomba;
    public float szybkoscBomby;
    public GameObject coin;
    private GameObject MusicPlayer;

    private GameObject kierunekStrzalu;
    private float time_temp = 0;
    private float time_temp_konieczlota = 0;
    private bool zasypzlotem;
    private bool strzalbombami;

    void Start () {
        strzalbombami = true;
        MusicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        zasypzlotem = false;
        kierunekStrzalu = gameObject.transform.GetChild(0).gameObject;

    }

    void Update() {
        if(strzalbombami) strzal_bombami();
        if (zasypzlotem && time_temp_konieczlota<4)
        {
            ZasypZlotem();
            time_temp_konieczlota += Time.deltaTime;
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
            rb.AddForce((kierunekStrzalu.transform.position - transform.position).normalized * szybkoscBomby);
            bombatemp.name = "bomba";
            Destroy(bombatemp, 3f);
        }
    }
    public override void Die(GameObject hpbar)
    {
        strzalbombami = false;
        MusicPlayer.GetComponent<start>().Pause();
        statystyki.staty.AddScore(punkty);
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        GetComponent<Renderer>().enabled = false;
        if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;
        if (GetComponent<EdgeCollider2D>() != null) GetComponent<EdgeCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(WaitZloto());
        Destroy(gameObject, 7.5f);
      //  GetComponent<MonoBehaviour>().enabled = false;
        Destroy(hpbar);
    }
    public void ZasypZlotem()
    {
        float rnd;
        time_temp += Time.deltaTime;
        kierunekStrzalu.transform.Rotate(0, 0, 0);
        if (time_temp > 0.1f)
        {
            rnd = Random.Range(170f, 178f);
            kierunekStrzalu.transform.position = new Vector3(rnd, 155, 0);
            time_temp = 0;
            GameObject cointemp;
            cointemp = Instantiate(coin, kierunekStrzalu.transform.position, Quaternion.identity) as GameObject;
            Rigidbody2D rb = cointemp.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down* szybkoscBomby * 1.2f);
            cointemp.name = "coin";
            Destroy(cointemp, 2);
        }
    }
    IEnumerator WaitZloto()
    {
        yield return new WaitForSeconds(1.5f);
        zasypzlotem = true;
    }
}
