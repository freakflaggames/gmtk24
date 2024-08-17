using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneWall : MonoBehaviour
{
    public float necessarySpeed;
    public float waitTime;
    public GameObject brokenWall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player.moveSpeed + player.boost >= necessarySpeed)
            {
                StartCoroutine(BreakWall());
            }
        }
    }

    IEnumerator BreakWall()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(brokenWall, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
