using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.5f;
    [SerializeField]
    private GameObject laser;
    Vector3 laserOffset;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;

    void Start()
    {
        this.transform.position = Vector3.zero;
        laserOffset = new Vector3(0, 0.5f, 0f);
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
        Instantiate(laser, this.transform.position + laserOffset, Quaternion.identity);
        canFire = Time.time + fireRate;
    }

    private void CalculateMovement()
    {
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
}
