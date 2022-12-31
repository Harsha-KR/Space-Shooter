using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    UIManager uIManager;
    private int score = 0000;

    public bool isCoOp = false;

    public int Score
    {
        get; private set;
    }
    public bool isPlayerDead { get; private set; }

    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int restartSceneID = SceneManager.GetActiveScene().buildIndex;
            RestartGame(restartSceneID);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void RestartGame(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void OnPlayerDeath()
    {
        isPlayerDead = true;
        if (uIManager == null)
        {
            uIManager = FindObjectOfType<UIManager>();
        }
        uIManager.GameOver();
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene(1);
    }
    
    public void CoOp()
    {
        SceneManager.LoadScene(2);
    }

    public void ScoreKeeper(int points)
    {
        score += points;
        uIManager.UpdateScore(score);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        uIManager.PauseMenuUIEnable();
                
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        uIManager.PauseMenuUIDisable();
    }

}
