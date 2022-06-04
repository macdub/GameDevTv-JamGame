using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] protected GameObject[] itemDropPrefabs;

    private float _radius = 1.5f;
    private int _itemCount = 0;

    private void Awake() 
    {
        _itemCount = itemDropPrefabs.Length;
    }

    public void DropItems()
    {
        if (_itemCount == 1)
        {
            var item = Instantiate(itemDropPrefabs[0], transform.position, transform.rotation);
        }
        else if (_itemCount > 1) 
        {
            float angle = 2*Mathf.PI / _itemCount;

            for( int i=0; i<_itemCount; i++ ) 
            {
                Vector2 itemPosition = new Vector2(
                        transform.position.x + _radius * Mathf.Cos(i * angle),
                        transform.position.y + _radius * Mathf.Sin(i * angle)
                );

                var item = Instantiate(
                    itemDropPrefabs[i], 
                    itemPosition, 
                    transform.rotation);
            }
        }
    }
}
