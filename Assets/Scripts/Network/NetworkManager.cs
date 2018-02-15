using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;

public class NetworkManager : MonoBehaviour {

    public string IP = "127.0.0.1";
    public int Port = 4296;

    // Use this for initialization
    void Start()
    {

        DarkRiftAPI.workInBackground = true;
        DarkRiftAPI.Connect(IP, Port);

    }

    private void OnApplicationQuit()
    {
        DarkRiftAPI.Disconnect();
    }
}
