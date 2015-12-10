using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class HealthContainer
{

    public int MaxHealth = 2 * 5;
    public int Health = 2 * 5;

    public void Damage(int amount)
    {
        Health -= amount;
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }

}

