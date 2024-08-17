using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostGain;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.boost += boostGain;
        }
    }
}
