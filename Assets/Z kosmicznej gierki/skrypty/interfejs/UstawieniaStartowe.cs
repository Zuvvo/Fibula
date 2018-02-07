using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UstawieniaStartowe : MonoBehaviour {
    
    GameObject LevelInfoText;
    GameObject Ustawienia;
    private bool sprawdzajka = true;

	// Use this for initialization
	void Start () {
        statystyki.staty.Hp = 100;
        statystyki.staty.Coins = 0;
        statystyki.staty.Score = 0;
        statystyki.staty.level = 1;
        statystyki.staty.Faza = 1;
        GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<start>().MenuTheme.Stop();
	}
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("LevelInfo") != null && sprawdzajka == true)
        {
            SetScoreText(UstawieniaMenu.Ustawienia.Jezyk);
            sprawdzajka = false;
        }
	}

    public void SetScoreText(int language)
    {
        LevelInfoText = GameObject.FindGameObjectWithTag("LevelInfo");
        TextMeshProUGUI mtext2 = LevelInfoText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI mtext3 = LevelInfoText.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        if (language == 0)
        {
            mtext2.text = "POZIOM 1";
            mtext3.text = "FAZA 1";
        }
        if (language == 1)
        {
            mtext2.text = "LEVEL 1";
            mtext3.text = "PHASE 1";
        }
    }
}
