using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupRoutine());
    }
        
    IEnumerator PowerupRoutine()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(15f, 20f));
            spawnPosition = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject _powerUp = Instantiate(PowerUp[Random.Range(0,PowerUp.Length)], spawnPosition, Quaternion.identity);
            _powerUp.transform.parent = powerUpContainer.transform;
        }

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            spawnPosition = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject _newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            _newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

}
