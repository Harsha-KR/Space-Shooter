using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initial = new Vector3(-3.84f, 1.05f, 0);
        Vector3 final = new Vector3(3.7f, 1.05f, 0);
        Vector3 direction = final - initial;
        Debug.Log(direction);
        Debug.Log("Normalized:" + direction.normalized);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
