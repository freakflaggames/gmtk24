using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerDeath death;

    private void Awake()
    {
        death = FindAnyObjectByType<PlayerDeath>().gameObject.GetComponent<PlayerDeath>();
    }
    private void Update()
    {
        if (death.isDead && Input.GetMouseButtonDown(0))
        {
            death.Respawn();
        }
    }
}
