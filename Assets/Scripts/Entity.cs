using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected bool canDie = true;

    private LevelManager _levelManager;
    private ItemDropper _itemDropper;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _itemDropper = GetComponent<ItemDropper>();
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0) Die();
    }

    private void Die()
    {
        // transition to death animation
        // destroy object
        if(CompareTag("Player") && canDie)
            _levelManager.LoadGameOver();
        
        if (_itemDropper != null) {
            _itemDropper.DropItems();
        }

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
