using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class Attacker : MonoBehaviour
{
    [SerializeField] float attackDamage = 10;
    [SerializeField] float attackDuration = 0.5f;
    [SerializeField] float attackTimer = 100;
    [SerializeField] ColliderBroadcaster colliderBroadcaster;
    [SerializeField] bool autoAttack;

    HashSet<Targeting> targets = new HashSet<Targeting>();

    public UnityEvent onStartAttack;
    public UnityEvent onStopAttack;
    public UnityEvent<Targeting> onTargetEnterTrigger;

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
        }

        if(autoAttack && canAttack && targets.Count > 0)
        {
            AttackTargets();
        }

        attackTimer += Time.deltaTime;
    }

    public void AttackTargets()
    {
        if (!canAttack) return;

        attackTimer = 0;

        canAttack = false;
        onStartAttack.Invoke();

        foreach (var target in GetTargetings())
        {
            target.AttackTarget(this);
        }
    }

    private void OnEnable()
    {
        colliderBroadcaster.onColliderEnter2D.AddListener(AddTargetEnterTrigger);
        colliderBroadcaster.onColliderExit2D.AddListener(RemoveTargetExitTrigger);
    }

    private void OnDisable()
    {
        colliderBroadcaster.onColliderEnter2D.RemoveListener(AddTargetEnterTrigger);
        colliderBroadcaster.onColliderExit2D.RemoveListener(RemoveTargetExitTrigger);
    }

    private void AddTargetEnterTrigger(Collider2D source, Collider2D collision)
    {
        if (collision.CompareTag(Tags.target_collider))
        {
            var targeting = collision.attachedRigidbody.GetComponentInChildren<Targeting>();
            targets.Add(targeting);
            onTargetEnterTrigger?.Invoke(targeting);
        }
    }

    private void RemoveTargetExitTrigger(Collider2D source, Collider2D collision)
    {
        if (collision.CompareTag(Tags.target_collider))
            targets.Remove(collision.attachedRigidbody.GetComponentInChildren<Targeting>());
    }
}
