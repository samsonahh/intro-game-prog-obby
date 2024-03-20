using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public bool isMoving;
    public bool isGrounded;
    public bool isJumping;
    public bool isFalling;
    public bool isDying;
    public float currentPlayerSpeed;

    [Header("Configurable")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
     
    }

    void Update()
    {
        HandleSpeedModifiers();
        HandleOutOfBoundsDeath();
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

    private void HandleOutOfBoundsDeath()
    {
        if(transform.position.y < -20f)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCoroutine());
    }

    private IEnumerator KillPlayerCoroutine()
    {
        isDying = true;
        PlayerAnimationController.Instance.PlayDieAnimation();
        FadeCanvasManager.Instance.TriggerFade();

        yield return new WaitForSeconds(1.5f);

        GameManager.Instance.Respawn();
        isDying = false;
    }
}
