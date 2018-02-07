using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {
    float RotateTimer = 0;
    RaycastHit2D hitup;
    RaycastHit2D hitright;
    RaycastHit2D hitdown;
    RaycastHit2D hitleft;
    public Vector3 pos;
    private float speed = 2f;
    private int rotation;
    public BoxCollider2D boxCollider;

    public bool move = false;
    // Use this for initialization
    void Start () {
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
            GoForward(speed);
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
            boxCollider.offset = new Vector2(0, DistanceToTarget);
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
                if (hitdown.collider == null)
                {
                    pos += Vector3.down;
                    move = true;
                }
                else move = false;
                break;

            case 90:
                if (hitright.collider == null)
                {
                    pos = pos += Vector3.right;
                    move = true;
                }
                else move = false;
                break;
            case 180:
                if (hitup.collider == null)
                {
                    pos = pos += Vector3.up;
                    move = true;
                }
                else move = false;
                break;
            case 270:
                if (hitleft.collider == null)
                {
                    pos = pos += Vector3.left;
                    move = true;
                }
                else move = false;
                break;
        }
    }
}
