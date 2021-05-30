using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;
    Vector3 spawnPosition;   

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        spawnPosition = new Vector3(Random.Range(-8f, 8f), 7f, 0f);


        if (transform.position.y <-5f)
        {
            transform.position = spawnPosition;
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Laser")
        { 
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
