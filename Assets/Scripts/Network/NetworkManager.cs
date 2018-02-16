using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkRift;
using System;

public class NetworkManager : MonoBehaviour {


    public InputField IPInput;
    public InputField PortInput;
    private string IP = "127.0.0.1";
    private int Port = 4296;

    // Use this for initialization
    void Start()
    {

    }

    private void OnApplicationQuit()
    {
        DarkRiftAPI.Disconnect();
    }
    public void ConnectToServer()
    {
            DarkRiftAPI.workInBackground = true;
            if (IPInput.text == "") IPInput.text = IP;
            if (PortInput.text == "") PortInput.text = Port.ToString();
            DarkRiftAPI.Connect(IPInput.text, Convert.ToInt32(PortInput.text));
    }
}
