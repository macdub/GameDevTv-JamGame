using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private float rateOfFire = 3.0f;
    [SerializeField] private float shotSpeed = 20.0f;
    private Vector2 _shotDirection;
    private bool _canShoot = true;

    private void Update()
    {
        Shoot();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        // don't create the projectile when the input is a zero vector
        if (_shotDirection == Vector2.zero) return;

        if (_canShoot)
        {
            StartCoroutine(FireRate());
        }
        
        IEnumerator FireRate()
        {
            _canShoot = false;
            var go = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
            var projectile = go.GetComponent<Projectile>();
            projectile.SetFireDirection(_shotDirection);
            projectile.SetShotSpeed(shotSpeed);
            yield return new WaitForSeconds(rateOfFire);
            _canShoot = true;
        }
    }

    private void OnFire(InputValue value)
    {
        _shotDirection = value.Get<Vector2>();
    }
}
