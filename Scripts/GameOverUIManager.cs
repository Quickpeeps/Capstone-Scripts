using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controls the GameOverUI
 * Calls functions from the game manager to save scores and control scenes
 * All Good
 */

public class GameOverUIManager : MonoBehaviour
{
    private Text scoreAchieved;
    private PlayerControls playerControls;
    private GameManager gameManager;
    private Canvas gameOverUI;
    private Canvas saveScoreUI;
    private InputField playerInput;
    public string playerName;


    void Awake()
    {
        scoreAchieved = GameObject.Find("ScoreAchieved").GetComponent<Text>();
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameOverUI = GameObject.Find("GameOverUI").GetComponent<Canvas>();
        saveScoreUI = GameObject.Find("SaveScoreUI").GetComponent<Canvas>();
        playerInput = GameObject.Find("PlayerInput").GetComponent<InputField>();
    }

    void OnEnable()
    {
        scoreAchieved.text = ("You Scored: " + playerControls.currentScore);
    }

    public void RetryButton()
    {
        gameManager.RestartSong();
    }

    public void QuitButton()
    {
        gameManager.QuitSong();
    }

    public void SaveButton()
    {
        gameOverUI.enabled = false;
        saveScoreUI.enabled = true;
    }

    public void ConfirmButton()
    {
        if (playerInput.text == "")
        {
            playerName = "Anonymous Player";
        }
        else
        {
            playerName = playerInput.text;
        }
        gameManager.InsertScores();
        saveScoreUI.enabled = false;
        gameOverUI.enabled = true;
    }

    public void BackButton()
    {
        saveScoreUI.enabled = false;
        gameOverUI.enabled = true;
    }
}