using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private float rotationSpeed;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
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

        if (movementDirection.magnitude > 0.05f)
        {
            playerManager.isMoving = true;

            float targetAngle = Camera.main.transform.eulerAngles.y + Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
            Vector3 targetDirection = targetRotation * Vector3.forward;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            transform.Translate(playerManager.currentPlayerSpeed * Time.fixedDeltaTime * targetDirection, Space.World);
        }
        else
        {
            playerManager.isMoving = false;
        }
    }
}
