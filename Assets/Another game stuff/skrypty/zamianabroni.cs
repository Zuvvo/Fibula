using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class zamianabroni : MonoBehaviour {

    public int wybranabron = 0;
    public GameObject obrazekzwybranabronia;
    private GameObject Audioplayer;

    // Use this for initialization
    void Start () {
        Audioplayer = GameObject.FindGameObjectWithTag("MusicPlayer");
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            wybranabron++;
            if (wybranabron == transform.childCount)
            {
                wybranabron = 0;
            }
            if (statystyki.staty.odblokowane_bronki[wybranabron]) wybierzbron();
            else
            {
                do
                {
                    wybranabron++;
                    if (wybranabron == transform.childCount)
                    {
                        wybranabron = 0;
                    }
                }
                while (statystyki.staty.odblokowane_bronki[wybranabron] == false);
                wybierzbron();
            }
        }
		
	}
    public void wybierzbron()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == wybranabron)
            {
                Audioplayer.GetComponent<start>().PlayReloadWeapon();
                weapon.gameObject.SetActive(true);
                obrazekzwybranabronia.transform.GetChild(i).transform.gameObject.SetActive(true);
                Refreshweapon(weapon);
                gameObject.transform.parent.GetChild(i + 1).gameObject.SetActive(true);
                
            }
            else
            {
                weapon.gameObject.SetActive(false);
                gameObject.transform.parent.GetChild(i + 1).gameObject.SetActive(false);
                obrazekzwybranabronia.transform.GetChild(i).transform.gameObject.SetActive(false);
            }
            i++;
        }
    }
    public void Refreshweapon(Transform bron)
    {
        if (bron.name == "armatakukurydziana")
        {
            transform.GetChild(1).GetComponent<Kukurydzalevel>().Refresh();
        }
        if (bron.name == "BronDefault")
        {
            transform.GetChild(0).GetComponent<defaultlevel>().Refresh();
        }
    }
    public void ZamianaBroniAndroid()
    {
        wybranabron++;
        if (wybranabron == transform.childCount)
        {
            wybranabron = 0;
        }
        if (statystyki.staty.odblokowane_bronki[wybranabron]) wybierzbron();
        else
        {
            do
            {
                wybranabron++;
                if (wybranabron == transform.childCount)
                {
                    wybranabron = 0;
                }
            }
            while (statystyki.staty.odblokowane_bronki[wybranabron] == false);
            wybierzbron();
        }
    }
}
