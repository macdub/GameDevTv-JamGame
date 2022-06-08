using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject shotPrefab;
    
    [Header("Shot Stats")]
    [SerializeField] protected float rateOfFire = 3.0f;
    [SerializeField] protected float burstRate = 0.1f;
    [SerializeField] protected int numShots = 1;
    [SerializeField] protected float shotSpeed = 20.0f;
    [SerializeField] protected string tagCompare;
    [SerializeField] protected Vector2 _shotDirection;

    protected bool _canShoot = true;
    protected AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    protected void OnFire(InputValue value)
    {
        _shotDirection = value.Get<Vector2>();
    }

    protected List<Vector3> GenerateSprayCone()
    {
        var angleStep = 15f / numShots;
        var currentAngle = -angleStep * numShots / 2;
        
        // In the case where _shotDirection CROSS Vector3.up is a zero vector, use Vector3.right to find the
        // axis vector. Otherwise, the vector from the AngleAxis calculations will be zero vectors and not
        // actually spread the shots correctly.
        var crossVector = Vector3.Cross(_shotDirection, Vector3.up) == Vector3.zero
            ? Vector3.right
            : Vector3.up;
        var axis = Vector3.Cross(_shotDirection, crossVector);
        var results = new List<Vector3> {Quaternion.AngleAxis(currentAngle, axis) * _shotDirection};

        while (results.Count < numShots)
        {
            currentAngle += angleStep;
            results.Add(Quaternion.AngleAxis(currentAngle, axis) * _shotDirection);
            if (currentAngle <= Mathf.Epsilon && numShots % 2 == 0)
                currentAngle += angleStep;
        }
        
        return results;
    }

    public void SetFireDirection(Vector2 target)
    {
        _shotDirection = target;
    }
}
