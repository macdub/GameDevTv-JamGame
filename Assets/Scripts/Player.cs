using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Vector2 playerLocation;

    [SerializeField] private int _sailorCrewLvl = 0;
    [SerializeField] private int _marinesCrewLvl = 0;
    [SerializeField] private int _spaceMarinesCrewLvl = 0;

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

            case ItemType.Sailor:
                _sailorCrewLvl += itemValue;
                break;

            case ItemType.Marines:
                _marinesCrewLvl += itemValue;
                break;

            case ItemType.Spacemarines:
                _spaceMarinesCrewLvl += itemValue;
                break;

            default:
                Debug.Log($"Hit with Drop of type: {itemType}");
                break;
        }
    }
}
