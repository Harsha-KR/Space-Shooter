using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    SpawnManager spawnManager;

    [SerializeField]
    UIManager uIManager;

    public bool isPlayerDead { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnPlayerDeath()
    {
        isPlayerDead = true;
        uIManager.GameOver();
    }

}
