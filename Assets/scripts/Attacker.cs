using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    float attackDamage = 10;

    [SerializeField]
    float attackDuration = 0.5f;
    float attackTimer = 100;

    HashSet<Targeting> targets = new HashSet<Targeting>();

    public UnityEvent onStartAttack;
    public UnityEvent onStopAttack;

    bool canAttack = true;

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public Targeting[] GetTargetings()
    {
        targets.Remove(null);
        return targets.ToArray();
    }

    private void Update()
    {
        if (attackTimer > attackDuration)
        {
            if(!canAttack)
            {
                onStopAttack.Invoke();
            }

            canAttack = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                attackTimer = 0;

                canAttack = false;
                onStartAttack.Invoke();

                foreach (var target in GetTargetings())
                {
                    target.AttackTarget(this);
                }
            }
        }

        attackTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targets.Add(collision.attachedRigidbody.GetComponentInChildren<Targeting>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targets.Remove(collision.attachedRigidbody.GetComponentInChildren<Targeting>());
    }
}
