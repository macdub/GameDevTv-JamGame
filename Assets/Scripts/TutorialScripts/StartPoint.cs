using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        col.gameObject.GetComponent<PlayerInput>().ActivateInput();
    }
}
