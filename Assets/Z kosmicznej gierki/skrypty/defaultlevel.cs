using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class defaultlevel : MonoBehaviour
{
    public int level = 1;
    public GameObject defaultWeapon;
    public GameObject bronki;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Activate()
    {
        defaultWeapon.SetActive(true);
    }
    public void Deactivate()
    {
        defaultWeapon.SetActive(false);
    }
    public void Upgrade()
    {
        if (level < 3)
        {
            level++;

            if(level==2) defaultWeapon.GetComponent<strzelaniedefault>().tempo_strzelanka = 0.15f;
            if (level == 3) defaultWeapon.GetComponent<strzelaniedefault>().tempo_strzelanka = 0.1f;
            if (bronki.GetComponent<zamianabroni>().wybranabron == 0) LvlUI(level);
        }
    }
    public void Refresh()
    {
        Activate();
        LvlUI(level);
    }
    public void LvlUI(int lvl)
    {
        if (bronki.GetComponent<zamianabroni>().wybranabron == 0)
        {
            GameObject lvlUi = GameObject.FindGameObjectWithTag("lvlbroni");
            TextMeshProUGUI mText = lvlUi.gameObject.GetComponent<TextMeshProUGUI>();
            mText.text = "lvl " + lvl.ToString();
        }
    }
}
