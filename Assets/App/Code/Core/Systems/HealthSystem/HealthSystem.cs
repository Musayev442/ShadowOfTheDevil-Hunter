using System;
using UnityEngine;
using App.Code.Core.Systems.Interfaces;

namespace App.Code.Core.Systems.HealthSystem
{
    [System.Serializable]
public class HealthSystem
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration = 0.5f;
    
    // Properties
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    
    // Events
    public event Action OnDeath;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnDamageTaken;
    
    private float invulnerabilityTimer;
    private string ownerName;
    
    public HealthSystem(string ownerName = "Character")
    {
        this.ownerName = ownerName;
        currentHealth = maxHealth;
    }
    
    public void Update(float deltaTime)
    {
        // Handle invulnerability timer
        if (isInvulnerable)
        {
            invulnerabilityTimer -= deltaTime;
            if (invulnerabilityTimer <= 0)
            {
                isInvulnerable = false;
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (isInvulnerable || IsDead || damage <= 0) return;
        
        int oldHealth = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        OnDamageTaken?.Invoke(damage);
        OnHealthChanged?.Invoke(currentHealth);
        
        Debug.Log($"{ownerName} took {damage} damage. Health: {currentHealth}/{maxHealth}");
        
        if (IsDead && oldHealth > 0)
        {
            OnDeath?.Invoke();
        }
        
        // Temporary invulnerability after taking damage
        if (!IsDead)
        {
            StartInvulnerability();
        }
    }
    
    public void TakePhysicalDamage(int damage, int defense)
    {
        int actualDamage = Mathf.Max(1, damage - defense);
        TakeDamage(actualDamage);
    }
    
    public void TakeMagicDamage(int damage, int magicDefense)
    {
        int actualDamage = Mathf.Max(1, damage - magicDefense);
        TakeDamage(actualDamage);
    }
    
    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;
        
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        OnHealthChanged?.Invoke(currentHealth);
        Debug.Log($"{ownerName} healed for {amount}. Health: {currentHealth}/{maxHealth}");
    }
    
    public void SetMaxHealth(int newMaxHealth)
    {
        float healthPercentage = maxHealth > 0 ? (float)currentHealth / maxHealth : 1f;
        maxHealth = newMaxHealth;
        currentHealth = Mathf.RoundToInt(newMaxHealth * healthPercentage);
        OnHealthChanged?.Invoke(currentHealth);
    }
    
    private void StartInvulnerability()
    {
        if (invulnerabilityDuration > 0)
        {
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
        }
    }
    
    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
        if (!invulnerable) invulnerabilityTimer = 0;
    }
}
}