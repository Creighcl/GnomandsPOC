using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] float minX = 0f;
    [SerializeField] float maxX = 118f;
    [SerializeField] List<GameObject> mobPrefabs = new List<GameObject>();


    void Start()
    {
        StartCoroutine(SpawnLoop());        
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            float randomXPosition = Random.Range(minX, maxX);
            int randomMobIndex = Random.Range(0, mobPrefabs.Count - 1);
            GameObject randomMob = mobPrefabs[randomMobIndex];
            var newMob = Instantiate(randomMob, transform);
            newMob.transform.localPosition = new Vector3(randomXPosition, 0f, 0f);
        }
    }
}
