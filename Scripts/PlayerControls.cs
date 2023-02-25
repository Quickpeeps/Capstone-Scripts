using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Declare Values for Movement
    private Rigidbody2D rb2d;
    private float baseSpeed;
    float horizontal;
    float vertical;
    

    //Declare HP Values
    public float currentHP = 0;
    public float maxHP = 100;
    public int currentScore;
    
    //Declare Dashing and Blocking bools
    public bool isDashing = false;
    public bool isBlocking = false;

    private GameManager gameManager;

    void Start() 
    {
        currentHP = maxHP;
        rb2d = GetComponent<Rigidbody2D>();
        baseSpeed = 10;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        //Get the player's horizontal and vertical input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        UpdateHP();
        Dashing();
        Blocking();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * baseSpeed, vertical * baseSpeed);
    }

    //Updates player's currentHP
    public void UpdateHP()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }

    //Increases the player score when called 
    public void PlayerScored()
    {
        currentScore += 10;
    }

    //Increases player speed when space is pressed
    public void Dashing()
    {
        //if the player presses the dash button, increase speed and set isDashing to true
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
            baseSpeed = 20;            
        }
        //Once the player releases the dash button, set isDashing to false, bring speed back to the original value
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isDashing = false;
            baseSpeed = 10;
        }
    }

    //Needs work 
    public void Blocking()
    {
        //if the player presses Left shift, set isBlocking to true
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Player is Blocking");
            isBlocking = true;
            baseSpeed = 5;
        }
        //once the player lets go of Left Shift, set isBlocking to false
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Player is no longer Blocking");
            isBlocking = false;
            baseSpeed = 10;
        }
    }




}
