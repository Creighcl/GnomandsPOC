using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] int maxHitpoints;
    [SerializeField] int currentHitpoints;
    bool isDead = false;

    public void TakeDamage(int damage)
    {
        currentHitpoints -= damage;
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (currentHitpoints <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }


}
