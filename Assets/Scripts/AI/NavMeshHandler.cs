using UnityEngine;
using UnityEngine.AI;

public class NavMeshHandler : MonoBehaviour
{
    private void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}
