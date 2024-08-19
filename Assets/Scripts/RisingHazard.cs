using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingHazard : MonoBehaviour
{
    public Vector3 startPosition;
    public float riseSpeed;
    private void Awake()
    {
        startPosition = transform.position;
    }
    private void OnEnable()
    {
        PlayerDeath.playerRespawned += Restart;
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * riseSpeed);
    }

    void Restart()
    {
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerDeath>())
        {
            PlayerDeath player = collision.gameObject.GetComponent<PlayerDeath>();
            if (!player.isDead)
            {
                player.Die();
            }
        }
    }
    private void OnDisable()
    {
        PlayerDeath.playerRespawned -= Restart;
    }
}
