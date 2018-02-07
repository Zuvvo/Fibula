using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kukurydzalevel : MonoBehaviour {
    public int level = 0;
    public GameObject kukurydzaLeft;
    public GameObject kukurydzaRight;
    public GameObject kukurydzaCenter;
    public GameObject kukurydzaPanel;
    public GameObject bronki;
    // Use this for initialization
    void Start () {
        KukurydzaActivate(level);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void KukurydzaActivate(int lvl)
    {
        switch (lvl)
        {
            case 1:
                kukurydzaCenter.SetActive(true);
                kukurydzaRight.SetActive(false);
                kukurydzaLeft.SetActive(false);
                break;
            case 2:
                kukurydzaCenter.SetActive(false);
                kukurydzaRight.SetActive(true);
                kukurydzaLeft.SetActive(true);
                break;
            case 3:
                kukurydzaCenter.SetActive(true);
                kukurydzaRight.SetActive(true);
                kukurydzaLeft.SetActive(true);
                break;
        }

    }
    public void Deactivate()
    {
        kukurydzaPanel.SetActive(false);
    }
    public void Upgrade()
    {
        if(level<3)
        {
            level++;
            if(bronki.GetComponent<zamianabroni>().wybranabron==1) LvlUI(level);
        }
    }
    public void Refresh()
    {
        KukurydzaActivate(level);
        LvlUI(level);
    }
    public void LvlUI(int lvl)
    {
        if (bronki.GetComponent<zamianabroni>().wybranabron == 1)
        {
            GameObject lvlUi = GameObject.FindGameObjectWithTag("lvlbroni");
            TextMeshProUGUI mText = lvlUi.gameObject.GetComponent<TextMeshProUGUI>();
            mText.text = "lvl " + lvl.ToString();
        }
    }
}
