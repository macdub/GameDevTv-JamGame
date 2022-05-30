using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInTrigger : MonoBehaviour
{
    [SerializeField] private string checkTag;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag(checkTag))
            EntityCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            EntityCount--;
    }

    public int EntityCount { get; private set; } = 0;
}
