using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] private Vector2 startLocation;
    [SerializeField] private Vector2 targetLocation;
    [SerializeField] private Sprite[] sprites;

    private Vector2 _moveDirection;
    private SpriteRenderer _spriteRenderer;
    private static readonly Vector2 SouthVector = new Vector2(0, -1);
    private Detector _detector;
    private Cannon _cannon;
    private bool _autoMove;
    private NavMeshAgent _navMeshAgent;
    
    private void Start()
    {
        startLocation = transform.position;
        _detector = GetComponent<Detector>();
        _cannon = GetComponent<Cannon>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_detector.MoveToDetected)
        {
            if (_detector.Target is null)
            {
                // move back to start location
                _moveDirection = Vector2.Distance(transform.position, startLocation) <= 0.05f
                    ? transform.position
                    : startLocation;
            }
            else
            {
                var distance = Vector2.Distance(transform.position, _detector.Target.gameObject.transform.position);
                targetLocation = distance <= _detector.DetectorSize / 2f
                    ? transform.position
                    : _detector.Target.transform.position;
                _moveDirection = targetLocation;
            }
        }
        
        if (_detector.Target is null)
            _cannon.SetFireDirection(Vector2.zero);
        else
        {
            targetLocation = _detector.DirectionToTarget;
            _cannon.SetFireDirection(targetLocation);
        }

        Sail();
    }
    private void Sail()
    {
        _navMeshAgent.SetDestination(_moveDirection);
        FlipSprite();
    }

    public void MoveTo(Vector2 location)
    {
        targetLocation = location;
        _moveDirection = (targetLocation - (Vector2)transform.position).normalized;
        _autoMove = true;
    }

    // this needs to be updated to use the animation setup
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

        // got to be a cleaner way to do this
        if (_moveDirection.x > 0 && _moveDirection.y > 0 && Between(moveAngle, 112.5f, 157.5f))
            _spriteRenderer.sprite = sprites[1]; // north east
        else if (_moveDirection.x > 0 && _moveDirection.y < 0 && Between(moveAngle, 22.5f, 67.5f))
            _spriteRenderer.sprite = sprites[7]; // south east
        else if (_moveDirection.x < 0 && _moveDirection.y < 0 && Between(moveAngle, 22.5f, 67.5f))
            _spriteRenderer.sprite = sprites[5]; // south west
        else if (_moveDirection.x < 0 && _moveDirection.y > 0 && Between(moveAngle, 112.5f, 157.5f))
            _spriteRenderer.sprite = sprites[3]; // north west
        else if (_moveDirection.y > 0 && Between(moveAngle, 157.5f, 202.5))
            _spriteRenderer.sprite = sprites[2]; // north
        else if (_moveDirection.y < 0 && Between(moveAngle, -22.5f, 22.5f))
            _spriteRenderer.sprite = sprites[6]; // south
        else if (_moveDirection.x < 0 && Between(moveAngle, 67.5f, 112.5f))
            _spriteRenderer.sprite = sprites[4]; // west
        else if (_moveDirection.x > 0 && Between(moveAngle, 67.5f, 112.5f))
            _spriteRenderer.sprite = sprites[0]; // east
    }
}
