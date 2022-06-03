using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected bool canDie = true;

    private LevelManager _levelManager;

    [SerializeField] protected GameObject healthItemPrefab;
    [SerializeField] protected int numOfHealthItemDroped = 3;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
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
        
        DropItems(healthItemPrefab, numOfHealthItemDroped);

        Destroy(gameObject);
    }

    protected void DropItems(GameObject itemPrefab, int numberOfItems)
    {
        if (numberOfItems == 1)
        {
            var item = Instantiate(itemPrefab, transform.position, transform.rotation);
        }
        else if (numberOfItems > 1) 
        {
            float radius = 1.5f;
            float angle = 2*Mathf.PI / numberOfItems;

            for( int i = 0; i<=numberOfItems; i++ ) 
            {
                Vector2 itemPosition = new Vector2(
                        transform.position.x + radius * Mathf.Cos(i * angle),
                        transform.position.y + radius * Mathf.Sin(i * angle)
                );

                var item = Instantiate(itemPrefab, itemPosition, transform.rotation);
            }
        }
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
