using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    float speed = 4f;

    Player player;
    int points;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if(anim == null)
        {
            Debug.Log("Animator is null on: " + this.gameObject.name);
        }
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.Log("Player is null in" + this.gameObject.name);
        }
        points = Random.Range(10, 15);
        speed += player.speedModifier/1000;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5f)
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
            ExplosionAnim();
                     
            
        }else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            ExplosionAnim();
        }
    }

    private void ExplosionAnim()
    {
        anim.SetTrigger("isTrigger");
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 2.2f);
    }

    private void ReduceSpeed()
    {
        speed = 0f;
    }

    public void IncreaseSpeed(float _speed)
    {
        speed += _speed / 1000;
    }
}