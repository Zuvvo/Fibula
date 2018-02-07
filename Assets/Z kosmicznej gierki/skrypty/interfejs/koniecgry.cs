using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class koniecgry : MonoBehaviour {
    public GameObject RekordSkoczni;
    public GameObject RekordSkoczniEN;
    public GameObject[] teksty;
	// Use this for initialization
	void Start () {
        CheckRecord();
        SetPoints();
        SetRecord();
        SetLanguage(UstawieniaMenu.Ustawienia.Jezyk);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Menu");
        }

    }
    public void PlayGame()
    {

        statystyki.staty.Reset(); //temp solution
        SceneManager.LoadScene("Start");
    }
    public void Menu()
    {
        statystyki.staty.Reset(); //temp solution
        SceneManager.LoadScene("Menu");
    }
    private void CheckRecord()
    {
        if (statystyki.staty.Score == statystyki.staty.highScore)
        {
            if (UstawieniaMenu.Ustawienia.Jezyk == 0)
            {

                RekordSkoczni.SetActive(true);
            }
            else
            {
                RekordSkoczniEN.SetActive(true);
            }
        }
    }
    public void SetPoints()
    {
        if (UstawieniaMenu.Ustawienia.Jezyk == 0)
        {
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Punkty: " + statystyki.staty.Score;
        }
        else
        {
            gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Points: " + statystyki.staty.Score;
        }
    }
    public void SetRecord()
    {
        if (UstawieniaMenu.Ustawienia.Jezyk == 0)
        {
            gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Rekord: " + statystyki.staty.highScore;
        }
        else
        {
            gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Record: " + statystyki.staty.highScore;
        }

    }
    public void SetLanguage(int language)
    {
        if (language == 0)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
            gameObject.transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(6).GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(9).GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(9).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
            gameObject.transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(9).GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(9).GetChild(1).gameObject.SetActive(true);
        }
    }
}
