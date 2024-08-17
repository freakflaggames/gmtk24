using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public PlayerGrowth player;

    public float zoomOutSpeed;

    private void Update()
    {
        float lensSize = virtualCamera.m_Lens.OrthographicSize;
        float targetSize = 10 + player.targetSize;
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(lensSize, targetSize, Time.deltaTime * zoomOutSpeed);
    }
}
