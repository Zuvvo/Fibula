using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTest : MonoBehaviour {

    float RotateTimer = 0;
    RaycastHit2D hitup;
    RaycastHit2D hitright;
    RaycastHit2D hitdown;
    RaycastHit2D hitleft;
    public Vector3 pos;
    private int rotation;
    public BoxCollider2D boxCollider;

    Spider spider = new Spider();

    // Dane pajaka lokalnego
    public int hp;
    public float moveSpeed;
    public FibulaPosition position;





    public bool move = false;
    // Use this for initialization
    void Start () {
        spider.position.x = (int)transform.position.x;
        spider.position.y = (int)transform.position.y;
        MapSystem.Map1Server[spider.position.x, spider.position.y].isBlocked = true;
        MapSystem.Map1Local[spider.position.x, spider.position.y].isBlocked = true;

        hp = spider.hp;
        moveSpeed = spider.moveSpeed;
        position.x = spider.position.x;
        position.y = spider.position.y;

        MapSystem.ObjListLocal.Add(spider);
        MapSystem.ObjListSerwer.Add(spider);
        pos = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        hitup = Physics2D.Raycast(transform.position, Vector2.up, 1);
        hitdown = Physics2D.Raycast(transform.position, Vector2.down, 1);
        hitright = Physics2D.Raycast(transform.position, Vector2.right, 1);
        hitleft = Physics2D.Raycast(transform.position, Vector2.left, 1);
        RotateTimer += Time.deltaTime;
        if(RotateTimer > 1f)
        {
            Rotate();
            RotateTimer = Random.Range(0,0.5f);
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
        gameObject.transform.Rotate(0,0,angles[RandomIndex]);
        pos = transform.position;
        rotation = (int)gameObject.transform.eulerAngles.z;

        //   Debug.Log(Time.time + "   " + gameObject.name + " rotation:  " + (int)gameObject.transform.eulerAngles.z);
        //Debug.Log(gameObject.name + "  down: " + hitdown.distance);
        //Debug.Log(gameObject.name + "  up: " + hitup.distance);
        //Debug.Log(gameObject.name + "  left: " + hitleft.distance);
        //Debug.Log(gameObject.name + "  right: " + hitright.distance);


        CheckforMoving();

    //    Debug.Log(Time.time + "   " + gameObject.name + "  " + transform.position + "    " + pos + "move: " + move.ToString());
    }
    public void GoForward(float _speed)
    {
        float DistanceToTarget = 0;
        if(pos != transform.position)
        {
            if(transform.position.x != pos.x)
            {
                DistanceToTarget = pos.x - transform.position.x;
            }
            if(transform.position.y != pos.y)
            {
                DistanceToTarget = pos.y - transform.position.y;
            }
            if (DistanceToTarget > 0) DistanceToTarget *= -1;
            boxCollider.offset = new Vector2(0, DistanceToTarget+0.04f);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, _speed * Time.deltaTime);
        if (pos == transform.position)
        {
            pos = transform.position;
            boxCollider.offset = new Vector2(0, 0);
          //  Debug.Log(gameObject.name + "stopped moving, Endpos: " + pos + "currentPos: " + transform.position);
            move = false;
        }
    }

    private void CheckforMoving()
    {
        switch (rotation)
        {
            case 0:
                if (MapSystem.Map1Server[spider.position.x,spider.position.y-1].isBlocked==false)
                {
                    MapSystem.Map1Server[spider.position.x, spider.position.y].isBlocked = false;
                    MapSystem.Map1Server[spider.position.x, spider.position.y - 1].isBlocked = true;
                    spider.position.y -= 1;
                    pos += Vector3.down;
                    move = true;
                }
                else move = false;
                break;

            case 90:
                if (MapSystem.Map1Server[spider.position.x+1, spider.position.y].isBlocked == false)
                {
                    MapSystem.Map1Server[spider.position.x, spider.position.y].isBlocked = false;
                    MapSystem.Map1Server[spider.position.x+1, spider.position.y].isBlocked = true;
                    spider.position.x += 1;
                    pos = pos += Vector3.right;
                    move = true;
                }
                else move = false;
                break;
            case 180:
                if (MapSystem.Map1Server[spider.position.x, spider.position.y+1].isBlocked == false)
                {
                    MapSystem.Map1Server[spider.position.x, spider.position.y].isBlocked = false;
                    MapSystem.Map1Server[spider.position.x, spider.position.y+1].isBlocked = true;
                    spider.position.y += 1;
                    pos = pos += Vector3.up;
                    move = true;
                }
                else move = false;
                break;
            case 270:
                if (MapSystem.Map1Server[spider.position.x -1, spider.position.y].isBlocked == false)
                {
                    MapSystem.Map1Server[spider.position.x, spider.position.y].isBlocked = false;
                    MapSystem.Map1Server[spider.position.x-1, spider.position.y].isBlocked = true;
                    spider.position.x -= 1;
                    pos = pos += Vector3.left;
                    move = true;
                }
                else move = false;
                break;
        }
    }
}
