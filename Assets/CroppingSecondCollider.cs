using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CroppingSecondCollider : MonoBehaviour
{
    [SerializeField] ScreenClipRight scr;
    [SerializeField] orientleft ol;
    BoxCollider2D _myBoxCollider;

    void Start()
    {
        _myBoxCollider = GetComponent<BoxCollider2D>();    
    }

    void Update()
    {
        CropTrailCameraColliderToVisibleArea();

    }

    private void CropTrailCameraColliderToVisibleArea()
    {
        float mapLength = scr.getMapLength();
        float aspectPixelOffset = ol.GetDifferenceX();
        float x = transform.position.x;
        _myBoxCollider.enabled = false;

        bool isPartiallyShowingOnRight = x < mapLength && x > mapLength + (2 * aspectPixelOffset);
        if (isPartiallyShowingOnRight)
        {
            _myBoxCollider.enabled = true;
            float a = mapLength - x;
            float centerVertex = (a / 2) + aspectPixelOffset;
            _myBoxCollider.offset = new Vector2(centerVertex, 0f);
            _myBoxCollider.size = new Vector2(a, _myBoxCollider.size.y);
            return;
        }

        bool isPartiallyShowingOnLeft = x > (2 * aspectPixelOffset) && x < 0;
        if (isPartiallyShowingOnLeft)
        {
            _myBoxCollider.enabled = true;
            float length = Mathf.Abs((2 * aspectPixelOffset) - x);
            float centerVertex = (-1 * aspectPixelOffset) - (length / 2);
            _myBoxCollider.offset = new Vector2(centerVertex, 0f);
            _myBoxCollider.size = new Vector2(length, _myBoxCollider.size.y);
        }
    }
}
