using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class QuadrantMovement : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Queue<TouchPointHistoricalEntry> touchPointHistory = new Queue<TouchPointHistoricalEntry>();

    [Header("Swipe Controls")]
    int maxFramesInTouchPointHistory = 20;
    [SerializeField] float swipeThreshold = 44f;
    [SerializeField] float trajectoryLookbackSeconds = 0.1f;
    [SerializeField] float destinationX = 0f;
    [SerializeField] float travelSpeed = 4f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleUserInput();
        Move();
    }

    private void Move()
    {
        if (transform.position.x != destinationX)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x + (Time.deltaTime * travelSpeed), -destinationX, destinationX);
            transform.position = newPosition;
        }
    }

    private void HandleUserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPointHistory.Clear();
            _rigidbody.velocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            CaptureFrameToHistory();
            PruneCaptureFrameHistory();

        }

        if (Input.GetMouseButtonUp(0))
        {
            var history = new List<TouchPointHistoricalEntry>(touchPointHistory);
            SwipeCamera(history);
        }
    }

    private void SwipeCamera(List<TouchPointHistoricalEntry> history)
    {
        var swipeMagnitude = GetTouchMagnitudeFromHistory();
        var goLeft = GetTouchVelocityFromHistory().x < 0f;
        if (swipeMagnitude >= swipeThreshold)
        {

            Vector3 newPosition = transform.position;
            newPosition.x += (20f * (goLeft ? -1 : 1));
            destinationX = newPosition.x;
            
        }
    }

    private void CaptureFrameToHistory()
    {
        var thisFrame = new TouchPointHistoricalEntry(Time.deltaTime, Input.mousePosition);
        touchPointHistory.Enqueue(thisFrame);
    }

    private void PruneCaptureFrameHistory()
    {
        if (touchPointHistory.Count > maxFramesInTouchPointHistory)
        {
            touchPointHistory.Dequeue();
        }
    }

    private Vector2 GetReleasedTouchDelta()
    {
        Vector2 lastTouchedPosition = touchPointHistory.Last().Position;
        Vector2 trailingTrajectoryBase = getSnapshotAtSecondsAgoFromHistory(trajectoryLookbackSeconds).Position;
        Vector2 delta = (trailingTrajectoryBase - lastTouchedPosition);
        return delta;
    }

    private float GetTouchMagnitudeFromHistory()
    {
        float magnitude = GetReleasedTouchDelta().magnitude;
        return magnitude;
    }

    private Vector2 GetTouchVelocityFromHistory()
    {
        Vector2 velocity = GetReleasedTouchDelta().normalized;
        return velocity;
    }
       
    private TouchPointHistoricalEntry getSnapshotAtSecondsAgoFromHistory(float seconds)
    {
        var thumbThrough = touchPointHistory.Reverse<TouchPointHistoricalEntry>();
        var traversedTime = 0f;
        TouchPointHistoricalEntry returnSnap = thumbThrough.First();
        foreach (TouchPointHistoricalEntry snap in thumbThrough)
        {
            returnSnap = snap;
            traversedTime += snap.TimeDelta;
            if (traversedTime >= seconds)
            {
                break;
            }
        }

        return returnSnap;
    }

    private class TouchPointHistoricalEntry
    {
        public TouchPointHistoricalEntry(float timeDelta, Vector2 position)
        {
            TimeDelta = timeDelta;
            Position = position;
        }

        public float TimeDelta { get; set; }
        public Vector2 Position { get; set; }
    }
}
