using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject impactEffect;
    private const float ShotLife = 2.0f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var t = transform;
        if (col.CompareTag("Enemy"))
        {
            Debug.Log($"HIT ENEMY: {col.name}");
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
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

    public void SetFireDirection(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    public void SetShotSpeed(float inSpeed)
    {
        speed = inSpeed;
    }
}
