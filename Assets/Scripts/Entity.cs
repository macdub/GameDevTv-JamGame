using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    
    
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0) Die();
    }

    private void Die()
    {
        // transition to death animation
        // destroy object
        Destroy(gameObject);
    }

    protected static bool Between<T>(T item, T start, T end)
    {
        return Comparer<T>.Default.Compare(item, start) >= 0
               && Comparer<T>.Default.Compare(item, end) <= 0;
    }

    public float MaxHealth
    {
        get => maxHealth;
        protected set => maxHealth = value;
    }

    public float CurrentHealth
    {
        get => health;
        protected set => health = value;
    }
}
