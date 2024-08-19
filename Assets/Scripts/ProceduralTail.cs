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
    public GameObject colliderPrefab;
     
    private void Start()
    {
        lr.positionCount = length;
        currentDist = targetDist;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
        bodyParts = new GameObject[length];

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i] = Instantiate(colliderPrefab);
            bodyParts[i].transform.SetParent(transform);
            print((float)(i / bodyParts.Length));
            bodyParts[i].transform.localScale = Vector3.one - Vector3.up * (float)(i / bodyParts.Length);

            BodyRotation body = bodyParts[i].GetComponent<BodyRotation>();

            if (i > 1)
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
            bodyParts[i - 1].transform.position = segmentPoses[i];
        }
        lr.SetPositions(segmentPoses);
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

        lr.SetPositions(segmentPoses);
    }
}
