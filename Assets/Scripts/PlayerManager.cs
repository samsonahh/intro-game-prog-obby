using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isMoving;
    public bool isGrounded;
    public bool isJumping;
    public bool isFalling;
    public float currentPlayerSpeed;

    [Header("Configurable")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;

    private void Start()
    {
     
    }

    void Update()
    {
        HandleSpeedModifiers();
    }

    private void HandleSpeedModifiers()
    {
        if (!isMoving)
        {
            currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, 0, 10f * Time.deltaTime);
            return;
        }

        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, sprintSpeed, 5f * Time.deltaTime);
            }
            else
            {
                currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, walkSpeed, 10f * Time.deltaTime);
            }
        }
    }
}
