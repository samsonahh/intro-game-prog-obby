using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayPlatform : MonoBehaviour
{
    [SerializeField] private float fadeAwayDuration = 1f;

    private BoxCollider boxCollider;
    private Renderer meshRenderer;
    private bool coroutineStarted = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!coroutineStarted)
            {
                coroutineStarted = true;
                StartCoroutine(FadeAway());
            }
        }
    }

    IEnumerator FadeAway()
    {
        for (float t = 0; t < fadeAwayDuration; t += Time.deltaTime)
        {
            meshRenderer.material.color = Color.Lerp(Color.black, Color.clear, t / fadeAwayDuration);
            yield return null;
        }

        boxCollider.enabled = false;

        yield return new WaitForSeconds(fadeAwayDuration);

        for (float t = 0; t < fadeAwayDuration; t += Time.deltaTime)
        {
            meshRenderer.material.color = Color.Lerp(Color.clear, Color.black, t / fadeAwayDuration);
            yield return null;
        }

        boxCollider.enabled = true;
        coroutineStarted = false;
    }
}
