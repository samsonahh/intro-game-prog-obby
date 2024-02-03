using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Transform cameraPitchTransform;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private bool isMouseLocked;
    [SerializeField] private float anchorHeight;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float playerFollowSpeed;
    [SerializeField] private float rotationSmoothSpeed;
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity;

    private float yaw;
    private float pitch;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Cursor.lockState = isMouseLocked ? CursorLockMode.Locked : CursorLockMode.None;

        HandleCameraMouseMovement();
    }

    private void FixedUpdate()
    {
        HandleCameraFollow();
    }

    private void HandleCameraFollow()
    {
        Vector3 newAnchorPosition = new Vector3(player.transform.position.x, player.transform.position.y + anchorHeight, player.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newAnchorPosition, playerFollowSpeed * Time.fixedDeltaTime);

        mainCamera.transform.localPosition = new Vector3(0, 0, -Mathf.Abs(distanceFromPlayer));
    }

    private void HandleCameraMouseMovement()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        yaw += x * mouseSensitivity;
        pitch -= y * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yaw, 0), rotationSmoothSpeed * Time.deltaTime);
        cameraPitchTransform.localRotation = Quaternion.Slerp(cameraPitchTransform.localRotation, Quaternion.Euler(pitch, 0, 0), rotationSmoothSpeed * Time.deltaTime);
    }
}
