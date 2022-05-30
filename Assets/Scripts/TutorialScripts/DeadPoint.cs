using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        var enemyDetectors = FindObjectsOfType<Detector>();
        foreach (var detector in enemyDetectors)
            detector.ToggleDetection();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var enemies = FindObjectsOfType<Enemy>();
        foreach(var enemy in enemies)
            enemy.MoveTo(GameObject.Find("DeleteEnemy").transform.position);
    }
}
