using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MelodyController : MonoBehaviour
{
    //Declare the scripts from the other object
    private GameManager gameManager;
    private PlayerControls playerControls;

    //Declare the melodyDamage, set it to 10 
    private int melodyDamage = 10;

    void Awake()
    {
        //Find the objects and get the script components 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
    }

    //Detects collision, checks tags for effect 
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player")) 
        {
            //if the player is not dashing, they will take 10 damage, the projectile will also be destroyed
            if (playerControls.isDashing == false)
            { 
                DamageCheck();
                Destroy(gameObject);
            }
            //if the player is dashing the game object will be destroyed and 10 points will be added to their score in the Game Manager
            if (playerControls.isDashing == true)
            {
                playerControls.PlayerScored();
                Destroy(gameObject);
            }         
        }
    }

    //Damage the player when called
    void DamageCheck()
    {
            playerControls.currentHP = playerControls.currentHP - melodyDamage;
            playerControls.UpdateHP();       
    }

    //Pushes the projectile from one end of the screen to the other
    //IDEA set empty objects opposite the spawners, set them as the target, make these projectiles approach them
    void Movement()
    {

    }

}
