using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    Vector3 _projectileLocalSpawnPoint = Vector3.zero;
    [SerializeField] float launchCooldownSeconds = 3f;
    Coroutine _launchLoopCoroutine;

    void Start()
    {
        Transform projectileSpawnPoint = transform.Find("ProjectileSpawnPoint");
        if (projectileSpawnPoint != null)
        {
            _projectileLocalSpawnPoint = projectileSpawnPoint.localPosition;
        }
        _launchLoopCoroutine = StartCoroutine(LaunchLoop());
    }

    private void OnDestroy()
    {
        StopCoroutine(_launchLoopCoroutine);
    }


    void Launch()
    {
        var newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.transform.localPosition = _projectileLocalSpawnPoint;
    }

    IEnumerator LaunchLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(launchCooldownSeconds);
            Launch();
        }
    }
}
