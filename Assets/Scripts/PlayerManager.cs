using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isMoving;
    public float currentPlayerSpeed;

    [Header("Configurable")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, sprintSpeed, 10f * Time.deltaTime);
        }
        else
        {
            currentPlayerSpeed = Mathf.Lerp(currentPlayerSpeed, walkSpeed, 10f * Time.deltaTime);
        }
    }
}
