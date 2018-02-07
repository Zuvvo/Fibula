using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kukurydza : MonoBehaviour {

    private GameObject MusicPlayer;
	// Use this for initialization
	void Start () {
        MusicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");


    }
	
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Gracz")
        {
            switch (gameObject.name)
            {
                case "kukurydzacrate":
                    if (!statystyki.staty.odblokowane_bronki[1]) statystyki.staty.odblokowane_bronki[1] = true;
                    if (statystyki.staty.kukurydze < 3) statystyki.staty.kukurydze++;
                    Destroy(gameObject);
                    //if (statystyki.staty.kukurydze == 2)
                    //{
                    //    statystyki.staty.odblokowane_bronki[1] = false;
                    //    statystyki.staty.odblokowane_bronki[2] = true;
                    //}
                    //if (statystyki.staty.kukurydze == 3)
                    //{
                    //    statystyki.staty.odblokowane_bronki[1] = false;
                    //    statystyki.staty.odblokowane_bronki[2] = false;
                    //    statystyki.staty.odblokowane_bronki[3] = true;
                    //}
                    GameObject.FindGameObjectWithTag("bronie").gameObject.transform.GetChild(1).GetComponent<Kukurydzalevel>().Upgrade();
                    GameObject.FindGameObjectWithTag("bronie").gameObject.transform.GetChild(1).GetComponent<Kukurydzalevel>().Refresh();
                    MusicPlayer.GetComponent<start>().PlayChestSound();
                    break;
                case "defaultcrate":
                    GameObject.FindGameObjectWithTag("bronie").gameObject.transform.GetChild(0).GetComponent<defaultlevel>().Upgrade();
                    //     GameObject.FindGameObjectWithTag("bronie").gameObject.transform.GetChild(0).GetComponent<defaultlevel>().Refresh();
                    MusicPlayer.GetComponent<start>().PlayChestSound();
                    Destroy(gameObject);
                    break;
            }
        }

    }
}
