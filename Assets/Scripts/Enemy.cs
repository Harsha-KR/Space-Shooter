using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip enemyExplosionAudio;
    [SerializeField]
    AudioClip enemyLaserAudio;
    Animator anim;
    [SerializeField]
    float speed = 4f;

    Player player;
    int points;

    [SerializeField]
    private GameObject enemyLaser;
    Vector3 enemyLaserOffset = new Vector3(0, -0.9f, 0f);

    float canFire = 1f;
    float fireRate;

    public bool isEnemyAlive;
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        if(audioSource == null)
        {
            Debug.Log("Audio source is null in " + this.gameObject.name);
        }
        points = Random.Range(10, 15);
        speed += player.speedModifier/1000;

        isEnemyAlive = true;
        fireRate = Random.Range(2f, 5f);
    }

    void Update()
    {
        Move();
        EnemyFire();
    }

    private void EnemyFire()
    {
        if(Time.time >= canFire && isEnemyAlive)
        {
            canFire = Time.time + fireRate;
            Instantiate(enemyLaser, this.transform.position + enemyLaserOffset, Quaternion.identity);
            audioSource.clip = enemyLaserAudio;
            audioSource.Play();
        }
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
            ExplosionSequence();
            isEnemyAlive = false;
            
        }else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            ExplosionSequence();
            isEnemyAlive = false;
        }
    }

    private void ExplosionSequence()
    {
        anim.SetTrigger("isTrigger");
        audioSource.clip = enemyExplosionAudio;
        audioSource.Play();
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 2.2f);
    }

    private void ReduceSpeed()
    {
        speed = 0f;
    }

    public void IncreaseSpeed(float _speed)
    {
        speed += _speed / 125;
    }
}