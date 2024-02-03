using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    [SerializeField] private float rotationSpeed;

    void Start()
    {
        
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(x, 0, z).normalized;

        if(movementDirection.magnitude > 0.05f)
        {
            float targetAngle = Camera.main.transform.eulerAngles.y + Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
            Vector3 targetDirection = targetRotation * Vector3.forward;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            transform.Translate(playerSpeed * Time.fixedDeltaTime * targetDirection, Space.World);
        }
    }
}
