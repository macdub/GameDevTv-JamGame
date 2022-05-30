using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        Destroy(col.gameObject);
    }
}
