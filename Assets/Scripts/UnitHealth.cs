using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    //Script to manage health
    public int currentHealth;
    int maxHealth;

    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    // Constructor
    public UnitHealth(int _health, int _masxHealth)
    {
        currentHealth = _health;
        maxHealth = _masxHealth;
    }

    //Methods
    public void DamageUnit(int _dmgAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= _dmgAmount;
        }
    }

    public void HealUnit(int _healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += _healAmount;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
