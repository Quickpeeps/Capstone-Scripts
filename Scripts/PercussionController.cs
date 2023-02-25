using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionController : MonoBehaviour
{

    //Get the scriptes from the player and game manager
    private GameManager gameManager;
    private PlayerControls playerControls;


    //Declare the percussion damage
    private int percussionDamage = 20; 

    // Start is called before the first frame update
    void Start()
    {
        //Find the objects and get the script components 
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the game object tag matches the player check the following
        if (collision.CompareTag("Player"))
        {
            //if the player is not dashing, they will take 10 damage, the projectile will also be destroyed
            if (playerControls.isBlocking == false)
            {
                Debug.Log("Players takes damage from percussion");
                DamageCheck();
                Destroy(gameObject);
            }
            //if the player is dashing the game object will be destroyed and 10 points will be added to their score in the Game Manager
            if (playerControls.isBlocking == true)
            {
                Debug.Log("Players score increases");
                playerControls.PlayerScored();
                Destroy(gameObject);
            }

        }
        if (collision.CompareTag("Border"))
        {

        }

    }

    void DamageCheck()
    {
        playerControls.currentHP = playerControls.currentHP - percussionDamage;
        playerControls.UpdateHP();
    }
}
