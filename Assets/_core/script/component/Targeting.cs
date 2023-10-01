using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Targeting : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float maxHealth;

    [SerializeField] HealthBar healthBar;

    public UnityEvent<Attacker> onTargetGetAttacked;
    public UnityEvent onHealthReachZero;

    Vector2 lastAttackedDirection;

    public Vector2 GetLastAttackedDirection()
    {
        return lastAttackedDirection;
    }

    public void AttackTarget(Attacker attacker)
    {
        if (health <= attacker.GetAttackDamage())
        {
            health = 0;
            onHealthReachZero.Invoke();
        }
        else health -= attacker.GetAttackDamage();

        onTargetGetAttacked.Invoke(attacker);
        lastAttackedDirection = transform.position - attacker.transform.position;

        UpdatHealthBar();
    }

    private void UpdatHealthBar()
    {
        healthBar.SetHealthPercent(GetHealthPercentage());
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth;
    }

    private void Start()
    {
        UpdatHealthBar();
    }

    private void OnValidate()
    {
        if(healthBar == null)
            Debug.LogWarning("healthBar is null", gameObject);
    }
}
