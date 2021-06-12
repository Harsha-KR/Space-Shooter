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
    Vector3 laserOffset;
    [SerializeField]
    private float fireRate;
    private float canFire = -1f;

    [SerializeField]
    GameObject shieldEffect;

    [SerializeField]
    private int life = 3;

    private bool isTrippleShotActive;
    public bool isShieldActive;

    private int score = 0000;

    public float speedModifier;

    UIManager uIManager;

    void Start()
    {
        this.transform.position = Vector3.zero;
        laserOffset = new Vector3(0, 0.9f, 0f);
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uIManager == null)
        {
            Debug.LogError("UI Manager is null, in player script");
        }

        if(spawnManager ==null)
        {
            Debug.LogError("SpawnManager is null, asign the Game object in inspector");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
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
    }

    public void TrippelShotActive()
    {
        isTrippleShotActive = true;
        StartCoroutine(TrippelShotCooldownRoutine());

    }
    IEnumerator TrippelShotCooldownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTrippleShotActive = false;
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * (speed + speedModifier/600) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3f, 0f), 0f);
        
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

        uIManager.UpdateLives(life);

        if(life == 0)
        {            
            stateManager.OnPlayerDeath();
            Destroy(this.gameObject);            
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

    public void ScoreKeeper(int points)
    {
        score += points;
        speedModifier = (float)score;
        uIManager.UpdateScore(score);
    }
}
