using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    Vector3 spawnPosition;

    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] PowerUp;
    [SerializeField]
    private GameObject powerUpContainer;

    public bool isPlayerDead { get; private set; }

    void Start()
    {
        isPlayerDead = false;
        Debug.Log("isPlayerDead" + isPlayerDead);
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupRoutine());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator PowerupRoutine()
    {
        while (isPlayerDead == false)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            spawnPosition = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject _powerUp = Instantiate(PowerUp[Random.Range(0,PowerUp.Length)], spawnPosition, Quaternion.identity);
            _powerUp.transform.parent = powerUpContainer.transform;
        }

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (isPlayerDead == false)
        {
            spawnPosition = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject _newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            _newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void OnPlayerDeath()
    {
        isPlayerDead = true;
        Debug.Log("isPlayerDead" + isPlayerDead);
    }

}
