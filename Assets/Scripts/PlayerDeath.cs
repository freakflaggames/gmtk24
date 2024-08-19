using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float deathTime;
    PlayerMovement movement;
    ProceduralTail tail;
    public GameObject deathParticles;
    public bool isDead;

    public GameObject lastCheckpoint;

    public delegate void PlayerDied();
    public static event PlayerDied playerDied;

    public delegate void PlayerRespawned();
    public static event PlayerRespawned playerRespawned;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        tail = FindAnyObjectByType<ProceduralTail>().gameObject.GetComponent<ProceduralTail>();
    }
    public void Die()
    {
        movement.canMove = false;
        StartCoroutine(WaitToDie());
    }
    private void Update()
    {
    }
    public void Respawn()
    {
        transform.position = lastCheckpoint.transform.position;

        isDead = false;
        playerRespawned?.Invoke();

        movement.canMove = true;
        movement.drag = 0;
        movement.boost = 0;

        tail.InitializeTail();

        gameObject.SetActive(true);
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(deathTime);

        isDead = true;
        playerDied?.Invoke();

        Instantiate(deathParticles, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }
}
