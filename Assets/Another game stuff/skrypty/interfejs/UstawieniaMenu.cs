using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UstawieniaMenu : MonoBehaviour {
    public static UstawieniaMenu Ustawienia;
    public float GlosnoscMuzyki;
    public int Jezyk;
    public GameObject OdtwarzaczMuzyki;
    // Use this for initialization
    void Start() {
        Jezyk = 1;
        glosnoscmuzyki = 1;
        statystyki.staty.LoadData();
        Ustawienia = this;
    }

    // Update is called once per frame

    public float glosnoscmuzyki { get { return GlosnoscMuzyki; }
        set
        {
            GlosnoscMuzyki = value;
            OdtwarzaczMuzyki.GetComponent<start>().glosnosc = value;
            Ustawienia = this;
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            Ustawienia = this;
        //    GameObject.FindGameObjectWithTag("UstawieniaStartowe").GetComponent<UstawieniaStartowe>().SetScoreText(Jezyk);
        }
    }
    public void Refresh()
    {
        Ustawienia = this;
    }
}
