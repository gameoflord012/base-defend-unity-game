using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    float attackDamage = 10;

    HashSet<Targeting> targets = new HashSet<Targeting>();

    public Targeting[] GetTargetings()
    {
        targets.Remove(null);
        return targets.ToArray();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var target in GetTargetings())
            {
                target.UpdateHealth(attackDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targets.Add(collision.attachedRigidbody.GetComponentInChildren<Targeting>());
    }
}
