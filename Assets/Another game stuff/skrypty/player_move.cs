using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Dictionary;
using System;

public class player_move : MonoBehaviour {
    public int playerSpeed = 10;

    public float moveX;
    Animator anim;
    public bool blokada;
    private AudioSource damage;
    private AudioSource smierc;
    private AudioSource CoinCollected;
    private Rigidbody2D playerRB;
    //lerp
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance = 0.5f;
    public float lerpTime = 0.5f;
    public float currentLerpTime = 0;
    public bool moveUp = false;
    public bool moveDown = false;
    public int PozycjaY;


    

    //
    void Start () {
        PozycjaY = 1;
        blokada = false;
        anim = GetComponent<Animator>();
        AudioSource[] asources = GetComponents<AudioSource>();
        damage = asources[0];
        smierc = asources[1];
        CoinCollected = asources[2];
        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {

        PlayerMove();

    }
    void Update () {
    }
    void PlayerMove()
    {
        //PORUSZANIE
        moveX = Input.GetAxisRaw("Horizontal");
        //ANIMACJE

        //FIZYKA
        if (!blokada)
        {

            Vector3 pos = playerRB.position;
            pos.x = Mathf.Clamp(pos.x, 167.4839f, 181.6765f);
            playerRB.position = pos;
            // kinematic, ale tez sie slizga

            //Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Horizontal"));
            //Vector3 moveDirection = transform.TransformDirection(inputDirection);
            //playerRB.MovePosition(transform.position + moveDirection * playerSpeed * Time.deltaTime);

            playerRB.velocity = new Vector2(moveX * playerSpeed, playerRB.velocity.y);

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (PozycjaY < 5)
                {
                    PozycjaY++;
                    currentLerpTime = 0;
                    moveUp = true;
                    startPos = transform.position;
                    endPos = transform.position + Vector3.up * distance;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (PozycjaY > 1)
                {
                    PozycjaY--;
                    currentLerpTime = 0;
                    moveDown = true;
                    startPos = transform.position;
                    endPos = transform.position + Vector3.down * distance;
                }
            }
        }
        if (moveUp)
        {
            blokada = true;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
                moveUp = false;
                blokada = false;
            }
            float Perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }
        if (moveDown)
        {
            blokada = true;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
                moveDown = false;
                blokada = false;
            }
            float Perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }
        ////freezowanie po puszczeniu strzalki
        //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    playerRB.constraints = RigidbodyConstraints2D.None;
        //}
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "bomba")
        {
            statystyki.staty.ChangeHp(-20);
            Destroy(collision.gameObject);
            if (statystyki.staty.Hp > 0)
            {
                damage.Play();
            }
            else if(statystyki.staty.Hp<=0)
            {
                smierc.Play();
            }
        }
        if (collision.gameObject.name == "coin")
        {
            CoinCollected.Play();
            statystyki.staty.Coins += 1;
            statystyki.staty.AddScore(3);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "grzyb")
        {
            statystyki.staty.ChangeHp(-10);
            Destroy(collision.gameObject);
            if (statystyki.staty.Hp > 0)
            {
                damage.Play();
            }
            else if (statystyki.staty.Hp <= 0)
            {
                smierc.Play();
            }
        }

    }

    public void Die()
    {
        anim.SetTrigger("wybuchanie");
        playerRB.velocity = new Vector2(0, 0);
        blokada = true;
        GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<start>().Pause();
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        StartCoroutine(Wait(3));
    }
    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("ekrankoncowy");
        statystyki.staty.SaveData();
    }
    public void LewoAndroid()
    {
        Debug.Log("Wcisnieto Lewo");
        moveX = -1;
    }
    public void AndroidPuszczony()
    {
        Debug.Log("Android Puszczony");
        moveX = 0;
    }
    public void PrawoAndroid()
    {
        Debug.Log("Wcisnieto Prawo");
        moveX = 1;
    }
    public void GoraAndroid()
    {
        if (!blokada)
        {
            if (PozycjaY < 5)
            {
                PozycjaY++;
                currentLerpTime = 0;
                moveUp = true;
                startPos = transform.position;
                endPos = transform.position + Vector3.up * distance;
            }
        }
        Debug.Log("Wcisnieto Gora");
        Debug.Log(PozycjaY);
    }
    public void DolAndroid()
    {
        if (!blokada)
        {
            if (PozycjaY > 1)
            {
                PozycjaY--;
                currentLerpTime = 0;
                moveDown = true;
                startPos = transform.position;
                endPos = transform.position + Vector3.down * distance;
            }
        }
        Debug.Log("Wcisnieto Dol");
        Debug.Log(PozycjaY);
    }
}
