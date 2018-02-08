using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauza : MonoBehaviour {
  //  public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject odtwarzaczMuzyki;
    public GameObject[] teksty;
    public bool GameIsPaused = false;

    // Use this for initialization
    void Start ()
    {
        SetMenuText(UstawieniaMenu.Ustawienia.Jezyk);
    }

    public void SetMusicPlayer()
    {
        odtwarzaczMuzyki = GameObject.FindGameObjectWithTag("MusicPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Start")
        {
            if (GameIsPaused)
            {
                pauseMenuUI.GetComponent<pauza>().Resume();
            }
            else
            {
                pauseMenuUI.GetComponent<pauza>().Pause();
            }
        }
    }
    public void Resume()
    {
        odtwarzaczMuzyki.GetComponent<start>().Resume();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        odtwarzaczMuzyki.GetComponent<start>().Pause();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        statystyki.staty.Reset();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        statystyki.staty.Reset();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
    public void AndroidPause()
    {
        if (GameIsPaused)
        {
            pauseMenuUI.GetComponent<pauza>().Resume();
        }
        else
        {
            pauseMenuUI.GetComponent<pauza>().Pause();
        }
    }
    public void SetMenuText(int language)
    {

        foreach (GameObject element in teksty)
        {
            if (language == 0)
            {
                element.transform.GetChild(0).gameObject.SetActive(true);
                element.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                element.transform.GetChild(1).gameObject.SetActive(true);
                element.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }
}
