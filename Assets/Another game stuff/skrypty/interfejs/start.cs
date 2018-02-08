using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour {

    public AudioSource _AudioSource1;
    public AudioSource _AudioSource2;
    public AudioSource Chest;
    public AudioSource ReloadWeapon;
    public AudioSource MenuTheme;
    public Material UfoBackground;
    public Material NGEBackground;
    public string WhatIsPlaying;
    public GameObject tło;

    public GameObject GlosnoscUstawienia;
    private float Glosnosc;

    // Use this for initialization
    void Start () {
        GlosnoscUstawienia = GameObject.FindGameObjectWithTag("Ustawienia");
        MenuTheme.Play();
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            MenuTheme.Play();
        }
        if (level == 1)
        {
            MenuTheme.Stop();
        }
        if (level == 2)
        {
            _AudioSource1.Stop();
            _AudioSource2.Stop();
            _AudioSource1.Play();
            tło = GameObject.FindGameObjectWithTag("tlo");
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnSystem>().SetMusicPlayer();
        }
        if (level == 3)
        {
            _AudioSource1.Stop();
            _AudioSource2.Stop();
        }
    }
    public float glosnosc
    {
        set
        {
            Glosnosc = value;
            AktualizujGlosnoscMuzyki();
        }
    }
    public void AktualizujGlosnoscMuzyki()
    {
        _AudioSource1.volume = Glosnosc;
        _AudioSource2.volume = Glosnosc;
        MenuTheme.volume = Glosnosc;
    }
    public void asuka_background()
    {
        _AudioSource1.Stop();
        _AudioSource2.Play();
        WhatIsPlaying = "asuka";
    }
    public void ufo_background()
    {
        _AudioSource2.Stop();
        _AudioSource1.Play();
        WhatIsPlaying = "ufo";
    }
    public string what_is_playing()
    {
        return WhatIsPlaying;
    }
    public void odtwarzacz(string muzyka)
    {
        if (muzyka == "asuka") asuka_background();
        if (muzyka == "ufo") ufo_background();
    }
    public void Pause()
    {
        if (what_is_playing() == "asuka") _AudioSource2.Pause();
        if (what_is_playing() == "ufo") _AudioSource1.Pause();
    }
    public void Resume()
    {
        if (what_is_playing() == "asuka") _AudioSource2.Play();
        if (what_is_playing() == "ufo") _AudioSource1.Play();
    }

    public void PlayChestSound()
    {
        Chest.Play();
    }
    public void PlayReloadWeapon()
    {
        ReloadWeapon.Play();
    }
    public void SetImageBackground(string backgroundname)
    {
        switch (backgroundname)
        {
            case "ufo":
                tło.GetComponent<MeshRenderer>().material = UfoBackground;
                break;
            case "NGE":
                tło.GetComponent<MeshRenderer>().material = NGEBackground;
                break;
        }
    }
    private void Update()
    {

    }
}
