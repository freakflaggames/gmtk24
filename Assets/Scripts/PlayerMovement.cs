using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float topSpeed;
    public float currentSpeed;

    public float boost;
    public float drag;

    public float stunTime;

    public Vector2 velocity;
    Vector2 moveInput;
    Quaternion rotation;

    Rigidbody2D rb;
    PlayerDeath death;

    public bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        death = GetComponent<PlayerDeath>();
    }
    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        currentSpeed = moveSpeed + boost + drag;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, topSpeed);

        if (currentSpeed <= 0)
        {
            death.Die();
        }

        //align rotation to move input
        if (moveInput.magnitude > 0 && currentSpeed > 0)
        {
            Vector2 direction = moveInput.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Bounce(float bounceForce)
    {
        Vector3 vel = -(Vector3)velocity + transform.right;

        canMove = false;

        rb.AddForce(vel.normalized * bounceForce);

        StartCoroutine(WaitToMove());
    }
    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(stunTime);
        canMove = true;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = moveInput * currentSpeed;
        }

        velocity = rb.velocity;
    }
}
