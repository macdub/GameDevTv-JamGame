using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon :Weapon //: MonoBehaviour
{
    [SerializeField] private CannonType cannonType;
    private enum CannonType { Standard, Spray }
    
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
                var go = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
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
                var go = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                var projectile = go.GetComponent<Projectile>();
                projectile.SetShotSettings(shotVectors[i-1], shotSpeed, tagCompare);
            }
            _audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(rateOfFire);
            _canShoot = true;
        }
    }
}
