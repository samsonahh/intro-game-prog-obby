using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        HandlePlayerJump();
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

    private void CheckGrounded()
    {
        bool middle = Physics.Raycast(transform.position + 0.025f * Vector3.up, Vector3.down, 0.05f);

        bool lt = Physics.Raycast(transform.position + 0.025f * Vector3.up + capsuleCollider.radius * Vector3.left, Vector3.down, 0.05f);
        bool rt = Physics.Raycast(transform.position + 0.025f * Vector3.up + capsuleCollider.radius * Vector3.right, Vector3.down, 0.05f);
        bool fd = Physics.Raycast(transform.position + 0.025f * Vector3.up + capsuleCollider.radius * Vector3.forward, Vector3.down, 0.05f);
        bool bk = Physics.Raycast(transform.position + 0.025f * Vector3.up + capsuleCollider.radius * Vector3.back, Vector3.down, 0.05f);

        playerManager.isGrounded = middle || lt || rt || fd || bk;
    }

    private void HandlePlayerJump()
    {
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && playerManager.isGrounded)
        {
            rigidBody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            playerManager.isGrounded = false;
            playerManager.isJumping = true;
        }

        if (rigidBody.velocity.y < 0)
        {
            playerManager.isFalling = true;
        }

        if (playerManager.isGrounded)
        {
            playerManager.isJumping = false;
            playerManager.isFalling = false;
        }
    }
}
