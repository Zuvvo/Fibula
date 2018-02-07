using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMoveTest : MonoBehaviour {
    RaycastHit2D hitup;
    RaycastHit2D hitright;
    RaycastHit2D hitdown;
    RaycastHit2D hitleft;
    Vector3 pos;
    private float speed = 2f;
    private Animator animator;
    private bool keyPressed = false;
    private BoxCollider2D boxCollider;

    public bool moveLeft = false;
    public bool moveRight = false;
    public bool moveDown = false;
    public bool moveUp = false;


    public bool move = false;
    void Start()
    {
        boxCollider = transform.GetComponent<BoxCollider2D>();
        animator = transform.GetChild(1).gameObject.GetComponent<Animator>();
        pos = transform.position; // Take the current position
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.J))
        {
            SetPositionOfPlayer(new Vector3(160.5f, 150f, 0));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Unstuck();
        }
    }
    void FixedUpdate()
    {
     //   animator.SetBool("Walking", false);
        //====RayCasts====//
        hitup = Physics2D.Raycast(transform.position, Vector2.up, 1);
        hitdown = Physics2D.Raycast(transform.position, Vector2.down, 1);
        hitright = Physics2D.Raycast(transform.position, Vector2.right, 1);
        hitleft = Physics2D.Raycast(transform.position, Vector2.left, 1);
        //==Inputs==//
        if (Input.GetKey(KeyCode.A) && transform.position == pos && hitleft.collider == null)
        {           //(-1,0)
            pos += Vector3.left ;// Add -1 to pos.x
            moveLeft = true;
        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos && hitright.collider == null)
        {           //(1,0)
            pos += Vector3.right ;// Add 1 to pos.x
            moveRight = true;
        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos && hitup.collider == null)
        {
        //    animator.SetBool("Up", true);
            pos += Vector3.up; // Add 1 to pos.y
            moveUp = true;
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos && hitdown.collider == null)
        {           //(0,-1)
        //    animator.SetBool("Down", true);
            pos += Vector3.down;// Add -1 to pos.y
            moveDown = true;
        }
        //The Current Position = Move To (the current position to the new position by the speed * Time.DeltaTime)

        if(Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
         //   Debug.Log("Key Pressed");
            keyPressed = true;
        }
        else
        {
            keyPressed = false;
        }
        if (moveUp) MoveUp();
        if (moveDown) MoveUp();
        if (moveLeft) MoveLeft();
        if (moveRight) MoveRight();
    }
    public void SetPositionOfPlayer(Vector3 position)
    {
        transform.position = position;
        pos = transform.position;
        move = false;
    }

    public void Unstuck()
    {
        float x = (int)transform.position.x + 0.5f;
        float y = (int)transform.position.y + 0.5f;
        transform.position = new Vector3(x, y, 0);
        pos = transform.position;

    }

    private void MoveUp()
    {
        Debug.Log("moving");
        //       animator.SetBool("Walking", true);
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
            boxCollider.offset = new Vector2(0, DistanceToTarget);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        if (pos == transform.position && !keyPressed)
        {
            //Debug.Log("moving stop");
            //animator.SetBool("Walking", false);
            //animator.SetBool("Up", false);
            //animator.SetBool("Down", false);
            //animator.SetBool("Left", false);
            //animator.SetBool("Right", false);


            pos = transform.position;
            boxCollider.offset = new Vector2(0, 0);
            moveUp = false;
        }
    }
    private void MoveDown()
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
            boxCollider.offset = new Vector2(0, DistanceToTarget);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        if (pos == transform.position && !keyPressed)
        {
            pos = transform.position;
            boxCollider.offset = new Vector2(0, 0);
            moveDown = false;
        }
    }
    private void MoveLeft()
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
            boxCollider.offset = new Vector2(DistanceToTarget, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        if (pos == transform.position && !keyPressed)
        {
            pos = transform.position;
            boxCollider.offset = new Vector2(0, 0);
            moveLeft = false;
        }
    }
    private void MoveRight()
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
            boxCollider.offset = new Vector2(DistanceToTarget, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        if (pos == transform.position && !keyPressed)
        {
            pos = transform.position;
            boxCollider.offset = new Vector2(0, 0);
            moveRight = false;
        }
    }
}
