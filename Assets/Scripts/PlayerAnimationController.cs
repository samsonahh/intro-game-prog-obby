using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerManager playerManager;
    private Animator animator;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        animator.SetFloat("PlayerSpeed", playerManager.currentPlayerSpeed);
    }
}
