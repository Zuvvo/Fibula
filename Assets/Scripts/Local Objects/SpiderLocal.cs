using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLocal : MonoBehaviour {



    float RotateTimer = 0;
    public Vector3 pos;
    private int rotation;
    MapMoving moving;

    Spider spider = new Spider();

    // Dane pajaka lokalnego
    public int hp;
    public float moveSpeed;
    public FibulaPosition position;
    FibulaPosition endPosition;

    public bool move = false;
    // Use this for initialization
    void Start()
    {
        spider.position.x = (int)transform.position.x;
        spider.position.y = (int)transform.position.y;
        MapSystem.Map1Server[spider.position.x, spider.position.y].isBlocked = true;
        MapSystem.Map1Local[spider.position.x, spider.position.y].isBlocked = true;
        MapSystem.Map1Server[spider.position.x, spider.position.y].EnemyOnTile = spider;

        hp = spider.hp;
        moveSpeed = spider.moveSpeed;
        position.x = spider.position.x;
        position.y = spider.position.y;

        MapSystem.ObjListLocal.Add(spider);
        MapSystem.ObjListSerwer.Add(spider);
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTimer += Time.deltaTime;
        if (RotateTimer > 1f)
        {
            Rotate();
            RotateTimer = Random.Range(0, 0.5f);
        }
        if (move)
        {
            GoForward(moveSpeed);
        }
    }

    public void Rotate()
    {
        int[] angles = new int[] { 90, 180, 270 };
        int RandomIndex = Random.Range(0, 3);
        gameObject.transform.Rotate(0, 0, angles[RandomIndex]);
        pos = transform.position;
        rotation = (int)gameObject.transform.eulerAngles.z;
        CheckforMoving();

        //    Debug.Log(Time.time + "   " + gameObject.name + "  " + transform.position + "    " + pos + "move: " + move.ToString());
    }
    public void GoForward(float _speed)
    {
        float DistanceToTarget = 0;
        if (pos != transform.position)
        {
            if (transform.position.x != pos.x)
            {
                DistanceToTarget = pos.x - transform.position.x;
            }
            if (transform.position.y != pos.y)
            {
                DistanceToTarget = pos.y - transform.position.y;
            }
            if (DistanceToTarget > 0) DistanceToTarget *= -1;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, _speed * Time.deltaTime);
        if (pos == transform.position)
        {
            pos = transform.position;
            //  Debug.Log(gameObject.name + "stopped moving, Endpos: " + pos + "currentPos: " + transform.position);
            move = false;
        }
    }

    private void CheckforMoving()
    {
        switch (rotation)
        {
            case 0:
                endPosition = spider.position;
                endPosition.y -= 1;
                if (MapMoving.MoveTo(spider.position, endPosition, spider))
                {
                    pos += Vector3.down;
                    move = true;
                }
                else move = false;
                break;
            case 90:
                endPosition = spider.position;
                endPosition.x += 1;
                if (MapMoving.MoveTo(spider.position, endPosition, spider))
                {
                    pos += Vector3.right;
                    move = true;
                }
                else move = false;
                break;
            case 180:
                endPosition = spider.position;
                endPosition.y += 1;
                if (MapMoving.MoveTo(spider.position, endPosition, spider))
                {
                    pos += Vector3.up;
                    move = true;
                }
                else move = false;
                break;
            case 270:
                endPosition = spider.position;
                endPosition.x -= 1;
                if (MapMoving.MoveTo(spider.position, endPosition, spider))
                {
                    pos += Vector3.left;
                    move = true;
                }
                else move = false;
                break;
        }
    }
}
