using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// PlayerStats Script.
/// </summary>
public class PlayerStats : Script
{
    public int Health { get; set; } = 100;
    public int HealthMaxHealth { get; set; } = 100;
    public bool IsImmortal { get; set; } = false;

    public override void OnUpdate()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (IsImmortal)
            return;
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > HealthMaxHealth)
            Health = HealthMaxHealth;
    }
    public void Die()
    {
        // Here you can add code that needs to be called when player dies
    }

    public void Respawn()
    {
        Health = HealthMaxHealth;
    }

}
