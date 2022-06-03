using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Health,
    //weapons,
    sailor,
    marines,
    spacemarines
}

public class Item : MonoBehaviour
{   
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected int itemValue;

    private Rigidbody2D _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"collision detected, collider {collision.collider}");
        Debug.Log($"collision detected, other collider {collision.otherCollider}");

        Collider2D col = collision.collider;
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().TakeItem(itemType, itemValue);
            //_audioPlayer.PlayDropClip()
            Destroy(gameObject);
        }
    }
}
