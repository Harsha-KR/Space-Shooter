using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;

    Player player;
    int points;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        points = Random.Range(10, 15);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <-5f)
        {
            Destroy(this.gameObject);
        }        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            if (player != null)
            {
                player.ScoreKeeper(points);
            }
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
