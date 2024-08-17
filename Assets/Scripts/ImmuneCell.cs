using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ImmuneCell : MonoBehaviour
{
    public float moveSpeed;
    public float drag;
    PlayerMovement player;
    Rigidbody2D rb;
    bool latched;
    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = FindAnyObjectByType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        PlayerDeath.playerDied += Die;
    }

    private void FixedUpdate()
    {
        if (!latched && canMove)
        {
            rb.simulated = true;
            Vector3 playerDirection = player.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;
        }
        else
        {
            rb.simulated = false;
        }
    }

    public void Die()
    {
        if (latched)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        PlayerDeath.playerDied -= Die;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !latched)
        {
            latched = true;
            transform.SetParent(collision.transform);
            player.drag -= drag;
        }
    }
}
