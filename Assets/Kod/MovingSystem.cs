using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSystem : MonoBehaviour {

    public float speed = 2f;

    public enum MovingDirection { Left, Right, Up, Down, Idle };
    public MovingDirection CurrentMoveDirection;

    private RaycastHit2D scan;
    private Vector3 endPosition;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private float DistanceToTarget = 0;

    void Start () {
        animator = transform.GetChild(0).GetComponent<Animator>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        endPosition = transform.position;
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); // przenieść do game managment

        if (CurrentMoveDirection != MovingDirection.Idle) Move(CurrentMoveDirection);
        else transform.position = NormalizePosition(transform.position);

        if (transform.position != endPosition) return;
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
        Debug.Log("Scanning: " + direction);
        if (direction == MovingDirection.Down)
        {
            scan = Physics2D.Raycast(transform.position, Vector2.down, 1);
            if (scan.collider == null)
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Down", true);
                CurrentMoveDirection = MovingDirection.Down;
                endPosition += Vector3.down;
            }
        }
        else if (direction == MovingDirection.Up)
        {
            scan = Physics2D.Raycast(transform.position, Vector2.up, 1);
            if (scan.collider == null)
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Up", true);
                CurrentMoveDirection = MovingDirection.Up;
                endPosition += Vector3.up;
            }
        }
        else if (direction == MovingDirection.Left)
        {
            scan = Physics2D.Raycast(transform.position, Vector2.left, 1);
            if (scan.collider == null)
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Left", true);
                CurrentMoveDirection = MovingDirection.Left;
                endPosition += Vector3.left;
            }
        }
        else if (direction == MovingDirection.Right)
        {
            scan = Physics2D.Raycast(transform.position, Vector2.right, 1);
            if (scan.collider == null)
            {
                SetAnimatorBoolsToFalse();
                animator.SetBool("Right", true);
                CurrentMoveDirection = MovingDirection.Right;
                endPosition += Vector3.right;
            }
        }
    }

    private void Move(MovingDirection direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed / 100);
        animator.SetBool("Idle", false);
        if (endPosition != transform.position)
        {
            if (transform.position.x != endPosition.x)
            {
                DistanceToTarget = endPosition.x - transform.position.x;
            }
            if (transform.position.y != endPosition.y)
            {
                DistanceToTarget = endPosition.y - transform.position.y;
            }
            if (direction == MovingDirection.Down) boxCollider.offset = new Vector2(0, DistanceToTarget +0.05f);
            else if (direction == MovingDirection.Up) boxCollider.offset = new Vector2(0, DistanceToTarget-0.05f);
            else if (direction == MovingDirection.Left) boxCollider.offset = new Vector2(DistanceToTarget+0.05f, 0);
            else if (direction == MovingDirection.Right) boxCollider.offset = new Vector2(DistanceToTarget-0.05f, 0);

        }
        else
        {
            if (!IsKeyPressed()) animator.SetBool("Idle", true);
            CurrentMoveDirection = MovingDirection.Idle;
            endPosition = transform.position;
            boxCollider.offset = new Vector2(0, 0);
        }
    }
    private Vector3 NormalizePosition(Vector3 position)
    {
        position.x = (int)position.x + 0.5f;
        position.y = (int)position.y + 0.5f;

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
