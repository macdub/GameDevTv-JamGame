using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew : Item
{   
    private Vector2 _startLocation;

    void Start()
    {
        _startLocation = transform.position;
        itemType = ItemType.Health;
        itemValue = 10;
    }

    void Update()
    {
        //_spriteRenderer.sprite = sprites[0];
    }
}
