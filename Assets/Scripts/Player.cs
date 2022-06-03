using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Vector2 playerLocation;

    private void Update()
    {
        playerLocation = transform.position;
    }

    public void TakeItem(ItemType itemType, int itemValue)
    {
        switch (itemType)
        {
            case ItemType.Health:
                health = (health + itemValue <= maxHealth)? health + itemValue: maxHealth;
                break;

            default:
                Debug.Log($"Hit with Drop of type: {itemType}");
                break;
        }
    }
}
