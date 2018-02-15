using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocal : MonoBehaviour {
    public enum MovingDirection { Left, Right, Up, Down, Idle };
    public MovingDirection CurrentMoveDirection;

    private float speed;
    private Animator animator;
    private float DistanceToTarget = 0;
    FibulaPlayer player = new FibulaPlayer();

    private FibulaPosition endPosition;


    void Start()
    {
        speed = player.speed;
        player.position.x = (int)transform.position.x;
        player.position.y = (int)transform.position.y;
        endPosition = player.position;
        animator = transform.GetChild(0).GetComponent<Animator>();
        MapSystem.Map1Server[player.position.x, player.position.y].isPlayerOnTile = true;
        MapSystem.Map1Server[player.position.x, player.position.y].isBlocked = true;
    }

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Z)) transform.position = NormalizePosition(transform.position); // w razie zablokowania xd
        if (CurrentMoveDirection != MovingDirection.Idle) Move(CurrentMoveDirection);

        if (transform.position != new Vector3(endPosition.x, endPosition.y, 0)) return;
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
        endPosition = player.position;
        if (direction == MovingDirection.Down)
        {
            endPosition.y -= 1;
            if (MapMoving.MoveTo(player.position, endPosition, player))
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Down", true);
                CurrentMoveDirection = MovingDirection.Down;
            }
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
    }

    private void Move(MovingDirection direction)
    {
        Vector3 startpos = transform.position;
        Vector3 endpos = new Vector3(endPosition.x, endPosition.y, 0);
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
}
