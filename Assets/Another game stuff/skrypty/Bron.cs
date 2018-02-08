using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bron : MonoBehaviour {
    private int lvl = 1;
    private enum weaponType { podstawowa, kukurydza };
    public GameObject obrazekzwybranabronia;
    public int Lvl { get { return lvl; }}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void wybierzbron()
    {
        //int i = 0;
        //foreach (Transform weapon in transform)
        //{
        //    if (i == wybranabron)
        //    {
        //        weapon.gameObject.SetActive(true);
        //        obrazekzwybranabronia.transform.GetChild(i).transform.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        weapon.gameObject.SetActive(false);
        //        obrazekzwybranabronia.transform.GetChild(i).transform.gameObject.SetActive(false);
        //    }
        //    i++;
        //}
    }
    public void Upgrade()
    {
        lvl++;
    }
    public void Downgrade()
    {
        lvl--;
    }
}
