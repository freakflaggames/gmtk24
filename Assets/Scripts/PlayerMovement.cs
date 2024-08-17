using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public Vector2 velocity;
    Vector2 moveInput;
    Quaternion rotation;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //align rotation to move input
        if (moveInput.magnitude > 0)
        {
            Vector2 direction = moveInput.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        transform.rotation = rotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
        velocity = rb.velocity;
    }
}
