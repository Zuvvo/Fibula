using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speed = 2f;

    public enum MovingDirection { Left, Right, Up, Down, Idle };
    public MovingDirection CurrentMoveDirection;
    
    private Animator animator;
    private float DistanceToTarget = 0;
    FibulaPlayer player = new FibulaPlayer();

    private FibulaPosition endPosition;


    void Start()
    {
        player.position.x = (int)transform.position.x;
        player.position.y = (int)transform.position.y;
        endPosition = player.position;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log((int)Time.time + ": Player position: " + player.position.x + " " + player.position.y);
        Debug.Log((int)Time.time + ": end position: " + endPosition.x + " " + endPosition.y);

        if (Input.GetKeyDown(KeyCode.Z)) transform.position = NormalizePosition(transform.position); // w razie zablokowania xd
        if (CurrentMoveDirection != MovingDirection.Idle) Move(CurrentMoveDirection);

        // stara wersja solucji problemu z wyliczaniem pozycji z ułamkami
        //   else transform.position = NormalizePosition(transform.position);

        if (transform.position != new Vector3(endPosition.x,endPosition.y,0)) return;
        if (Input.GetKey(KeyCode.S))
        {
            ScanAndSetEndPosition(MovingDirection.Down);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            ScanAndSetEndPosition(MovingDirection.Up);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ScanAndSetEndPosition(MovingDirection.Left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ScanAndSetEndPosition(MovingDirection.Right);
        }
    }

    private void ScanAndSetEndPosition(MovingDirection direction)
    {
     //   Debug.Log("Scanning: " + direction);
     //   Debug.Log("Player position" + player.position.x + " " + player.position.y);
        endPosition = player.position;
        if (direction == MovingDirection.Down)
        {
            endPosition.y -= 1;
            //   scan = Physics2D.Raycast(transform.position, Vector2.down, 1);
            if (MapMoving.MoveTo(player.position, endPosition, player))
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Down", true);
                CurrentMoveDirection = MovingDirection.Down;
                //     vector3endposition += new Vector3(0, -0.5f, 0);                                        // w razie gdyby wyliczyło pozycję
                //     vector3endposition = new Vector3(vector3endposition.x, (int)vector3endposition.y, vector3endposition.z);   // z ułamkiem i zablokowało ludzika
            }
            //..... i tak źle to działa.....
            else
            {
                endPosition = player.position;
                animator.SetBool("Idle", true);
            }
        }
        else if (direction == MovingDirection.Up)
        {
            endPosition.y += 1;
            if (MapMoving.MoveTo(player.position, endPosition, player))
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Up", true);
                CurrentMoveDirection = MovingDirection.Up;
            }
            else
            {
                endPosition = player.position;
                animator.SetBool("Idle", true);
            }
        }
        else if (direction == MovingDirection.Left)
        {
            endPosition.x -= 1;
            if (MapMoving.MoveTo(player.position, endPosition, player))
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Left", true);
                CurrentMoveDirection = MovingDirection.Left;
            }
            else
            {
                endPosition = player.position;
                animator.SetBool("Idle", true);
            }
        }
        else if (direction == MovingDirection.Right)
        {
            endPosition.x += 1;
            if (MapMoving.MoveTo(player.position, endPosition, player))
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Right", true);
                CurrentMoveDirection = MovingDirection.Right;
            }
            else
            {
                endPosition = player.position;
                animator.SetBool("Idle", true);
            }
        }
     //   Debug.Log("End position" + endPosition.x + " " + endPosition.y);
    }

    private void Move(MovingDirection direction)
    {
        Vector3 startpos = transform.position;
        Vector3 endpos = new Vector3(endPosition.x, endPosition.y, 0);
    //    Debug.Log(startpos + "     " + endpos);
        transform.position = Vector3.MoveTowards(startpos, endpos, speed / 100);
        animator.SetBool("Idle", false);
        if (endpos != transform.position)
        {
            if (transform.position.x != endpos.x)
            {
                DistanceToTarget = endpos.x - transform.position.x;
            }
            if (transform.position.y != endpos.y)
            {
                DistanceToTarget = endpos.y - transform.position.y;
            }

        }
        else
        {
            if (!IsKeyPressed()) animator.SetBool("Idle", true);
            CurrentMoveDirection = MovingDirection.Idle;
          //  endpos = transform.position;
        }
    }
    private Vector3 NormalizePosition(Vector3 position)
    {
        position.x = (int)(position.x);
        position.y = (int)(position.y);

        return position;
    }
    private void SetAnimatorBoolsToFalse()
    {
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
        animator.SetBool("Down", false);
        animator.SetBool("Up", false);
    }
    private bool IsKeyPressed()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) return true;
        else return false;
    }

    private void ScanForLayers()
    {
        // layerScans[0] = 
    }
}
