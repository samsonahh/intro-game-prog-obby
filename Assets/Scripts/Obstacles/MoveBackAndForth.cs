using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float timeItTakesToGetToEnd = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private Rigidbody rigidBody;

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + distance * direction.normalized;

        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        StartCoroutine(Behaviour());
    }

    IEnumerator Behaviour()
    {
        while (true)
        {
            for(float t = 0f; t < timeItTakesToGetToEnd; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, t / timeItTakesToGetToEnd);
                yield return null;
            }

            for (float t = 0f; t < timeItTakesToGetToEnd; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(endPos, startPos, t / timeItTakesToGetToEnd);
                yield return null;
            }
        }
    }
}
