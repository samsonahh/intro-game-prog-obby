using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovementTwo : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0, z);

        transform.Translate(Time.deltaTime * moveDir);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(10*Vector3.up, ForceMode.Impulse);
        }
    }
}
