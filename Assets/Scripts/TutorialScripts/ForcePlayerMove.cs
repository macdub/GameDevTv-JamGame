using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForcePlayerMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        col.gameObject.GetComponent<PlayerInput>().DeactivateInput();
        col.gameObject.GetComponent<PlayerMovement>().MoveTo(GameObject.Find("MovePoint").transform.position);
    }
}
