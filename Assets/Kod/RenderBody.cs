using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderBody : MonoBehaviour {

    public GameObject BodyEditor;
    public GameObject PreviewHolder;
    private int NumberOfElements = 5;

    private enum BodyPart {Hair, Skin, Body, Legs, Boots};
    private BodyPart SelectedBodyPart = BodyPart.Hair;

    private SpriteRenderer[] Sprites;
    private Slider[] Sliders;
    private Color[] Colors;

    void Start() {
        InitializeElements();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) RunPanel();
        if (BodyEditor.active) SetColors();
    }

    private void InitializeElements()
    {
        Sliders = new Slider[3];
        Sprites = new SpriteRenderer[NumberOfElements];
        Colors = new Color[NumberOfElements];
        for(int i = 0; i < NumberOfElements; i++)
        {
            Sprites[i] = transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>();
            if(i<3) Sliders[i] = BodyEditor.transform.GetChild(i).GetComponent<Slider>();
            Colors[i] = new Color(255, 255, 255, 255);
        }
    }
    public void SetColorSliders()
    {
        Colors[(int)SelectedBodyPart] = new Color(Sliders[0].value, Sliders[1].value, Sliders[2].value, 255);
        PreviewHolder.transform.GetChild((int)SelectedBodyPart).GetComponent<Image>().color = new Color(Sliders[0].value, Sliders[1].value, Sliders[2].value, 255);
        
    }
    private void RunPanel()
    {
        if (!BodyEditor.active)
        {
            Debug.Log("Panel activated");
            SelectedBodyPart = BodyPart.Hair;
            BodyEditor.SetActive(true);
            SetPositionOfSliders();
            SetPreview();
        }
        else BodyEditor.SetActive(false);
    }
    private void SetColors()
    {
        for (int i = 0; i < NumberOfElements; i++)
        {
            Sprites[i].color = Colors[i];
        }
    }
    private void SetPositionOfSliders()
    {
        Sliders[0].value = Sprites[(int)SelectedBodyPart].color.r;
        Sliders[1].value = Sprites[(int)SelectedBodyPart].color.g;
        Sliders[2].value = Sprites[(int)SelectedBodyPart].color.b;
    }
    public void SetPreview()
    {
        for(int i = 0; i < Sprites.Length; i++)
        {
            PreviewHolder.transform.GetChild(i).GetComponent<Image>().color = Colors[4-i];
        }
    }
    public void ButtonHair()
    {
        SelectedBodyPart = BodyPart.Hair;
        SetPositionOfSliders();
    }
    public void ButtonSkin()
    {
        SelectedBodyPart = BodyPart.Skin;
        SetPositionOfSliders();
    }
    public void ButtonBody()
    {
        SelectedBodyPart = BodyPart.Body;
        SetPositionOfSliders();
    }
    public void ButtonLegs()
    {
        SelectedBodyPart = BodyPart.Legs;
        SetPositionOfSliders();
    }
    public void ButtonBoots()
    {
        SelectedBodyPart = BodyPart.Boots;
        SetPositionOfSliders();
    }
    public void RandomColors()
    {
        if(PreviewHolder.transform.GetComponent<Animator>().enabled == false)
        PreviewHolder.transform.GetComponent<Animator>().enabled = true;
        else PreviewHolder.transform.GetComponent<Animator>().enabled = false;
        float[] rnd = new float[Colors.Length];

        for (int i = 0; i < Colors.Length; i++)
        {
            for (int j = 0; j < Colors.Length; j++)
            {
                rnd[j] = Random.Range(0f, 1f);
            }
            Colors[i] = new Color(rnd[0], rnd[1], rnd[2], 255f);
        }
        SetColors();
        SetPositionOfSliders();
        SetPreview();
    }
}
