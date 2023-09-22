using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float maxHealth;

    [SerializeField] HealthBar healthBar;

    public void UpdateHealth(float damage)
    {
        if (health < damage) health = 0;
        else if (damage <= health) health -= damage;

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
        Debug.LogWarning("healthBar is null");
    }
}
