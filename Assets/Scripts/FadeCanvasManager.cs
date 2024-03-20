using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCanvasManager : MonoBehaviour
{
    public static FadeCanvasManager Instance;

    private Animator animator;
    private bool coroutineStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerFade()
    {
        if (!coroutineStarted)
        {
            coroutineStarted = true;

            StartCoroutine(TriggerFadeCoroutine());
        }
    }

    IEnumerator TriggerFadeCoroutine()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        animator.Play("FadeIn");

        coroutineStarted = false;
    }

    public void ReloadLevel()
    {
        if (!coroutineStarted)
        {
            coroutineStarted = true;

            StartCoroutine(ReloadLevelCoroutine());
        }
    }

    IEnumerator ReloadLevelCoroutine()
    {
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Obby");
    }
}
