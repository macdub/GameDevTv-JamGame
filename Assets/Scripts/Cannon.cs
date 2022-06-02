using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Vector2 _shotDirection;

    [SerializeField] private CannonType cannonType;
    private enum CannonType { Standard, Spray }
    public bool _canShoot = true;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    
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
            switch (cannonType)
            {
                case CannonType.Spray:
                    StartCoroutine(FireSpray());
                    break;
                case CannonType.Standard:
                    StartCoroutine(FireRate());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        IEnumerator FireRate()
        {
            _canShoot = false;
            for (var i = numShots; i > 0; i--)
            {
                //StartCoroutine(RoundRate());
                var go = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
                var projectile = go.GetComponent<Projectile>();
                projectile.SetShotSettings(_shotDirection, shotSpeed, tagCompare);
                _audioPlayer.PlayShootingClip();
                yield return new WaitForSeconds(burstRate);
            }

            yield return new WaitForSeconds(rateOfFire);
            _canShoot = true;
        }

        IEnumerator FireSpray()
        {
            _canShoot = false;
            var shotVectors = GenerateSprayCone();
            for (var i = numShots; i > 0; i--)
            {
                var go = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
                var projectile = go.GetComponent<Projectile>();
                projectile.SetShotSettings(shotVectors[i-1], shotSpeed, tagCompare);
            }
            _audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(rateOfFire);
            _canShoot = true;
        }
    }

    private void OnFire(InputValue value)
    {
        _shotDirection = value.Get<Vector2>();
    }

    private List<Vector3> GenerateSprayCone()
    {
        var angleStep = 15f / numShots;
        var currentAngle = -angleStep * numShots/2;
        var axis = Vector3.Cross(_shotDirection, Vector3.up);
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
