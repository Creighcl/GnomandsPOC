using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orientleft : MonoBehaviour
{
    [SerializeField] float differenceX = 0;
    void Start()
    {
       /* float boundaryX = Camera.main.ViewportToWorldPoint(new Vector2(0,0)).x;
        Vector3 newPosition = transform.position;
        newPosition.x = boundaryX;
        transform.position = newPosition;
        differenceX = boundaryX;*/

        GameObject background = (GameObject) Instantiate(Resources.Load("Prefabs/Stages/PocStageBackground"), GameObject.Find("_Stage/Background")?.transform);
        Debug.Log(background.GetComponent<SpriteRenderer>().bounds.size.x);
        Debug.Log("MY GUESS IS " + ((background.GetComponent<SpriteRenderer>().bounds.size.x / 2) + differenceX).ToString("0.000"));
        Debug.Log("MY GUESS IS " + ((-background.GetComponent<SpriteRenderer>().bounds.size.x / 2) + differenceX).ToString("0.000"));
    }

    public float GetDifferenceX()
    {
        return differenceX;
    }

    void Update()
    {
        
    }
}
