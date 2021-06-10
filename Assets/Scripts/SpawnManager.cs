using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject asteroid;
    Vector3 spawnPosition;

    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] PowerUp;
    [SerializeField]
    private GameObject powerUpContainer;
    [SerializeField]
    StateManager stateManager;    

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupRoutine());
        StartCoroutine(AsteroidRoutine());
    }

    IEnumerator PowerupRoutine()
    {
        while (stateManager.isPlayerDead == false)
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));
            spawnPosition = new Vector3(Random.Range(-7f, 7f), 7f, 0f);
            GameObject _powerUp = Instantiate(PowerUp[Random.Range(0,PowerUp.Length)], spawnPosition, Quaternion.identity);
            _powerUp.transform.parent = powerUpContainer.transform;
        }

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (stateManager.isPlayerDead == false)
        {
            spawnPosition = new Vector3(Random.Range(-7f, 7f), 7f, 0f);
            GameObject _newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            _newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator AsteroidRoutine()
    {
        while(stateManager.isPlayerDead == false)
        {
            spawnPosition = new Vector3(Random.Range(-7f, 7f), 7f, 0f);
            GameObject _newAsteroid = Instantiate(asteroid, spawnPosition, Quaternion.identity);
            _newAsteroid.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(20f, 30f));
        }
    }
}
