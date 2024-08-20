using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimpleHazard : MonoBehaviour
{
    PlayerMovement player;
    public float minDistance;
    public ImmuneCell[] cells;
    public GameObject poppedPimple;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(player.transform.position, transform.position);
        if (currentDistance < minDistance)
        {
            Pop();
        }
    }

    void Pop()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].transform.SetParent(null);
            cells[i].canMove = true;
        }
        Instantiate(poppedPimple, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
