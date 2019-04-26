using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] Animator anim = null;
    [SerializeField] BoxCollider2D blastRadius = null;
    [SerializeField] BoxCollider2D secondaryBlastRadius = null;

   public void Fire()
    {
        var a = new ContactFilter2D();
        a.SetLayerMask(LayerMask.GetMask("Enemy"));
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(blastRadius, a, results);
        if (secondaryBlastRadius.enabled)
        {
            List<Collider2D> resultsSecondaryCamera = new List<Collider2D>();
            Physics2D.OverlapCollider(secondaryBlastRadius, a, resultsSecondaryCamera);
            results.AddRange(resultsSecondaryCamera);
        }

        if (results.Count > 0)
        {
            results.ForEach(o => {
                Destructible destructibleObject = o.GetComponent<Destructible>();
                if (destructibleObject != null) {
                    destructibleObject.TakeDamage(5);
                }
            });
            }
    }
}
