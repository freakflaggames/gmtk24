using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    public float targetSize = 1;
    float startSize = 1;

    public float growTime;
    float growTimer;

    public ProceduralTail tail;

    private void FixedUpdate()
    {
        if (growTimer > 0)
        {
            growTimer -= Time.deltaTime;
        }

        float growAmount = Mathf.Lerp(targetSize, startSize, growTimer / growTime);

        transform.localScale = Vector3.one * growAmount;

        tail.lr.startWidth = growAmount;
        tail.lr.endWidth = growAmount;

        tail.currentDist = growAmount * tail.targetDist;
    }

    public void Grow(float growAmount)
    {
        startSize = transform.localScale.x;
        targetSize = startSize + growAmount;
        growTimer = growTime;
    }
}
