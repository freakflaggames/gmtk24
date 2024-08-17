using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : MonoBehaviour
{
    public float bounceForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.Bounce(bounceForce);
        }
    }
}
