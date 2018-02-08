using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour {

    public GameObject Zaciemniacz;
    public GameObject[] Scena;
    public AudioSource music;

    private Animator _Animator;

    void Start () {
        ChangeLanguage(UstawieniaMenu.Ustawienia.Jezyk);
        music.volume *= UstawieniaMenu.Ustawienia.glosnoscmuzyki;
        Zaciemniacz.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        _Animator = gameObject.transform.GetChild(4).GetComponent<Animator>();
        StartCoroutine(AnimateStory());
    }
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            SkipStory();
        }
    }
    public void SkipStory()
    {
        SceneManager.LoadScene("Start");
    }
    public void ZaciemnianieON()
    {

    }
    public void Odslanianie()
    {
        _Animator.SetBool("odslanianie", true);
    }
    IEnumerator AnimateStory()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(3f);
                _Animator.SetBool("START", true);
                yield return new WaitForSeconds(3f);
                _Animator.SetBool("START", false);
                yield return new WaitForSeconds(10f);
                SceneManager.LoadScene("Start");
            }
            else
            {

                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSeconds(3f);
                _Animator.SetBool("START", true);
                yield return new WaitForSeconds(8f);
                _Animator.SetBool("START", false);
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        yield break;
    }
    public void ChangeLanguage(int language)
    {
        foreach(GameObject element in Scena)
        {
            if (language == 0)
            {

                element.transform.GetChild(language).gameObject.SetActive(true);
                element.transform.GetChild(language+1).gameObject.SetActive(false);
            }
            else
            {
                element.transform.GetChild(language).gameObject.SetActive(true);
                element.transform.GetChild(language-1).gameObject.SetActive(false);
            }
        }
    }
}
