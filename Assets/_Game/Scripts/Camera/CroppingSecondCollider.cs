using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CroppingSecondCollider : MonoBehaviour
{
    BoxCollider2D _myBoxCollider;
    float _mapWidth = 1;

    private void Start()
    {
        _myBoxCollider = GetComponent<BoxCollider2D>();
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
        _mapWidth = width;
    }

    void Update()
    {
        CropTrailCameraColliderToVisibleArea();
    }

    private float getScreenWidth()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        return width;
    }

    private void CropTrailCameraColliderToVisibleArea()
    {
        float x = transform.position.x;
        _myBoxCollider.enabled = false;

        float rightSide_leftBoundary = (_mapWidth / 2f) - (getScreenWidth() / 2);
        float rightSide_rightBoundary = (_mapWidth / 2f) + (getScreenWidth() / 2);

        bool isPartiallyShowingOnRight = x > rightSide_leftBoundary && x < rightSide_rightBoundary;
        if (isPartiallyShowingOnRight)
        {
            _myBoxCollider.enabled = true;
            float visibleWidthFromLeft = rightSide_rightBoundary - x;
            float centerVertex = -(getScreenWidth() - visibleWidthFromLeft) / 2;
            _myBoxCollider.offset = new Vector2(centerVertex, 0f);
            _myBoxCollider.size = new Vector2(visibleWidthFromLeft, _myBoxCollider.size.y);
            return;
        }

        float leftSide_leftBoundary = -(_mapWidth / 2f) - (getScreenWidth() / 2);
        float leftSide_rightBoundary = -(_mapWidth / 2f) + (getScreenWidth() / 2);

        bool isPartiallyShowingOnLeft = x > leftSide_leftBoundary && x < leftSide_rightBoundary;
        if (isPartiallyShowingOnLeft)
        {
            _myBoxCollider.enabled = true;
            float visibleWidthFromRight = -(leftSide_leftBoundary - x);
            float centerVertex = (getScreenWidth() - visibleWidthFromRight) / 2;
            
            _myBoxCollider.offset = new Vector2(centerVertex, 0f);
            _myBoxCollider.size = new Vector2(visibleWidthFromRight, _myBoxCollider.size.y);
        }
    }
}
