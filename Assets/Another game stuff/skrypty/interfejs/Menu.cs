using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    
    public Slider slider;
    public GameObject Ustawienia;
    public GameObject Background1;
    public GameObject Background2;
    public GameObject tytul;

    public float Glosnosc;

    public GameObject[] teksty;

    public float glosnosc { get { return Glosnosc; }
        set
        {
            Glosnosc = value;
            Ustawienia.transform.GetComponent<UstawieniaMenu>().glosnoscmuzyki = Glosnosc;
        }
    }
    public void Update()
    {
    }
    void Start()
    {
        statystyki.staty.LoadData();
     //   UstawieniaMenu.Ustawienia.Refresh();
        SetCurrentLanguage();
        if (gameObject.name == "MainMenu")
        {
            Ustawienia = GameObject.FindGameObjectWithTag("Ustawienia").gameObject;
        }
        Glosnosc = Ustawienia.transform.GetComponent<UstawieniaMenu>().glosnoscmuzyki;
    }
    private void Awake()
    {
            Ustawienia = GameObject.FindGameObjectWithTag("Ustawienia").gameObject;
            slider.value = Ustawienia.transform.GetComponent<UstawieniaMenu>().glosnoscmuzyki;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Background(string wlacznik)
    {
        if (wlacznik == "off")
        {
            tytul.SetActive(false);
            Background1.SetActive(false);
        }
        if (wlacznik == "on")
        {
            tytul.SetActive(true);
            Background1.SetActive(true);
        }
    }
    public void ChangeLanguage()
    {
        int jezyk = GameObject.FindGameObjectWithTag("Ustawienia").GetComponent<UstawieniaMenu>().Jezyk;
        int PoprzedniJezyk = jezyk;
        jezyk++;
        if (jezyk > 1) jezyk = 0;
        foreach (GameObject obiekt in teksty)
        {
            obiekt.transform.GetChild(jezyk).gameObject.SetActive(true);
            obiekt.transform.GetChild(PoprzedniJezyk).gameObject.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("Ustawienia").GetComponent<UstawieniaMenu>().Jezyk = jezyk;
    }
    public void SetCurrentLanguage()
    {
        foreach (GameObject obiekt in teksty)
        {
            int jezyk = GameObject.FindGameObjectWithTag("Ustawienia").GetComponent<UstawieniaMenu>().Jezyk;
            int PoprzedniJezyk = 0;
            if (jezyk == 0) PoprzedniJezyk = 1;
            else PoprzedniJezyk = 0;
            obiekt.transform.GetChild(jezyk).gameObject.SetActive(true);
            obiekt.transform.GetChild(PoprzedniJezyk).gameObject.SetActive(false);
        }

    }
}
