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

    private bool dieCoroutineStarted = false;

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
        if (!dieCoroutineStarted)
        {
            dieCoroutineStarted = true;
            StartCoroutine(KillPlayerCoroutine());
        }
    }

    private IEnumerator KillPlayerCoroutine()
    {
        isDying = true;
        PlayerAnimationController.Instance.PlayAnimationSmoothly("Die", 0.1f);
        FadeCanvasManager.Instance.TriggerFade();

        yield return new WaitForSeconds(1f);
        GameManager.Instance.Respawn();
        isMoving = false;
        PlayerAnimationController.Instance.PlayAnimationSmoothly("Movement", 0.1f);
        yield return new WaitForSeconds(1f);

        isDying = false;
        dieCoroutineStarted = false;
    }
}
