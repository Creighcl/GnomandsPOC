using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] float minX = 0f;
    [SerializeField] float maxX = 118f;
    [SerializeField] List<GameObject> mobPrefabs = new List<GameObject>();
    bool isSpawning = false;

    void OnDestroy()
    {
        TurretSceneManager.instance.onTimeHasRunOut -= HandleTimeHasRunOut;
    }

    void Start()
    {
        TurretSceneManager.instance.onTimeHasRunOut += HandleTimeHasRunOut;
        StartSpawner();
    }

    private void HandleTimeHasRunOut()
    {
        if (isSpawning)
        {
            StopSpawner();
        }
    }

    public void StartSpawner()
    {
        isSpawning = true;
        StartCoroutine(SpawnLoop());
    }

    public void StopSpawner()
    {
        isSpawning = false;
    }

    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            float randomXPosition = Random.Range(minX, maxX);
            int randomMobIndex = Random.Range(0, mobPrefabs.Count);
            GameObject randomMob = mobPrefabs[randomMobIndex];
            var newMob = Instantiate(randomMob, transform);
            newMob.transform.localPosition = new Vector3(randomXPosition, 0f, 0f);
            yield return new WaitForSeconds(1f);
        }
    }
}
