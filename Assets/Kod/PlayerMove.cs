using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {
    public int playerSpeed = 10;
    private float moveX;
    private float moveY;
    private bool flipped;
    
    public float timer = 0;

    private bool isMoving = false;
    private bool isMovingRight = false;
    private bool isMovingleft = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private Animator animator;

    [SerializeField] private float distanceToMove;
    [SerializeField] private float moveSpeed;
    private bool moveToPoint = false;
    private Vector3 endPosition;

    // Use this for initialization
    void Start () {
        flipped = false;
        endPosition = transform.position;
        animator = transform.GetChild(1).gameObject.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        // Move();
        TileMove();
		
	}
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
        if (moveToPoint && timer<1)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
        }
    }

    void TileMove()
    {


        moveY = Input.GetAxisRaw("Vertical");
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.A)) //Left
        {
            isMoving = true;
            isMovingleft = true;
            //if (flipped)
            //{
            //    Flip();
            //    flipped = false;
            //}
            endPosition = new Vector3(endPosition.x - distanceToMove, endPosition.y, endPosition.z);
            moveToPoint = true;
        }
        if (Input.GetKeyDown(KeyCode.D)) //Right
        {
            isMoving = true;
            isMovingRight = true;
            //if (!flipped)
            //{
            //    Flip();
            //    flipped = true;
            //}
            endPosition = new Vector3(endPosition.x + distanceToMove, endPosition.y, endPosition.z);
            moveToPoint = true;
        }
        if (Input.GetKey(KeyCode.W)) //Up
        {
            isMoving = true;
            isMovingUp = true;
            endPosition = new Vector3(endPosition.x, endPosition.y + distanceToMove, endPosition.z);
            moveToPoint = true;
        }
        if (Input.GetKey(KeyCode.S)) //Down
        {
            isMoving = true;
            isMovingDown = true;
            endPosition = new Vector3(endPosition.x, endPosition.y - distanceToMove, endPosition.z);
            moveToPoint = true;
        }
    }
    

    void Flip()
    {
        gameObject.transform.Rotate(new Vector3(0, 180, 0));
    }
    

}
