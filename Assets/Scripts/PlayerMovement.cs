using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer _playerRenderer;
    private Rigidbody2D _playerRigidbody;
    private Vector2 _moveInput;
    private static readonly Vector2 NorthVector = new Vector2(0, -1);

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Sail();
    }

    private void Sail()
    {
        var velocity = new Vector2(_moveInput.x * moveSpeed, _moveInput.y * moveSpeed);
        _playerRigidbody.velocity = velocity;
    }

    private void FlipSprite()
    {
        // 8 directions
        // 0 : East -> 1, 0
        // 1 : North East -> 1, -1
        // 2 : North -> 0, -1
        // 3 : North West -> -1, -1
        // 4 : West -> -1, 0
        // 5 : South West -> -1, 1
        // 6 : South -> 0, 1
        // 7 : South East -> 1, 1

        var moveAngle = Vector2.Angle(_moveInput, NorthVector);

        if (_moveInput.x > 0 && _moveInput.y > 0 && Between(moveAngle, 112.5f, 157.5f))
            _playerRenderer.sprite = sprites[1]; // north east
        else if (_moveInput.x > 0 && _moveInput.y < 0 && Between(moveAngle, 22.5f, 67.5f))
            _playerRenderer.sprite = sprites[7]; // south east
        else if (_moveInput.x < 0 && _moveInput.y < 0 && Between(moveAngle, 22.5f, 67.5f))
            _playerRenderer.sprite = sprites[5]; // south west
        else if (_moveInput.x < 0 && _moveInput.y > 0 && Between(moveAngle, 112.5f, 157.5f))
            _playerRenderer.sprite = sprites[3]; // north west
        else if (_moveInput.y > 0 && Between(moveAngle, 157.5f, 202.5))
            _playerRenderer.sprite = sprites[2]; // north
        else if (_moveInput.y < 0 && Between(moveAngle, -22.5f, 22.5f))
            _playerRenderer.sprite = sprites[6]; // south
        else if (_moveInput.x < 0 && Between(moveAngle, 67.5f, 112.5f))
            _playerRenderer.sprite = sprites[4]; // west
        else if (_moveInput.x > 0 && Between(moveAngle, 67.5f, 112.5f))
            _playerRenderer.sprite = sprites[0]; // east
    }

    private static bool Between<T>(T item, T start, T end)
    {
        return Comparer<T>.Default.Compare(item, start) >= 0
               && Comparer<T>.Default.Compare(item, end) <= 0;
    }
    
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        Debug.Log($"VECTOR: {_moveInput} VECTOR ANGLE: {Vector2.Angle(_moveInput, NorthVector)}");
        FlipSprite();
    }
}
