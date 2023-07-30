using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    StateManager stateManager;
    [SerializeField]
    SpawnManager spawnManager;
    [SerializeField]
    private float speed;
    private float speedBoostMultiplier = 2f;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject trippleShot;
    [SerializeField]
    private GameObject explosion;

    Vector3 laserOffset;
    [SerializeField]
    private float fireRate;
    private float canFire = -1f;

    #region Player Childern
    [SerializeField]
    GameObject shieldEffect;
    [SerializeField]
    GameObject damageLow;
    [SerializeField]
    GameObject damageHigh; 
    #endregion

    [SerializeField]
    private int life = 3;

    private AudioSource audioSource;

    private bool isTrippleShotActive;
    public bool isShieldActive;

    [SerializeField]
    bool isRightPlayer;

    public float speedModifier;

    UIManager uIManager;
    [SerializeField]
    GameObject pauseMenu;

    Animator playerAnimator;

    float horizontalInput;
    float verticalInput;

    void Start()
    {
        if (stateManager != null )
        {
            if(stateManager.isCoOp == false)
            {
                this.transform.position = Vector3.zero;
            }
            if(stateManager.isCoOp == true && isRightPlayer == true)
            {
                this.transform.position = new Vector3(5f, -1.5f, 0);
            }
            if (stateManager.isCoOp == true && isRightPlayer == false)
            {
                this.transform.position = new Vector3(-5f, -1.5f, 0);
            }

        }
        
        laserOffset = new Vector3(0, 0.9f, 0f);
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
        if(uIManager == null)
        {
            Debug.LogError("UI Manager is null, in player script");
        }

        if(spawnManager ==null)
        {
            Debug.LogError("SpawnManager is null, asign the Game object in inspector");
        }

        if (audioSource == null)
        {
            Debug.LogError("Audio Source is null on player");
        }

        playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && isRightPlayer == false)
        {
            FireLaser();
        }
        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > canFire && isRightPlayer == true)
        {
            FireLaser();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeSelf == true) 
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }
    }
    private void FireLaser()
    {
        canFire = Time.time + fireRate;
        if (isTrippleShotActive == true)
        {
            Instantiate(trippleShot, this.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laser, this.transform.position + laserOffset, Quaternion.identity);
        }
        audioSource.Play();
    }

    public void TrippleShotActive()
    {
        isTrippleShotActive = true;
        StartCoroutine(TrippleShotCooldownRoutine());

    }
    IEnumerator TrippleShotCooldownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTrippleShotActive = false;
    }

    private void CalculateMovement()
    {
        if(isRightPlayer == false)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }

        if(isRightPlayer == true)
        {
            horizontalInput = Input.GetAxis("Player2 Horizontal");
            verticalInput = Input.GetAxis("Player2 Vertical");
        }
        

        playerAnimator.SetFloat("Blend", horizontalInput);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        speedModifier = stateManager.Score;

        transform.Translate(direction * (speed + speedModifier/75) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2f, 0f), 0f);
        
        if (transform.position.x > 9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0f);
        }
        else if (transform.position.x < -9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0f);
        }
    }

    public void SpeedPowerupActive()
    {
        StartCoroutine(SpeedPowerupCoroutine());
    }
    IEnumerator SpeedPowerupCoroutine()
    {
        speed *= speedBoostMultiplier;
        yield return new WaitForSeconds(5f);
        speed /= speedBoostMultiplier;
    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            shieldEffect.gameObject.SetActive(false);
            return;
        }

        life--;

        uIManager.UpdateLives(life,isRightPlayer);
        FireDamage();

        if(life == 0)
        {
            stateManager.OnPlayerDeath();
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 0.15f); ; 
            
        }
    } 
    
    public void ShieldActive()
    {
        isShieldActive = true;
        shieldEffect.gameObject.SetActive(true);
        StartCoroutine(ActivateShieldRoutine());
    }
    
    IEnumerator ActivateShieldRoutine()
    {        
        yield return new WaitForSeconds(5f);
        isShieldActive = false;
        shieldEffect.gameObject.SetActive(false);
    }

    public void FireDamage()
    {
        if(life==2)
        {
            damageLow.SetActive(true);
        }else if(life ==1)
        {
            damageHigh.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyLaser")
        {
            Destroy(collision.gameObject);
            GameObject _laserExplosion = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            _laserExplosion.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Damage();
        }
    }
}
