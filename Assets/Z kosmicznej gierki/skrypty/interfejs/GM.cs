using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    private static bool exist = false;



    private void Awake()
    {
        if (exist) Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        exist = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            GameObject.FindGameObjectWithTag("CanvasGame").gameObject.transform.GetChild(2).GetComponent<pauza>().SetMusicPlayer();
        }
    }
}
