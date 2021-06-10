using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    float angularSpeed = 20f;

    private void Update()
    {
        Rotate();
    }
    
    private void Rotate()
    {
        transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
    }
}

