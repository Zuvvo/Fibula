using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour {

    GameObject[] GUIElements;


    void Start()
    {
        GUIElements = new GameObject[transform.GetChildCount()];

        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            GUIElements[i] = transform.GetChild(i).gameObject;
        }
    }








}

