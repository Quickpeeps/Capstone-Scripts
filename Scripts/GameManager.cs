using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

/*
 * Controls the scenes in the game
 * Passes information to the database
 * All good
 */
public class GameManager : MonoBehaviour
{
    //Declares instance
    public static GameManager instance; 

    //Database elements
    private string dbName;
    private int dbScore;
    private string connectionString;

    //Declare elements from other objects
    private PlayerControls playerControls;
    private GameOverUIManager gameOverUIM;
    private Canvas gameOverUI;
    private Canvas playerUI;

    void Awake()
    {
        DoNotDuplicate();

        //Set the connection string 
        connectionString = "URI=file:C:/Users/mikep/Cadence/Assets/ProjectCadenceDB.sqlite";

        //Defines the elements from other scripts
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        gameOverUI = GameObject.Find("GameOverUI").GetComponent<Canvas>();
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        gameOverUIM = GameObject.Find("GameOverUI").GetComponent<GameOverUIManager>();
    }


    void Start()
    {
        StartLevel();
    }

    //Makes sure this isn't duplicated at any point
    private void DoNotDuplicate()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel()
    {
        playerUI.enabled = true;
        gameOverUI.enabled = false;
    }

    //Called only when the player dies or the song has concluded
    public void GameOver()
    {
        playerUI.enabled = false;
        gameOverUI.enabled = true;
    }

    public void RestartSong()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Quits to the main menu scene when called 
    public void QuitSong()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    //Inserts the name and score to the database when called
    public void InsertScores()
    {
        dbScore = playerControls.currentScore;
        dbName = gameOverUIM.playerName;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Highscores(Playername, Playerscore) VALUES(\"{0}\",\"{1}\")", dbName, dbScore);                 
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    //Method to get the scores from the database
    public void GetScores()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Highscores";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetString(1));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    } 
   
}
