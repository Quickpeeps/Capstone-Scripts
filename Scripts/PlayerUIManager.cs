using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls the players in game UI
 * Updates the players HP bar and score display every frame
 * All good 
*/

public class PlayerUIManager : MonoBehaviour
{
    //Declare player script
    private PlayerControls playerControls;

    //Declare UI elements 
    private Image hpBar;
    private Text scoreText;

    void Awake()
    {
        //Find the components declared earlier
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        hpBar = GameObject.Find("HPBar").GetComponent<Image>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    void Update()
    {
        DisplayHP();
        DisplayScore();
    }

    void DisplayHP()
    {
        hpBar.fillAmount = Mathf.Clamp(playerControls.currentHP / 100, 0, 1f);
    }

    void DisplayScore()
    {
        scoreText.text = ("Score: " + playerControls.currentScore);
    }
}
