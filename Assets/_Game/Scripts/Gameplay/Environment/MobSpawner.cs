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
        TurretSceneManager.Instance.onTimerExpire -= HandleTimerExpire;
    }

    void Start()
    {
        TurretSceneManager.Instance.onTimerExpire += HandleTimerExpire;
        StartSpawner();
    }

    private void HandleTimerExpire()
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
            newMob.transform.SetParent(GameObject.Find("Actors").transform);
            newMob.transform.localPosition = new Vector3(randomXPosition, 0f, 0f);
            yield return new WaitForSeconds(1f);
        }
    }
}
