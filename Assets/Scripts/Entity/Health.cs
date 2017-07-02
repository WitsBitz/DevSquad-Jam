using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour 
{
    [SerializeField] private int maxHealth;

    public UnityEvent damageTakenEvent;

    private int health;
    public int Value
    {
        get
        {
            return health;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        int newHealth = Mathf.Clamp(health - damage, 0, maxHealth);
        if(newHealth != health)
        {
            health = newHealth;
            damageTakenEvent.Invoke();
        }
    }

    private void Start()
    {
        health = MaxHealth;
    }

    public void ResetHealth()
    {
        health = MaxHealth;
    }
}
