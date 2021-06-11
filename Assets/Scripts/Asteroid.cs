using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float angularSpeed = 20f;
    [SerializeField]
    GameObject explosionVFX;
    SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        Rotate();
    }
    
    private void Rotate()
    {
        transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Instantiate(explosionVFX, this.transform.position, Quaternion.identity);
            spawnManager.StartSpawnning();
            Destroy(this.gameObject, 0.15f);
        }
    }

}

