using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontallyLoopCamera : MonoBehaviour
{
    [SerializeField] Camera primaryCam;
    [SerializeField] Camera trailCam = null;
    float mapWidth = 1;

    private void Start()
    {
        TurretSceneManager.Instance.OnMapWidthChange += SetMapWidth;
    }

    private void OnDestroy()
    {
        if (TurretSceneManager.Instance != null)
        {
            TurretSceneManager.Instance.OnMapWidthChange -= SetMapWidth;
        }
    }

    private void SetMapWidth(float width)
    {
        mapWidth = width;
    }

    private Vector3 getLeftMapMarginPosition()
    {
        return new Vector3(-(mapWidth / 2), 0f, 0f);
    }

    private Vector3 getRightMapMarginPosition()
    {
        return new Vector3(mapWidth / 2, 0f, 0f);
    }

    private float getHalfScreenWidth()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        return width / 2;
    }

    void Update()
    {
        Vector3 newTrailCamPosition = trailCam.transform.localPosition;
        newTrailCamPosition.x = transform.position.x < 1 ? mapWidth : -mapWidth;
        trailCam.transform.localPosition = newTrailCamPosition;

        float rightClipViewportPosition = Camera.main.WorldToViewportPoint(getRightMapMarginPosition()).x;
        if (rightClipViewportPosition < 0) transform.position = new Vector3(-(mapWidth / 2)+getHalfScreenWidth(), transform.position.y, transform.position.z);

        float leftClipViewportPosition = Camera.main.WorldToViewportPoint(getLeftMapMarginPosition()).x;

        float screenResolutionOffset = 0;
        float rightmostCameraPositionX = mapWidth - (2 * Mathf.Abs(screenResolutionOffset));
        if (leftClipViewportPosition > 1) transform.position = new Vector3((mapWidth/2)- getHalfScreenWidth(), transform.position.y, transform.position.z);
    }
}
