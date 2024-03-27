using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePlatform : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isReverse;
    [SerializeField] private float startAngle;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        startAngle += isReverse ? Time.deltaTime * speed : Time.deltaTime * -speed;
        startAngle = startAngle % (360);

        transform.position = new Vector3(startPos.x + radius * Mathf.Sin(startAngle * Mathf.Deg2Rad), 0, startPos.z + radius * Mathf.Cos(startAngle * Mathf.Deg2Rad));
    }

    private void OnValidate()
    {
        startPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPos, radius);
        if (isReverse)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(radius * Mathf.Cos(startAngle * Mathf.Deg2Rad), 0, -radius * Mathf.Sin(startAngle * Mathf.Deg2Rad)));
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-radius * Mathf.Cos(startAngle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(startAngle * Mathf.Deg2Rad)));
        }
        Gizmos.DrawWireCube(new Vector3(startPos.x + radius * Mathf.Sin(startAngle * Mathf.Deg2Rad), 0, startPos.z + radius * Mathf.Cos(startAngle * Mathf.Deg2Rad)), transform.localScale);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player)) { }
        {
            player.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player)) { }
        {
            player.transform.SetParent(null);
            if (isReverse)
            {
                player.RigidBody.AddForce(new Vector3(radius * Mathf.Cos(startAngle * Mathf.Deg2Rad), 0, -radius * Mathf.Sin(startAngle * Mathf.Deg2Rad)), ForceMode.VelocityChange);
            }
            else
            {
                player.RigidBody.AddForce(new Vector3(-radius * Mathf.Cos(startAngle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(startAngle * Mathf.Deg2Rad)), ForceMode.VelocityChange);
            }
        
        }
    }
}
