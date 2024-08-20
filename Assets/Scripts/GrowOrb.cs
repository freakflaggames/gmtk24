using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOrb : MonoBehaviour
{
    public float growAmount;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerGrowth>())
        {
            PlayerGrowth player = collision.gameObject.GetComponent<PlayerGrowth>();
            
            player.Grow(growAmount);

            Destroy(gameObject);
            
        }
    }
}
