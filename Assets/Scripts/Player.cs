using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Vector2 playerLocation;

    private void Update()
    {
        playerLocation = transform.position;
    }
}
