using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSystem : MonoBehaviour {

    public enum SpawnState {  SPAWNING, WAITING, COUNTING };
    
    [System.Serializable]
    public class Wave
    {
        public string nazwa;
        public GameObject enemy;
        public int liczebnosc;
        public float tempo;
        public string muzyka;
        public float PrzerwaPrzedSpawnem;
    }

    public Wave[] waves;
    
    public float fala_odliczanie;
    public GameObject hpbarprefab;
    public RectTransform barpanel;
    public GameObject sp_prefab;
    public int LiczbaDoWyswietlenia;
    public GameObject UfoSpecjalne;

    private GameObject[] ufo = new GameObject[20];
    private GameObject[] ufaboczne;
    private TextMeshProUGUI mtext;
    private Transform figurki;
    private int level;
    private float odliczanie_text;
    private Animator odliczanie_text_animator;

    public int EnemyCount;
    public GameObject MusicPlayer;
    public GameObject GameInfoText;

    private bool UfoSpecialManouver;
    private float searchtimer = 1f;
    private SpawnState state = SpawnState.COUNTING;
    public Transform[] spawnpoint = new Transform[40];
    private int faza = 1;

    private void Awake()
    {
        GameInfoText = GameObject.FindGameObjectWithTag("CanvasGame").gameObject.transform.GetChild(3).gameObject;
        GameInfoText.SetActive(false);
     //   MusicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
    }

    void Start()
    {
        UfoSpecialManouver = false;
        EnemyCount = 20;
        odliczanie_text = 1;
        ufaboczne = GameObject.FindGameObjectsWithTag("ufobok");
        mtext = GameInfoText.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        figurki = GameObject.FindGameObjectWithTag("figurki").transform;
        GameInfoText.SetActive(true);
        level = 1;
        odliczanie_text_animator = GameInfoText.transform.GetChild(2).gameObject.GetComponent<Animator>();
        spawnpoints();
        if (spawnpoint.Length == 0)
        {
            Debug.LogError("weno ustaw spawnpointy plz");
        }
    }

    void Update()
    {
        if (GameInfoText != null)
        {

        }
     //   Debug.Log("fala odliczanie: " + (fala_odliczanie + 0.5f));
        if (state == SpawnState.WAITING)
        {
            if (faza==2)
            {

            }

            if (!EnemyIsAlive())
            {
                Wavekompleted();
            }
            else
            {
                return;
            }
        }
        
        if(fala_odliczanie <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
              //  odliczanie_text_animator.SetBool("odliczanie", false);
                GameInfoText.SetActive(false);

                StartCoroutine(Spawn(waves[faza-1]));
            }
        }
        else
        {
            fala_odliczanie -= Time.deltaTime;
            if (fala_odliczanie < 3 && fala_odliczanie > 0.2f)
            {
                AnimateText();
            }
            if (fala_odliczanie < 0.2f || fala_odliczanie > 3)
            {
                odliczanie_text_animator.SetBool("odliczanie", false);
            }
            PlayMusic();
        }
        
    }
    public void SetMusicPlayer()
    {
        MusicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
    }


    void Wavekompleted()
    {
        Debug.Log("Wave kompleted");
        state = SpawnState.COUNTING;

        if(faza==4)
        {
            Debug.Log("faza 4 kompleted");
            GameObject[] pociski = GameObject.FindGameObjectsWithTag("pocisk");
            foreach(GameObject pocisk in pociski)
            {
                Destroy(pocisk);
            }
            ufaboczne[0].GetComponent<UfoBokStrzelanie>().PowrotDoPoczatkowejPozycji();
            ufaboczne[1].GetComponent<UfoBokStrzelanie>().PowrotDoPoczatkowejPozycji();
        }

        if (faza == waves.Length)
        {
            faza = 1;
            statystyki.staty.Faza = 1;
            NextLevel();
        }
        else
        {
            faza++;
            statystyki.staty.Faza++;
        }

        if (faza == 2) EnemyCount = 20;
        if (faza == 3) ZmianaFigurki("asuka");
        else ZmianaFigurki("ufo");

        if (faza == 4)
        {
            GameInfoText.SetActive(false);
            AtakUfo();
        }
        else GameInfoText.SetActive(true);

        fala_odliczanie = waves[faza-1].PrzerwaPrzedSpawnem;
        SwitchBackgroundImage();
        if (UstawieniaMenu.Ustawienia.Jezyk == 1)
        {
            GameInfoText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "LEVEL " + level.ToString();
            GameInfoText.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "PHASE " + faza.ToString();
        }
        else
        {
            GameInfoText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "POZIOM " + level.ToString();
            GameInfoText.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "FAZA " + faza.ToString();
        }
        mtext.text = 3.ToString();
        odliczanie_text = 1;

    }

    private bool EnemyIsAlive()
    {
        searchtimer -= Time.deltaTime;
        if (searchtimer <= 0f)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            EnemyCount = enemies.Length;
            searchtimer = 1f;
            if (faza == 2 && EnemyCount < 11 && UfoSpecialManouver == false)
            {
                UfoSpecialManouver = true;
                gameObject.GetComponent<lvl2manouver>().ManewrRozpoczety = true;
            }
            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator Spawn(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        for (int i=0;i < _wave.liczebnosc; i++)
        {
            if(_wave.enemy.name=="ufo")
            {
                
                ufo[i] = SpawnEnemy(_wave.enemy, i);
            }
            if (_wave.enemy.name == "asuka") SpawnEnemy_Asuka(_wave.enemy);
            yield return new WaitForSeconds(_wave.tempo);
        }
        
        state = SpawnState.WAITING;

        yield break;
    }

    void spawnpoints()
    {
        Vector2 pozycja = new Vector3(167.5f, 153f);
        for (int i = 0; i < 20; i++)
        {
            GameObject _spawnpoint = Instantiate(sp_prefab, pozycja, sp_prefab.transform.rotation) as GameObject;
            spawnpoint[i] = _spawnpoint.transform;
            pozycja.x += 1.54f;
            if (i == 9)
            {
                pozycja.x = 167.5f;
                pozycja.y -= 1.5f;
            }
        }
    }

    public void AnimateText()
    {
        odliczanie_text_animator.SetBool("odliczanie", true);
        odliczanie_text += Time.deltaTime;
        if (odliczanie_text >= 1)
        {
            if (((int)(fala_odliczanie + 0.5f)) == 3)
            {
                mtext.text = ((int)fala_odliczanie + 1).ToString();
                odliczanie_text = 0.2f;
            }
            else
            {
                mtext.text = ((int)fala_odliczanie).ToString();
                odliczanie_text = 0;
            }
        }
    }
    public void PlayMusic()
    {

        string aktualnaMuzyka = MusicPlayer.GetComponent<start>().what_is_playing();
        if (waves[faza-1].muzyka != aktualnaMuzyka)
        {
            MusicPlayer.GetComponent<start>().odtwarzacz(waves[faza-1].muzyka);
        }
    }
    public void NextLevel()
    {
        statystyki.staty.level++;
        level++;
    }
    public void SwitchBackgroundImage()
    {
        switch (faza)
        {
            case 1:
                Debug.Log("ss ufo");
                MusicPlayer.GetComponent<start>().SetImageBackground("ufo");
                break;
            case 2:
                MusicPlayer.GetComponent<start>().SetImageBackground("ufo");
                break;
            case 3:
                MusicPlayer.GetComponent<start>().SetImageBackground("NGE");
                break;
            case 4:
                MusicPlayer.GetComponent<start>().SetImageBackground("ufo");
                break;
        }
    }
    private void ZmianaFigurki(string figurka)
    {
        if(figurka=="asuka")
        {
            figurki.transform.GetChild(0).gameObject.SetActive(false);
            figurki.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (figurka == "ufo")
        {
            figurki.transform.GetChild(0).gameObject.SetActive(true);
            figurki.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
    private void AtakUfo()
    {
        figurki.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        figurki.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Animator>().enabled = true;
        StartCoroutine(UfoAtak());
    }

    IEnumerator UfoAtak()
    {
        figurki.transform.GetChild(0).GetChild(0).transform.Rotate(0, 0, 45);
        figurki.transform.GetChild(0).GetChild(0).transform.Rotate(0, 0, -45);
        for (int i = 0; i < 135; i++)
        {
            ufaboczne = GameObject.FindGameObjectsWithTag("ufobok");
            if(i==30) UfoSpecjalne.SetActive(true);
            figurki.transform.GetChild(0).GetChild(0).transform.Rotate(0, 0, -1);
            figurki.transform.GetChild(0).GetChild(1).transform.Rotate(0, 0, 1);
            figurki.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().speed += 0.03f;
            figurki.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Animator>().speed += 0.03f;
            yield return new WaitForSeconds(0.04f);
        }
        GameInfoText.SetActive(true);
        ufaboczne[0].GetComponent<UfoBokStrzelanie>().strzelanko = true;
        ufaboczne[1].GetComponent<UfoBokStrzelanie>().strzelanko = true;
        yield break;
    }


    GameObject SpawnEnemy(GameObject _enemy, int numer_enemy)
    {
        Transform _sp = spawnpoint[numer_enemy];
       GameObject statek = Instantiate(_enemy, _sp.position, _sp.rotation);
        return statek;
    //    GenerateHealthBar(enemy.transform);
        
    }
    void SpawnEnemy_Asuka(GameObject _enemy)
    {
        GameObject enemy = Instantiate(_enemy, new Vector3(174, 154), Quaternion.identity);
        enemy.name = "asuka";
    //    GenerateHealthBar(enemy.transform);
    }
}
