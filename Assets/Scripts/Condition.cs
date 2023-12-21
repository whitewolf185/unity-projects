using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{

    [SerializeField] HealthBar _healthBar;
    [SerializeField] int _maxHealth;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _healthBar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
