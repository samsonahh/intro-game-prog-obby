using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTwo : MonoBehaviour
{
    float mySpeed;

    // Start is called before the first frame update
    void Start()
    {
        mySpeed = 5f;
        Debug.Log(mySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime);
    }
}
