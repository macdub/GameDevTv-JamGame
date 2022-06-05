using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Health,
    //weapons,
    Sailor,
    Marines,
    Spacemarines
}

[RequireComponent(typeof(Rigidbody2D),typeof(CircleCollider2D))]
public class Item : MonoBehaviour
{   
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected int itemValue;

    private Rigidbody2D _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().TakeItem(itemType, itemValue);
            //_audioPlayer.PlayDropClip()
            Destroy(gameObject);
        }
    }
}
