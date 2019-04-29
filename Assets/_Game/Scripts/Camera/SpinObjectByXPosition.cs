using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectByXPosition : MonoBehaviour
{
    [SerializeField] GameObject spinningRoot = null;
    [SerializeField] float _mapWidth = 1;

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

    void SetMapWidth(float width)
    {
        _mapWidth = width;
    }

    void Update()
    {
        float distance = transform.position.x;
        float rotation = (distance / _mapWidth) * 360;
        spinningRoot.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));   
    }
}
