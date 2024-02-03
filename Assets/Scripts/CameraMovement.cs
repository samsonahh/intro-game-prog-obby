using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Movement player;
    [SerializeField] private Transform cameraPitchTransform;

    [SerializeField] private bool isMouseLocked;
    [SerializeField] private float playerFollowSpeed;
    [SerializeField] private float rotationSmoothSpeed;

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity;

    private float yaw;
    private float pitch;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    private void Update()
    {
        Cursor.lockState = isMouseLocked ? CursorLockMode.Locked : CursorLockMode.None;

        HandleCameraMouseMovement();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, playerFollowSpeed * Time.fixedDeltaTime);
    }

    private void HandleCameraMouseMovement()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        yaw += x * mouseSensitivity;
        pitch -= y * mouseSensitivity;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yaw, 0), rotationSmoothSpeed * Time.deltaTime);
        cameraPitchTransform.localRotation = Quaternion.Slerp(cameraPitchTransform.localRotation, Quaternion.Euler(pitch, 0, 0), rotationSmoothSpeed * Time.deltaTime);
    }
}
