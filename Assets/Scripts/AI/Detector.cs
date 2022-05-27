using System.Collections;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private string detectionTag = "Player";
    [SerializeField] private bool entityNearby;
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("Overlap Parameters")]
    [SerializeField] private Transform detectorOrigin;
    [SerializeField] private float detectorSize = 1f;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] private float detectionDelay = 0.3f;
    [SerializeField] private LayerMask detectorLayerMask;
    
    [Header("Gizmo Parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            entityNearby = target is not null;
        }
    }
    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    private void PerformDetection()
    {
        var collider = Physics2D.OverlapCircle(
            (Vector2) detectorOrigin.position + detectorOriginOffset,
            detectorSize,
            detectorLayerMask);

        Target = collider?.gameObject;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos || detectorOrigin == null) return;
        Gizmos.color = gizmoIdleColor;
        if (entityNearby)
            Gizmos.color = gizmoDetectedColor;
        Gizmos.DrawSphere((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
    }
}
