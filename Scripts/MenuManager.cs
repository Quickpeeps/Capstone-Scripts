using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Canvas mainMenu;
    private Canvas scoreBoard;
    private string connectionString; 


    void Awake()
    {       
        connectionString = "URI=file:C:/Users/mikep/Cadence/Assets/ProjectCadenceDB.sqlite";       
        mainMenu = GameObject.Find("MainMenu").GetComponent<Canvas>();
        scoreBoard = GameObject.Find("ScoreBoard").GetComponent<Canvas>();
    }

    public void Start()
    {
        mainMenu.enabled = true;
        scoreBoard.enabled = false;
    }

    //Sends the player to the first level scene
    public void StartButton()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void ScoresButton()
    {
        mainMenu.enabled = false;
        scoreBoard.enabled = true; 
    }

    //Takes the player to the website for extras
    public void ExtrasButton()
    {
        Application.OpenURL("http://cadencescores.com/home");
    }

    public void SBBackButton()
    {
        mainMenu.enabled = true;
        scoreBoard.enabled = false;
    }

    //Quits the game when the player presses the exit button
    public void ExitButton()
    {
        Application.Quit();
    }

    private void GetScores()
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
