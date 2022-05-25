using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private float fireRate = 3.0f;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnFire(InputValue value)
    {
        var shotDirection = value.Get<Vector2>();
    }
}
