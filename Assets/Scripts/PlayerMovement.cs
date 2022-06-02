using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer _playerRenderer;
    private Animator _animator;
    private Rigidbody2D _playerRigidbody;
    private Vector2 _moveDirection;
    private Vector2 _moveToLocation;
    private bool _autoMove;
    private static readonly Vector2 SouthVector = new Vector2(0, -1);

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_autoMove && Vector2.Distance(transform.position, _moveToLocation) <= 0.5)
        {
            _moveDirection = Vector2.zero;
            _autoMove = false;
        }

        Sail();
    }

    private void Sail()
    {
        var velocity = _moveDirection * moveSpeed;// Vector2(_moveInput.x * moveSpeed, _moveInput.y * moveSpeed);
        _playerRigidbody.velocity = velocity;
        //FlipSprite();
        ScaleFog();
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

        var moveAngle = Vector2.Angle(_moveDirection, SouthVector);

        if (_moveDirection.x > 0 && _moveDirection.y > 0 && Between(moveAngle, 112.5f, 157.5f))
            _playerRenderer.sprite = sprites[1]; // north east
        else if (_moveDirection.x > 0 && _moveDirection.y < 0 && Between(moveAngle, 22.5f, 67.5f))
            _playerRenderer.sprite = sprites[7]; // south east
        else if (_moveDirection.x < 0 && _moveDirection.y < 0 && Between(moveAngle, 22.5f, 67.5f))
            _playerRenderer.sprite = sprites[5]; // south west
        else if (_moveDirection.x < 0 && _moveDirection.y > 0 && Between(moveAngle, 112.5f, 157.5f))
            _playerRenderer.sprite = sprites[3]; // north west
        else if (_moveDirection.y > 0 && Between(moveAngle, 157.5f, 202.5))
            _playerRenderer.sprite = sprites[2]; // north
        else if (_moveDirection.y < 0 && Between(moveAngle, -22.5f, 22.5f))
            _playerRenderer.sprite = sprites[6]; // south
        else if (_moveDirection.x < 0 && Between(moveAngle, 67.5f, 112.5f))
            _playerRenderer.sprite = sprites[4]; // west
        else if (_moveDirection.x > 0 && Between(moveAngle, 67.5f, 112.5f))
            _playerRenderer.sprite = sprites[0]; // east
    }

    private void ScaleFog()
    {
        if (_moveDirection == Vector2.zero) return;
        var moveAngle = Vector2.Angle(_moveDirection, SouthVector);
        var fogs = FindObjectsOfType<ParticleSystem>();
        
        if (Between(moveAngle, 157.5f, 202.5f) || Between(moveAngle, -22.5f, 22.5f))
        {
            foreach (var ps in fogs)
            {
                var psShape = ps.shape;
                psShape.radius = 0.6f;
            }
        }
        else
        {
            foreach (var ps in fogs)
            {
                var psShape = ps.shape;
                psShape.radius = 1.33f;
            }
        }
    }

    private static bool Between<T>(T item, T start, T end)
    {
        return Comparer<T>.Default.Compare(item, start) >= 0
               && Comparer<T>.Default.Compare(item, end) <= 0;
    }
    
    private void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();

        if (_moveDirection == Vector2.zero) return;
        _animator.SetFloat("XInput", _moveDirection.x);
        _animator.SetFloat("YInput", _moveDirection.y);
    }

    public void MoveTo(Vector2 location)
    {
        _moveToLocation = location;
        _moveDirection = (_moveToLocation - (Vector2)transform.position).normalized;
        _animator.SetFloat("XInput", _moveDirection.x);
        _animator.SetFloat("YInput", _moveDirection.y);
        _autoMove = true;
    }

    public Sprite GetSpriteAt(int i)
    {
        return i > sprites.Length ? null : sprites[i];
    }
}
