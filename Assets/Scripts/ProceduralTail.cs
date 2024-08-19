using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTail : MonoBehaviour
{
    public int length;
    public LineRenderer lr;
    public Vector3[] segmentPoses;
    Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float currentDist;
    public float smoothSpeed;

    public float tailWidth;
    public float tailTaper;

    public GameObject[] bodyParts;
    public GameObject endPrefab;
    public GameObject colliderPrefab;
     
    private void Start()
    {
        currentDist = targetDist;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
        bodyParts = new GameObject[length-1];

        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (i == bodyParts.Length-1)
            {
                bodyParts[i] = Instantiate(endPrefab);
            }
            else
            {
                bodyParts[i] = Instantiate(colliderPrefab);
            }

            bodyParts[i].transform.SetParent(transform);

            BodyRotation body = bodyParts[i].GetComponent<BodyRotation>();

            if (i > 0)
            {
                body.target = bodyParts[i - 1].transform;
            }
            else
            {
                body.target = transform;
            }
        }

        InitializeTail();
    }
    public void InitializeTail()
    {
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = segmentPoses[i - 1] - Vector3.up * targetDist;
            //bodyParts[i - 1].transform.position = segmentPoses[i];
        }
    }
    private void Update()
    {
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * currentDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            bodyParts[i - 1].transform.position = segmentPoses[i];
        }
    }
}
