using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject impactEffect;
    private string _tagCompare = "Enemy";
    private const float ShotLife = 2.0f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var t = transform;
        
        if (col.CompareTag(_tagCompare))
        {
            Debug.Log($"HIT: {col.name}");
            col.gameObject.GetComponent<Entity>().TakeDamage(damage);
            Instantiate(impactEffect, t.position, t.rotation);
            Destroy(gameObject);
        }
        else if (col.CompareTag("Obstacle"))
        {
            Debug.Log($"HIT OBSTACLE: {col.name}");
            Instantiate(impactEffect, t.position, t.rotation);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, ShotLife);
        }
    }

    public void SetShotSettings(Vector2 direction, float inSpeed, string tagCompare)
    {
        _tagCompare = tagCompare;
        speed = inSpeed;
        rb.velocity = direction.normalized * speed;
    }
}
