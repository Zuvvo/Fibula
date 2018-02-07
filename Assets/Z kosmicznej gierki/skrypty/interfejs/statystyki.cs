using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;

public class statystyki : MonoBehaviour
{
    //static zeby mozna z innych klas odpalac te metody
    public static statystyki staty;
    public GameObject TimerUI;
    public GameObject ScoreUI;
    public GameObject ScoreUI2;
    public GameObject highscoreUI;
    public GameObject coinsUI;
    public GameObject hpUI;
    public static float timer;
    public float Hp;
    public int Score;
    public int highScore;
    public int Coins;
    public bool[] odblokowane_bronki;
    public int kukurydze;
    public int Faza;
    public int EnemyCount;
    private int Level;
    private string coinsPLENG;

    public int level
    {
        get { return Level; }
        set
        {
            Level = value;
            staty = this;
        }
    }
    public void StartGameStats()
    {
        staty.Hp = 100;
        staty.Coins = 0;
        staty.Score = 0;
    }

    public void Update()
    {
        if (highscoreUI == null)
        {
            highscoreUI = GameObject.FindGameObjectWithTag("highscore");
        }
        if (coinsUI == null)
        {
            coinsUI = GameObject.FindGameObjectWithTag("coins");
        }
        if (hpUI == null)
        {
            hpUI = GameObject.FindGameObjectWithTag("hp");
        }
        czas();
        coins();
        if (Score > highScore)
        {
            highScore = Score;
        }
    }

    public void czas()
    {
        if (TimerUI != null)
        {
            TextMeshProUGUI mText = TimerUI.gameObject.GetComponent<TextMeshProUGUI>();
            timer += Time.deltaTime;
            mText.text = "Czas: " + (int)timer;
        }
    }
    public void AddScore(int punkty)
    {
        staty.Score += punkty;
        ScoreUI = GameObject.FindGameObjectWithTag("score");
        ScoreUI.gameObject.GetComponent<TextMeshProUGUI>().text = Score.ToString();
    }
    public void coins()
    {
        if (coinsUI != null)
        {
            coinsUI.gameObject.GetComponent<TextMeshProUGUI>().text = Coins.ToString();
        }


    }
    public void ChangeHp(float input)
    {
        if (hpUI == null) return;
        Hp += input;
        TextMeshProUGUI mText = hpUI.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        hpUI.transform.GetChild(0).GetComponent<Image>().fillAmount = Hp / 100f;
        mText.text = Hp.ToString();
        if (Hp <= 0)
        {
            mText.text = "0!";
            GameObject.FindGameObjectWithTag("Player").GetComponent<player_move>().Die();
        }
    }

    public void Start()
    {
        odblokowane_bronki = new bool[20];
        for (int i=0; i< odblokowane_bronki.Length; i++)
        {
            odblokowane_bronki[i] = false;
        }
        odblokowane_bronki[0] = true;
    }
    private void Awake()
    {
        staty = this;
    }

    public void SaveData()
    {
        if (Score > highScore)
        {
            highScore = Score;
        }
        BinaryFormatter BinForm = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        gameData data = new gameData(); // data container
        data.highscore = highScore;
      //  data.glosnoscMuzyki = UstawieniaMenu.Ustawienia.glosnoscmuzyki;
      //  data.jezyk = UstawieniaMenu.Ustawienia.Jezyk;
        BinForm.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();
            highScore = data.highscore;
         //   UstawieniaMenu.Ustawienia.Jezyk = data.jezyk;
         //   UstawieniaMenu.Ustawienia.glosnoscmuzyki = data.glosnoscMuzyki;
         //   UstawieniaMenu.Ustawienia.Refresh();
        }
    }
    public void Reset()
    {
        for (int i = 0; i < odblokowane_bronki.Length; i++)
        {
            // temp solution
            odblokowane_bronki[1] = false;
        }
    }
    public void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            staty = this;
            ScoreUI = GameObject.FindGameObjectWithTag("score");
            StartGameStats();
            if (GameObject.FindGameObjectWithTag("Ustawienia").GetComponent<UstawieniaMenu>().Jezyk == 1) coinsPLENG = "Score: ";
            else coinsPLENG = "Punkty: ";
        }
        if (level == 3)
        {
            staty = this;
            SaveData();
        }
        if (level == 0)
        {
            staty = this;
        }
    }

}

[Serializable]
class gameData
{
    public int highscore;
    public float glosnoscMuzyki;
    public int jezyk;
}
