using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private float rateOfFire = 3.0f;
    [SerializeField] private float burstRate = 0.1f;
    [SerializeField] private float shotSpeed = 20.0f;
    [SerializeField] private int numShots = 1;
    [SerializeField] private string tagCompare = "";
    private Vector2 _shotDirection;
    public bool _canShoot = true;

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
            for (var i = numShots; i > 0; i--)
            {
                Debug.Log($"I: {i}");
                //StartCoroutine(RoundRate());
                var go = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
                var projectile = go.GetComponent<Projectile>();
                projectile.SetShotSettings(_shotDirection, shotSpeed, tagCompare);
                yield return new WaitForSeconds(burstRate);
            }

            yield return new WaitForSeconds(rateOfFire);
            _canShoot = true;
        }

        IEnumerator RoundRate()
        {
            var go = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
            var projectile = go.GetComponent<Projectile>();
            projectile.SetShotSettings(_shotDirection, shotSpeed, tagCompare);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnFire(InputValue value)
    {
        _shotDirection = value.Get<Vector2>();
    }

    public void SetFireDirection(Vector2 target)
    {
        _shotDirection = target;
    }
}
