using Cinemachine;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject _followCam;
    private GameObject _enemyArea;
    private bool isSpawned;

    private void Awake()
    {
        _followCam = GameObject.Find("FollowCamera");
        _enemyArea = GameObject.Find("EnemyChecker");
    }

    private void Update()
    {
        if (_enemyArea.GetComponent<CheckInTrigger>().EntityCount > 0 || isSpawned) return;
        isSpawned = true;
        CreatePlayer();
    }
    
    public void CreatePlayer()
    {
        var go = Instantiate(playerPrefab, transform.position, transform.rotation);
        _followCam.GetComponent<CinemachineVirtualCamera>().m_Follow = go.transform;
    }
}
