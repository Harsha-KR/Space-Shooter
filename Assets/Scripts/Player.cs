using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    SpawnManager spawnManager;
    private float speed = 5f;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject trippleShot;
    Vector3 laserOffset;
    [SerializeField]
    private float fireRate;
    private float canFire = -1f;

    private int life = 3;

    private bool isTrippleShotActive;
    private bool isSpeedPowerUpActive;

    void Start()
    {
        this.transform.position = Vector3.zero;
        laserOffset = new Vector3(0, 0.9f, 0f);

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
        if(isSpeedPowerUpActive == true)
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);

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
        isSpeedPowerUpActive = true;
        StartCoroutine(SpeedPowerupCoroutine());
    }
    IEnumerator SpeedPowerupCoroutine()
    {
        yield return new WaitForSeconds(5f);
        isSpeedPowerUpActive = false;
    }

    public void Damage()
    {
        life--;

        if(life == 0)
        {
            Destroy(this.gameObject);
            spawnManager.OnPlayerDeath();
        }
    }       
}
