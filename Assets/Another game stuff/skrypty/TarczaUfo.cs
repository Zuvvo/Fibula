using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarczaUfo : MonoBehaviour {
    public float CzasDoZnikniecia;

    private float LicznikCzasowy = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LicznikCzasowy += Time.deltaTime;
        if (LicznikCzasowy >= CzasDoZnikniecia) gameObject.SetActive(false);
		
	}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "pociskkukurydza")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "pociskdefault")
        {
            Destroy(collision.gameObject);
        }
    }
}
