using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ImmuneCell : MonoBehaviour
{
    public float moveSpeed;

    GameObject player;

    GameObject latchCollider;

    Rigidbody2D rb;

    bool latched;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    private void FixedUpdate()
    {
        if (!latched)
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;
        }
        else
        {
            rb.simulated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !latched)
        {
            latched = true;
            transform.SetParent(collision.gameObject.transform);
        }
    }
}
