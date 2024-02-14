using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTwo : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 45f*Time.deltaTime, 0);
    }
}
